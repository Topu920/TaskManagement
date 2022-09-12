using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Requests.CommentInfo;
using Application.Requests.MemberInfo;
using Domain.Entities.Models;
using Infrastructure.Services.TaskApp;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class CommentInfoService:BaseRepository<Comment>, ICommentInfoService
    {
        private  readonly  IMemberInfoService _memberInfoService;
        public CommentInfoService(TaskManagementContext dbContext, IMemberInfoService memberInfoService) : base(dbContext)
        {
            _memberInfoService = memberInfoService;
        }

        public async Task<List<CommentDto>> GetAllCommentListById(string taskId)
        {
            try
            {
               
                var data = await DbContext.Comments.Where(x => x.TaskId == Guid.Parse(taskId)).AsNoTracking()
                    .Select(x=>new CommentDto()
                    {
                        CommentId = x.CommentId,
                        CommentDescription = x.CommentDescription ?? "",
                        ProjectId = x.ProjectId,
                        TaskId = x.TaskId,
                        CreateDate = x.CreateDate,
                        CreateBy = x.CreateBy,
                        CreateByName = _memberInfoService.GetMember(x.CreateBy).Name ?? "",
                        Files =  DbContext.FileUploads.Where(f=>f.CommentId==x.CommentId).
                            Select(f=>new FileDto()
                            {
                                FileId = f.FileId,
                                FileName = f.FileName,
                                FileSize = f.FileSize,
                                FileExtension = f.FileExtension,
                                FileUniqueName = f.FileUniqueName,
                                CommentId = f.CommentId
                            })
                            .ToList()

                    }).OrderByDescending(x=>x.CreateDate)
                    .ToListAsync();
                return data;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

      
    }
}
