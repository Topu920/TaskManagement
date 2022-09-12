using MediatR;

namespace Application.Requests.GroupInfo.Queries
{
    public record GetGroupMemberList(long UserId) : IRequest<List<GroupDetailsDto>>
    {
       
    }
    public class GetGroupMemberListHandler : IRequestHandler<GetGroupMemberList, List<GroupDetailsDto>>
    {
        private readonly IGroupInfoService _groupService;

        public GetGroupMemberListHandler(IGroupInfoService groupService)
        {
            _groupService = groupService;
        }

        public async Task<List<GroupDetailsDto>> Handle(GetGroupMemberList request, CancellationToken cancellationToken)
        {
            try
            {
                var list = await _groupService.GetAllGroupMemberList(request.UserId);

                return list;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }
    }
}
