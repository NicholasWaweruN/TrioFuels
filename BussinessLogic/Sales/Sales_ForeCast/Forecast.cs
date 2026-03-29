using BussinessLogic.Authentication.CommonTasks;
using BusinessLogic.Sales.CommonSalesTasks;
using DataAccessLayer.Common;
using DataAccessLayer.Context;
using DataAccessLayer.DTOs.Payments;
using Microsoft.EntityFrameworkCore;
using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Transforms.TimeSeries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogic.Sales.Sales_ForeCast
{
	public class Forecast
	{
		private readonly IAuthCommonTasks _authentication;
		private readonly OTOContext _context;
		private readonly ICommonSalesTasks _salesTasks;
		public Forecast(IAuthCommonTasks authentication, OTOContext context, ICommonSalesTasks salesTasks)
		{
			_authentication = authentication;
			_context = context;
			_salesTasks = salesTasks;
		}
		public async Task<ServiceResponse<object>> GetForeCastData()
		{
			var sql = @$"Select SalesDate, Amount, Litres from vw_SalesData ORDER BY SalesDate ASC";

			try
			{
				var result = _context.Database.SqlQueryRaw<FuelSale>(sql);
				var transactions = await result.AsNoTracking().ToListAsync();

				if (transactions.Count == 0)
					return ServiceResponse<object>.Information("No sales data found", null);

				var forecastData = PredictFutureSales(transactions);

				return ServiceResponse<object>.Success("Sales forecast generated successfully", forecastData);
			}
			catch (Exception ex)
			{
				return ServiceResponse<object>.Error("Something went wrong", ex.Message);
			}
		}

		private List<float> PredictFutureSales(List<FuelSale> salesData)
		{
			MLContext mlContext = new MLContext();

			var data = mlContext.Data.LoadFromEnumerable(salesData);

			var forecastingPipeline = mlContext.Forecasting.ForecastBySsa(
				outputColumnName: nameof(SalesForecast.PredictedSales),
				inputColumnName: nameof(FuelSale.Amount), // Predict based on sales amount
				windowSize: 7,   // 7-day moving average
				seriesLength: 30, // Use last 30 records
				trainSize: salesData.Count,
				horizon: 10 // Predict next 10 days
			);

			var model = forecastingPipeline.Fit(data);

			var forecastEngine = model.CreateTimeSeriesEngine<FuelSale, SalesForecast>(mlContext);

			var forecast = forecastEngine.Predict();

			return [.. forecast.PredictedSales];
		}


		public class FuelSale
		{
			public DateTime SalesDate { get; set; }
			public float Amount { get; set; } // Sales Amount
			public float Litres { get; set; } // Fuel Sold
		}


		public class SalesForecast
		{
			[ColumnName("PredictedSales")]
			public float[] PredictedSales { get; set; } = [];
		}


}
}
