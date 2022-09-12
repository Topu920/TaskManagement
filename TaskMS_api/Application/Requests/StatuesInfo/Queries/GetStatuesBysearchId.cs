using AutoMapper;
using MediatR;

namespace Application.Requests.StatuesInfo.Queries
{
    public class GetStatuesBysearchId : IRequest<List<StatuesInfoDto>>
    {
        public int Id { get; set; }
    }

    public class GetStatuesBysearchIdHandler : IRequestHandler<GetStatuesBysearchId, List<StatuesInfoDto>>
    {
        private readonly IStatuesService _statuesService;
        private readonly IMapper _mapper;

        public GetStatuesBysearchIdHandler(IStatuesService statuesService, IMapper mapper)
        {
            _statuesService = statuesService;
            _mapper = mapper;
        }

        public async Task<List<StatuesInfoDto>> Handle(GetStatuesBysearchId request, CancellationToken cancellationToken)
        {
            return _mapper.Map<List<StatuesInfoDto>>(await _statuesService.GetStatuesByFlagNo(request.Id));
        }
    }
}
