using System;
using System.Collections.Generic;

namespace Domain.Entities.Models
{
    public partial class FileUpload
    {
        public Guid FileId { get; set; }
        public string? FileName { get; set; }
        public int? FileSize { get; set; }
        public string? FileExtension { get; set; }
        public string? FileUniqueName { get; set; }
        public Guid? CommentId { get; set; }

        public virtual Comment? Comment { get; set; }
    }
}
