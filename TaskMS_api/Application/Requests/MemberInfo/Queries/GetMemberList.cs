using MediatR;

namespace Application.Requests.MemberInfo.Queries
{
    public class GetMemberList : IRequest<List<MemberInfoDto>>
    {
        public string SearchKey { get; init; } = null!;
    }

    public class GetMemberListHandler : IRequestHandler<GetMemberList, List<MemberInfoDto>>
    {
        private readonly IMemberInfoService _memberInfoService;

        public GetMemberListHandler(IMemberInfoService memberInfoService)
        {
            _memberInfoService = memberInfoService;
        }

        public async Task<List<MemberInfoDto>> Handle(GetMemberList request, CancellationToken cancellationToken)
        {
            var data = await _memberInfoService.GetMemberList(request.SearchKey);
            return data;
        }
    }
}
