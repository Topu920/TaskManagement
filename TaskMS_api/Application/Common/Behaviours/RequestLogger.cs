//using System.Threading;
//using System.Threading.Tasks;
//using Application.Common.Interfaces;
//using Application.Common.Models;
//using MediatR.Pipeline;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Http.Features;
//using Microsoft.Extensions.Logging;
//namespace Application.Common.Behaviours
//{
//    public class RequestLogger<TRequest> : IRequestPreProcessor<TRequest>
//    {
//        private readonly ILogger _logger;
//        private readonly ICurrentUserService _currentUserService;
//        private readonly ILogToDatabaseService _logToDatabaseService;
//        private readonly IHttpContextAccessor _context;

//        public RequestLogger(ILogger<TRequest> logger, ICurrentUserService currentUserService, ILogToDatabaseService logToDatabaseService, IHttpContextAccessor context)
//        {
//            _logger = logger;
//            _currentUserService = currentUserService;
//            _logToDatabaseService = logToDatabaseService;
//            _context = context;
//        }

//        public async Task Process(TRequest request, CancellationToken cancellationToken)
//        {
//            var requestName = typeof(TRequest).Name;
//            var userId = _currentUserService.UserId;
//            var userName = _currentUserService.UserName;

//            var logToDatabase = new RequestLoggerEntity
//            {
//                RequestName = requestName,
//                UserId = userId,
//                UserName = userName,
//                UserIp = UserIPandMac()
//            };

//            await _logToDatabaseService.Save(logToDatabase, cancellationToken);

//            _logger.LogInformation("SaRa Ecommerce Request: {Name} {@UserId} {@UserName} {@Request}",
//                requestName, userId, userName, request);
//        }

//        public string UserIPandMac()
//        {

//            string ip = string.Empty;
//            if (!string.IsNullOrEmpty(_context.HttpContext.Request.Headers["X-Forwarded-For"]))
//            {
//                ip = _context.HttpContext.Request.Headers["X-Forwarded-For"];
//            }
//            else
//            {
//                ip = _context.HttpContext.Request.HttpContext.Features.Get<IHttpConnectionFeature>().RemoteIpAddress.ToString();
//            }
//            return ip;
//        }
//    }
//}
