using DataAccessLayer.Common;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.EntityModels.Transactions
{
    public class ProgramTypes : BaseEntity
    {
        [Required, StringLength(4),Unicode(false)]
        public string ProgramTypeCode { get; set; } = string.Empty;
        [Required, StringLength(50), Unicode(false)]
        public string ProgramTypeName { get; set; } = string.Empty;
    }
}
