using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;

namespace Application.Requests.CommentInfo.Queries
{
    public class GetCommentListByTaskId:IRequest<List<CommentDto>>
    {
        public string TaskId { get; set; } = null!;
    }

    public class GetCommentListByTaskIdHandler: IRequestHandler<GetCommentListByTaskId, List<CommentDto>>
    {
        private readonly ICommentInfoService _commentInfoService;
        private readonly IMapper _mapper;

        public GetCommentListByTaskIdHandler(IMapper mapper, ICommentInfoService commentInfoService)
        {
            _mapper = mapper;
            _commentInfoService = commentInfoService;
        }

        public async Task<List<CommentDto>> Handle(GetCommentListByTaskId request, CancellationToken cancellationToken)
        {
            try
            {
                var list = await _commentInfoService.GetAllCommentListById( request.TaskId);

                return _mapper.Map<List<CommentDto>>(list);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
