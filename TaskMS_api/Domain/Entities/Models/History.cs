using System;
using System.Collections.Generic;

namespace Domain.Entities.Models
{
    public partial class History
    {
        public Guid HistoryId { get; set; }
        public Guid? ProjectId { get; set; }
        public Guid? TaskId { get; set; }
        public string? HistoryDescription { get; set; }
        public DateTime? CreateDate { get; set; }
        public long? CreateBy { get; set; }
    }
}
