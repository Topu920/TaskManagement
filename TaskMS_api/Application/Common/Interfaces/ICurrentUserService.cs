namespace Application.Common.Interfaces
{
    public interface ICurrentUserService
    {
        int UserId { get; }
        string UserName { get; }
        string EmployeeId { get; }
        int HeadOfficeId { get; }
        int BranchOfficeId { get; }

    }
}
