using DataAccessLayer.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Graph.Models;

namespace FuelFlow.Extensions;
public static class ViewInitializer
{
	public static async Task UpdateViewsAsync(OTOContext context)
	{

		await context.Database.ExecuteSqlRawAsync(
	  @"DROP VIEW IF EXISTS ""vw_SalesData"" CASCADE;");

		await context.Database.ExecuteSqlRawAsync(
			@"DROP VIEW IF EXISTS ""vw_PaymentsView"" CASCADE;");

		// Create vw_PaymentsView first
		await context.Database.ExecuteSqlRawAsync(@"
CREATE OR REPLACE VIEW ""vw_PaymentsView"" AS
SELECT
    pt.""SaleId"",
    STRING_AGG(pt.""PaymentRefrence"", ';') AS ""TransId""
FROM ""PaymentTransactions"" pt
WHERE pt.""PaymentRefrence"" IS NOT NULL
  AND pt.""PaymentRefrence"" <> ''
GROUP BY pt.""SaleId"";
");

		// Then create vw_SalesData
		await context.Database.ExecuteSqlRawAsync(@"
CREATE OR REPLACE VIEW ""vw_SalesData"" AS
SELECT
    qt.""ShiftNumber"",
    qt.""SaleId"",
    qt.""DateCreated"" AS ""SalesDate"",

    s.""StationName"",
    d.""TillNumber"",
    d.""DispenserName"",
    d.""StorageLocation"",
    z.""NozzleName"",

    CONCAT(
        COALESCE(su.""FirstName"", ''),
        ' ',
        COALESCE(su.""MiddName"", ''),
        ' ',
        COALESCE(su.""LastName"", '')
    ) AS ""AttendantName"",

    c.""CustomerName"",

    COALESCE(
        v.""VehicleRegistrationNumber"",
        u.""FirstName""
    ) AS ""Vehicle"",

    pd.""ProductName"" AS ""PetroleumName"",

    p.""PaymentTypeName"" AS ""PaymentType"",

    (qt.""QuantityCredit"" - qt.""QuantityDebit"") AS ""Litres"",

    qt.""Price"",

    (qt.""AmountCredit"" - qt.""AmountDebit"") AS ""Amount"",

    vp.""TransId"",

    CAST(0.00 AS numeric(18,2)) AS ""RunningBalance""

FROM ""QuantityTransactions"" qt

INNER JOIN ""Nozzles"" z
    ON z.""NozzleCode"" = qt.""NozzleCode""

INNER JOIN ""Dispensers"" d
    ON d.""DispenserCode"" = qt.""DispenserCode""

INNER JOIN ""Stations"" s
    ON s.""StationCode"" = qt.""StationCode""

INNER JOIN ""PaymentTypes"" p
    ON p.""PaymentTypeId"" = qt.""PaymentTypeCode""

INNER JOIN ""Shifts"" sh
    ON sh.""ShiftNumber"" = qt.""ShiftNumber""

INNER JOIN ""AspNetUsers"" su
    ON su.""UserCode"" = sh.""UserCode""

LEFT JOIN ""Vehicles"" v
    ON v.""VehicleCode"" = qt.""VehicleCode""

LEFT JOIN ""AspNetUsers"" u
    ON u.""UserCode"" = qt.""VehicleCode""

LEFT JOIN ""Customers"" c
    ON c.""CustomerCode"" = v.""CustomerCode""

LEFT JOIN ""Products"" pd
    ON pd.""ProductCode"" = v.""ProductCode""

LEFT JOIN ""vw_PaymentsView"" vp
    ON vp.""SaleId"" = qt.""SaleId"";
");
	}
}