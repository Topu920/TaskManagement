using AutoMapper;
using Common.Service.Responses;
using MediatR;

namespace Application.Requests.UserInfos.Commands
{
    public class CheckUserInfo : IRequest<UserInfoResponse>
    {
        public string EmpId { get; set; } = null!;
        public string UserPass { get; set; } = null!;
    }
    public class UserInfoResponse : BaseResponse
    {
        public LogInDto LogInDto { get; set; } = null!;
    }

    public class CheckInfoHandler : IRequestHandler<CheckUserInfo, UserInfoResponse>
    {
        private readonly IUserInfoService _userInfoService;
        private readonly IMapper _mapper;

        public CheckInfoHandler(IUserInfoService userInfoService, IMapper mapper)
        {
            _userInfoService = userInfoService;
            _mapper = mapper;
        }

        public async Task<UserInfoResponse> Handle(CheckUserInfo request, CancellationToken cancellationToken)
        {
            UserInfoResponse response = new();
            try
            {
                var check = await _userInfoService.CheckUser(request.EmpId, request.UserPass);
                if (check.IsUserExist)
                {
                    response.Success = true;
                    response.Message = "Log In Successful, Welcome " + check.UserName;
                    response.LogInDto = _mapper.Map<LogInDto>(check);
                }
                else
                {
                    response.Success = false;
                    response.Message = "Log In Failed";
                }
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Message = e.InnerException == null ? e.Message : e.InnerException.Message;
            }
            return response;

        }
    }
}
