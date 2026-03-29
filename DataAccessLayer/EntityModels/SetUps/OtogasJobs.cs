using DataAccessLayer.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityModels.SetUps
{
	public class OtogasJobs
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public long Id { get; set; }
		public string JobName { get; set; } = string.Empty;
		public string JobCode { get; set; } = string.Empty;
		public int JobStatus { get; set; }
		public DateTime LastRun { get; set; }
		public bool HasRun { get; set; }
	}
}
