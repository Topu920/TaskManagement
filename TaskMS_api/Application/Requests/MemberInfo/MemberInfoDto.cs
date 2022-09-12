namespace Application.Requests.MemberInfo
{
    public class MemberInfoDto
    {
        public long EmpId { get; set; }
        public string EmpCode { get; set; } = null!;
        public string? Name { get; set; }
        public int DesignationId { get; set; }
        public string DesignationName { get; set; } = null!;
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; } = null!;
        public string? EmailAddress { get; set; } = null!;
        public string FullInfoLine { get; set; } = null!;
        public bool IsUserActive { get; set; }
    }
}
