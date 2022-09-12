using Application.Requests.GroupInfo;
using Application.Requests.MemberInfo;
using Domain.Entities.Models;
using Domain.Entities.Models.HrmsModels;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.TaskApp
{
    public class GroupInfoService : BaseRepository<GroupMember>, IGroupInfoService
    {
        private readonly CoreERPContext _coreErpContext;

        public GroupInfoService(TaskManagementContext dbContext, CoreERPContext coreErpContext) : base(dbContext)
        {
            _coreErpContext = coreErpContext;
        }

        public GroupMemberDetail CheckExist(Guid requestGroupId, long item)
        {
            return DbContext.GroupMemberDetails.FirstOrDefault(f => f.GroupId == requestGroupId && f.MemberUserId == item) ?? throw new InvalidOperationException();
        }

        public async Task<List<GroupInfoDto>> GetAllGroupList()
        {
            #region comment
            //var groupmember = await _dbContext.GroupMemberDetails.ToListAsync();
            //var memberInfo = _coreErpContext.HumanResourceEmployeeBasics.Where(e=>e.EmpStatusId==1).Select(x => new MemberInfoDto()
            //{
            //    EmpId = x.EmpId,
            //    Name = x.Name,
            //    DesignationName = _coreErpContext.CommonDesignations.Where(ds=>ds.DesignationId== x.DesignationId).FirstOrDefault().DesignationName,
            //    DepartmentName = _coreErpContext.CommonDepartments.Where(ds=>ds.DepartmentId== x.DepartmentId).FirstOrDefault().DepartmentName,

            //}).ToList();
            #endregion

            var data = await DbContext.GroupMembers
                .Select(x => new GroupInfoDto()
                {
                    GroupId = x.GroupId,
                    GroupName = x.GroupName,
                    CreateBy = x.CreateBy,
                    IsPrivate = x.IsPrivate,
                    GroupMemberDetails = new List<GroupDetailsDto>()
                }).ToListAsync();
            return data;
        }

        public async Task<List<GroupDetailsDto>> GetAllGroupMemberList(long? userId)
        {
            var memberInfo = await _coreErpContext.HumanResourceEmployeeBasics.Where(e => e.EmpStatusId == 1)
                .Select(x => new MemberInfoDto()
                {
                    EmpId = x.EmpId,
                    Name = x.Name,
                    DesignationName = _coreErpContext.CommonDesignations.Single(ds => ds.DesignationId == x.DesignationId).DesignationName,
                    DepartmentName = _coreErpContext.CommonDepartments.Single(ds => ds.DepartmentId == x.DepartmentId).DepartmentName,

                }).ToListAsync();
            var data = await DbContext.GroupMemberDetails
                .Select(x => new GroupDetailsDto()
                {
                    GroupMemberDetailsId = x.GroupMemberDetailsId,
                    GroupId = x.GroupId,
                    GroupName = DbContext.GroupMembers.Single(g => g.GroupId == x.GroupId).GroupName,
                    IsPrivate = DbContext.GroupMembers.Single(g => g.GroupId == x.GroupId).IsPrivate,
                    CreateBy = DbContext.GroupMembers.Single(g => g.GroupId == x.GroupId).CreateBy,
                    MemberUserId = x.MemberUserId,
                    MemberName = x.MemberUserId != null ? GetEmployeeInfo(memberInfo, (long)x.MemberUserId).Name : null,
                    DepartmentName = x.MemberUserId != null ? GetEmployeeInfo(memberInfo, (long)x.MemberUserId).DepartmentName : null,
                    DesignationName = x.MemberUserId != null ? GetEmployeeInfo(memberInfo, (long)x.MemberUserId).DesignationName : null,


                }).Where(s => s.IsPrivate == "N" || s.CreateBy == userId).ToListAsync();
            return data;
        }



        private static MemberInfoDto GetEmployeeInfo(IEnumerable<MemberInfoDto> memberInfo, long memberUserId)
        {
            return memberInfo.First(x => x.EmpId == memberUserId);
        }
    }
}
