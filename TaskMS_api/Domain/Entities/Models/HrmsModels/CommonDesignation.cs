using System;
using System.Collections.Generic;

namespace Domain.Entities.Models.HrmsModels
{
    public partial class CommonDesignation
    {
        public int DesignationId { get; set; }
        public string DesignationName { get; set; } = null!;
        public string? DesignationNameBan { get; set; }
        public int? DesGroupId { get; set; }
        public int? GradeId { get; set; }
        /// <summary>
        /// 1=Gadget , 2=Non Gadget
        /// </summary>
        public int? GadgetTypeId { get; set; }
        public int OrderBy { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDelete { get; set; }
        public DateTime? EntryDate { get; set; }
        public int? EntryUserId { get; set; }
        public string? TerminalId { get; set; }
    }
}
