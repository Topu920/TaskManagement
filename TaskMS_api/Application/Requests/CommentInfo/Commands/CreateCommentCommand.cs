using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Requests.HistoryInfo;
using Application.Requests.MemberInfo;
using AutoMapper;
using Common.Service.Responses;
using Domain.Entities.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Requests.CommentInfo.Commands
{
    public  class CreateCommentCommand:IRequest<CreateCommentResponse>
    {
        public Guid CommentId { get; set; }
        public string? CommentDescription { get; set; }
        public Guid? ProjectId { get; set; }
        public Guid? TaskId { get; set; }
       // public DateTime? CreateDate { get; set; }
        public long? CreateBy { get; set; }
        public virtual ICollection<FileDto>? Files { get; set; }
    }

    public class CreateCommentResponse : BaseResponse
    {

    }

    public class CreateCommentHandler : IRequestHandler<CreateCommentCommand, CreateCommentResponse>
    {
        private readonly ICommentInfoService _commentInfoService;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateCommentHandler> _logger;
        private readonly IHistoryInfoService _historyInfoService;
        private readonly IMemberInfoService _memberInfoService;
        public CreateCommentHandler(ICommentInfoService commentInfoService, IMapper mapper, ILogger<CreateCommentHandler> logger, IHistoryInfoService historyInfoService, IMemberInfoService memberInfoService)
        {
            _commentInfoService = commentInfoService;
            _mapper = mapper;
            _logger = logger;
            _historyInfoService = historyInfoService;
            _memberInfoService = memberInfoService;
        }

        public async Task<CreateCommentResponse> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {
            var response = new CreateCommentResponse();
            try
            {
                var comment = new Comment()
                {
                    CommentId = request.CommentId,
                    CommentDescription = request.CommentDescription,
                    ProjectId = request.ProjectId,
                    TaskId = request.TaskId,
                    CreateDate = DateTime.Now,
                    CreateBy = request.CreateBy,
                    FileUploads = request.Files!=null ? SetFiles(request.Files,request.CommentId):new List<FileUpload>()
                };
              
                if (comment.CommentId == Guid.Empty)
                {

                    comment = await _commentInfoService.AddAsync(comment);
                    response.Message =  "comment Saved Successfully";
                    _logger.LogInformation($"{response.Message =  "Comment is Successfully Created"}");
                   
                }
              
                // var ss = await SaveHistory(task.ProjectId, task.TaskId, task.CreateBy, message);

            }
            catch (Exception e)
            {
                response.Success = false;
                response.Message = e.InnerException == null ? e.Message : e.InnerException.Message;
            }
            return response;
        }

        private static List<FileUpload> SetFiles(IEnumerable<FileDto> requestFiles, Guid requestCommentId)
        {
            return requestFiles.Select(item => new FileUpload()
                {
                    FileId = item.FileId,
                    FileName = item.FileName,
                    FileExtension = item.FileExtension,
                    FileSize = item.FileSize,
                    FileUniqueName = item.FileUniqueName,
                    CommentId = requestCommentId
                })
                .ToList();
        }
        private async Task<string> SaveHistory(Guid projectId, Guid taskId, long? createBy, string message)
        {
            try
            {
                var user = _memberInfoService.GetMember(createBy);
                var history = new History
                {

                    ProjectId = projectId,
                    TaskId = taskId,
                    HistoryDescription = message + user.Name,
                    CreateBy = createBy,
                    CreateDate = DateTime.Now
                };

                history = await _historyInfoService.AddAsync(history);
                return history.HistoryId.ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
