using System;
using System.Collections.Generic;

namespace Domain.Entities.Models
{
    public partial class Comment
    {
        public Comment()
        {
            FileUploads = new HashSet<FileUpload>();
        }

        public Guid CommentId { get; set; }
        public string? CommentDescription { get; set; }
        public Guid? ProjectId { get; set; }
        public Guid? TaskId { get; set; }
        public DateTime? CreateDate { get; set; }
        public long? CreateBy { get; set; }

        public virtual ICollection<FileUpload> FileUploads { get; set; }
    }
}
