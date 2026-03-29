namespace DataAccessLayer.EntityModels.Transactions
{
	using DataAccessLayer.Common;
	using Microsoft.EntityFrameworkCore;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	/// <summary>
	/// Defines the <see cref="CustomerTransactions" />
	/// </summary>
	public class CustomerTransactions : BaseEntity
	{
		/// <summary>
		/// Gets or sets the VehicleCode
		/// </summary>
		[Required, StringLength(10), Unicode(false)]
		public string VehicleCode { get; set; } = string.Empty;

		/// <summary>
		/// Gets or sets the TransactionReference
		/// </summary>
		[Required, StringLength(30), Unicode(false)]
		public string TransactionReference { get; set; } = string.Empty;

		/// <summary>
		/// Gets or sets the Credit
		/// </summary>
		[Precision(18, 2)] public decimal Credit { get; set; } = 0;

		/// <summary>
		/// Gets or sets the Debit
		/// </summary>
		[Precision(18, 2)] public decimal Debit { get; set; } = 0;

		/// <summary>
		/// Gets or sets the UserReference
		/// </summary>
		[Required, StringLength(30), Unicode(false)]
		public string UserReference { get; set; } = string.Empty;

		/// <summary>
		/// Gets or sets the Narration
		/// </summary>
		[Required, StringLength(100), Unicode(false)]
		public string Narration { get; set; } = string.Empty;

		/// <summary>
		/// Gets or sets the TopUpType
		/// </summary>
		public int TopUpType { get; set; }

		/// <summary>
		/// Gets or sets the Source
		/// </summary>
		public int Source { get; set; }
		[Required, StringLength(30), Unicode(false)]
		public string BatchNumber { get; set; } = string.Empty;
	}

	/// <summary>
	/// Defines the <see cref="CustomerFunds" />
	/// </summary>
	public class CustomerFunds : BaseEntity
	{
		/// <summary>
		/// Gets or sets the CustomerCode
		/// </summary>
		[Required, StringLength(10), Unicode(false)]
		public string CustomerCode { get; set; } = string.Empty;

		/// <summary>
		/// Gets or sets the SystemReference
		/// </summary>
		[Required, StringLength(30), Unicode(false)]
		public string SystemReference { get; set; } = string.Empty;

		/// <summary>
		/// Gets or sets the Credit
		/// </summary>
		[Precision(18, 2)] public decimal Credit { get; set; } = 0;

		/// <summary>
		/// Gets or sets the Debit
		/// </summary>
		[Precision(18, 2)] public decimal Debit { get; set; } = 0;

		/// <summary>
		/// Gets or sets the UserReference
		/// </summary>
		[Required, StringLength(30), Unicode(false)]
		public string UserReference { get; set; } = string.Empty;

		/// <summary>
		/// Gets or sets the Narration
		/// </summary>
		[Required, StringLength(100), Unicode(false)]
		public string Narration { get; set; } = string.Empty;
	}

	/// <summary>
	/// Defines the <see cref="TopUpTypes" />
	/// </summary>
	public class TopUpTypes
	{
		/// <summary>
		/// Gets or sets the TopUpType
		/// </summary>
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int TopUpType { get; set; }

		/// <summary>
		/// Gets or sets the TopUpDescription
		/// </summary>
		[StringLength(30), Required, Unicode(false)]
		public string TopUpDescription { get; set; } = string.Empty;
	}
}
