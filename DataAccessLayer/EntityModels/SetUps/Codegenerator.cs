using DataAccessLayer.Common;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.EntityModels.Transactions
{
    public class Codegenerator : BaseEntity
	{ 
        public int Seed { get; set; }
        public int NextNumber { get; set; }
        [StringLength(10), Unicode(false)]
        public string Prefix { get; set; } = string.Empty;
        [StringLength(10), Unicode(false)]
        public string Suffix { get; set; } = string.Empty;
        [StringLength(30), Unicode(false)]
        public string TypeName { get; set; } = string.Empty;
        public int Length { get; set; }
    }
}
