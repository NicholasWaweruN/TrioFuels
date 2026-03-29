using DataAccessLayer.Common;

namespace DataAccessLayer.DTOs.EmailDtos
{
    public class emailDtos : BaseEntity
    {
        public string ReportName { get; set; } = string.Empty;
        public string ToEmailAddress { get; set; } = string.Empty;
        public string FromEmailAddress { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public string Cc { get; set; } = string.Empty;
    }

}
