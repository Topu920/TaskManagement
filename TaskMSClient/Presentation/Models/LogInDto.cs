namespace Presentation.Models
{
    public class LogInDto
    {
        public long? userId { get; set; }
        public string empId { get; set; } = null!;
        public int? empNo { get; set; }
        public string? userName { get; set; }

        public string? usrDesign { get; set; }

        public bool isUserExist { get; set; }
    }
}
