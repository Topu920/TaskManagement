using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Requests.CommentInfo
{
    public class CommentDto
    {
        public Guid CommentId { get; set; }
        public string? CommentDescription { get; set; }
        public Guid? ProjectId { get; set; }
        public Guid? TaskId { get; set; }
        public DateTime? CreateDate { get; set; }
        public long? CreateBy { get; set; }
        public string? CreateByName { get; set; }
        public List<FileDto> Files { get; set; } = null!;
    }

    public class FileDto
    {
        public Guid FileId { get; set; }
        public string? FileName { get; set; }
        public int? FileSize { get; set; }
        public string? FileExtension { get; set; }
        public string? FileUniqueName { get; set; }
        public Guid? CommentId { get; set; }
    }
}
