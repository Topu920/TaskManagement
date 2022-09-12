using Application.Common.Interfaces;

namespace Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;

        public DateTime? ConvertDateToBangladeshDateFormat(DateTime? dateTime)
        {
            if (dateTime == null)
            {
                return null;
            }

            var bdZone = TimeZoneInfo.FindSystemTimeZoneById("Bangladesh Standard Time");
            var localDateTime = TimeZoneInfo.ConvertTimeFromUtc((DateTime)dateTime, bdZone);
            return localDateTime;



        }
    }
}
