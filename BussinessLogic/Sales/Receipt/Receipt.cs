namespace BusinessLogic.Sales.Receipts
{
	using BussinessLogic.Authentication.CommonTasks;
	using BussinessLogic.Sales.NewSales;
	using DataAccessLayer.Context;
	using Syncfusion.Pdf;

	/// <summary>
	/// Defines the <see cref="ReceiptService" />
	/// </summary>
	public class ReceiptService
	{
		/// <summary>
		/// Defines the _document
		/// </summary>
		private readonly PdfDocument _document;

		/// <summary>
		/// Defines the _context
		/// </summary>
		private readonly OTOContext _context;

		/// <summary>
		/// Defines the _authentication
		/// </summary>
		private readonly IAuthCommonTasks _authentication;

		/// <summary>
		/// Defines the _messaging
		/// </summary>
		private readonly IEmailService _messaging;

		/// <summary>
		/// Initializes a new instance of the <see cref="ReceiptService"/> class.
		/// </summary>
		/// <param name="document">The document<see cref="PdfDocument"/></param>
		/// <param name="context">The context<see cref="OTOContext"/></param>
		/// <param name="authentication">The authentication<see cref="IAuthCommonTasks"/></param>
		/// <param name="messaging">The messaging<see cref="IEmailService"/></param>
		public ReceiptService(PdfDocument document, OTOContext context, IAuthCommonTasks authentication, IEmailService messaging)
		{
			_document = document;
			_context = context;
			_authentication = authentication;
			_messaging = messaging;
		}

		/// <summary>
		/// The GenerateFuelReceiptHtml
		/// </summary>
		/// <param name="receipt">The receipt<see cref="FuelTransactionDto"/></param>
		/// <returns>The <see cref="string"/></returns>

	}
}
