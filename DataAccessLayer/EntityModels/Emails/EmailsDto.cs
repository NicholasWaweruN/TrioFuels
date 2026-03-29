using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityModels.Emails
{
	public class EmailsDto
	{
		public string To { get; set; } = string.Empty;
		public string ToCC { get; set; } = string.Empty;
	}
}
