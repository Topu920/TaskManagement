using MediatR;

namespace Application.Requests.GroupInfo.Queries
{
    public record GetAllGroupList(long UserId) : IRequest<List<GroupInfoDto>>
    {
        
    }

    public class GetAllGroupListHandler : IRequestHandler<GetAllGroupList, List<GroupInfoDto>>
    {
        private readonly IGroupInfoService _groupService;

        public GetAllGroupListHandler(IGroupInfoService groupService)
        {
            _groupService = groupService;
        }

        public async Task<List<GroupInfoDto>> Handle(GetAllGroupList request, CancellationToken cancellationToken)
        {
            try
            {
                var list = await _groupService.GetAllGroupList();
                

                return list.Where(x => x.IsPrivate == "N" || x.CreateBy == request.UserId).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }
    }
}
