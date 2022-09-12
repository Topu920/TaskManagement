using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Service.Repositories;
using Domain.Entities.Models;

namespace Application.Requests.CommentInfo
{
    public interface ICommentInfoService : IAsyncRepository<Comment>
    {
        Task<List<CommentDto>> GetAllCommentListById(string taskId);
    }
}
