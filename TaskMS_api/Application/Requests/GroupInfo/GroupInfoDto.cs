namespace Application.Requests.GroupInfo
{
    public class GroupInfoDto
    {
        public Guid GroupId { get; set; }
        public string GroupName { get; set; } = null!;
        public long? CreateBy { get; set; }
        public string? IsPrivate { get; set; }
        public bool IsUserActive { get; set; }

        public virtual List<GroupDetailsDto> GroupMemberDetails { get; set; } = null!;
    }

    public class GroupDetailsDto
    {
        public Guid GroupMemberDetailsId { get; set; }
        public Guid? GroupId { get; set; }
        public string GroupName { get; set; } = null!;
        public long? MemberUserId { get; set; }
        public string? MemberName { get; set; }
        public string? DesignationName { get; set; }
        public string? DepartmentName { get; set; }
        public string? IsPrivate { get; set; }
        public long? CreateBy { get; set; }

    }
}
