namespace Application.Common.Models
{
    public class RequestLoggerEntity
    {
        public int Id { get; set; }
        public string RequestName { get; set; } = null!;
        public int UserId { get; set; }
        public string UserName { get; set; } = null!;
        public DateTime DateTime { get; set; }
        public string UserIp { get; set; } = null!;
    }
}
