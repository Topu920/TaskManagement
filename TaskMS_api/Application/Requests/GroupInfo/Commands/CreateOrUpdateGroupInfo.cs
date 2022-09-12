using Common.Service.Responses;
using Domain.Entities.Models;
using MediatR;

namespace Application.Requests.GroupInfo.Commands
{
    public sealed class CreateOrUpdateGroupInfo : IRequest<CreateOrUpdateGroupInfoResponse>
    {
        public Guid GroupId { get; set; }
        public string Name { get; set; } = null!;
        public string IsPrivate { get; set; } = null!;
        public long? CreateBy { get; set; }
        public ICollection<long> EmpIdListCollection { get; set; } = null!;
    }
    public class CreateOrUpdateGroupInfoResponse : BaseResponse
    {
    }

    public class CreateOrUpdateGroupInfoHandler : IRequestHandler<CreateOrUpdateGroupInfo, CreateOrUpdateGroupInfoResponse>
    {
        private readonly IGroupInfoService _groupService;

        public CreateOrUpdateGroupInfoHandler(IGroupInfoService groupService)
        {
            _groupService = groupService;
        }

        public async Task<CreateOrUpdateGroupInfoResponse> Handle(CreateOrUpdateGroupInfo request, CancellationToken cancellationToken)
        {
            var response = new CreateOrUpdateGroupInfoResponse();
            try
            {
                var groupMember = new GroupMember()
                {
                    GroupId = request.GroupId,
                    GroupName = request.Name,
                    IsPrivate = request.IsPrivate,
                    GroupMemberDetails = SetGroupMember(request.EmpIdListCollection, request.GroupId)
                };

                if (groupMember.GroupId == Guid.Empty)
                {

                    groupMember.CreateBy = request.CreateBy;
                    groupMember.CreateDate = DateTime.Now;
                    await _groupService.AddAsync(groupMember);
                    response.Message = "Saved Successfully!";
                }

            }
            catch (Exception e)
            {
                response.Success = false;
                response.Message = e.InnerException == null ? e.Message : e.InnerException.Message;
                Console.WriteLine(e);
                throw;
            }

            return response;
        }

        private List<GroupMemberDetail> SetGroupMember(IEnumerable<long> requestEmpIdListCollection, Guid requestGroupId)
        {
            var groupMember = new List<GroupMemberDetail>();
            foreach (var item in requestEmpIdListCollection)
            {
                var group = new GroupMemberDetail()
                {
                    GroupId = requestGroupId,
                    MemberUserId = item
                };
                if (requestGroupId != Guid.Empty)
                {
                    var check = _groupService.CheckExist(requestGroupId, item);
                    group.GroupMemberDetailsId = check.GroupMemberDetailsId;
                }
                else
                {
                    group.GroupMemberDetailsId = new Guid();
                }
                groupMember.Add(group);

            }

            return groupMember;
        }
    }
}
