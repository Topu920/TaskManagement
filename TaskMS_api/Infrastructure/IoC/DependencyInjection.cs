using Application.Common.Interfaces;
using Application.Requests.CommentInfo;
using Application.Requests.GroupInfo;
using Application.Requests.HistoryInfo;
using Application.Requests.MemberInfo;
using Application.Requests.ProjectInfo;
using Application.Requests.StatuesInfo;
using Application.Requests.TaskInfo;
using Application.Requests.UserInfos;
using Domain.Entities.Models;
using Domain.Entities.Models.Auth;
using Domain.Entities.Models.HrmsModels;
using Infrastructure.Filter;
using Infrastructure.Services;
using Infrastructure.Services.TaskApp;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.IoC
{
    public static class DependencyInjection
    {

        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<TaskManagementContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")).EnableSensitiveDataLogging());

            services.AddDbContext<CoreERPContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("hrmsConnectionsString")).EnableSensitiveDataLogging());
            services.AddDbContext<ERPUSERDBContext>(options =>
                           options.UseSqlServer(configuration.GetConnectionString("UserConnectionsString")).EnableSensitiveDataLogging());

            #region comment


            //IdentityBuilder builder = services.AddIdentityCore<User>(opt =>
            //{
            //    opt.Password.RequireDigit = false;
            //    opt.Password.RequiredLength = 4;
            //    opt.Password.RequireNonAlphanumeric = false;
            //    opt.Password.RequireUppercase = false;
            //    opt.Password.RequireLowercase = false;
            //});

            //builder = new IdentityBuilder(builder.UserType, typeof(Role), builder.Services);
            //builder.AddEntityFrameworkStores<ApplicationDbContext>();
            //builder.AddRoleValidator<RoleValidator<Role>>();
            //builder.AddRoleManager<RoleManager<Role>>();
            //builder.AddSignInManager<SignInManager<User>>();

            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //    .AddJwtBearer(options =>
            //    {
            //        options.TokenValidationParameters = new TokenValidationParameters
            //        {
            //            ValidateIssuerSigningKey = true,
            //            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII
            //                .GetBytes(configuration.GetSection("AppSettings:Token").Value)),
            //            ValidateIssuer = false,
            //            ValidateAudience = false,
            //        };
            //    });

            //services.AddMvc(options =>
            //{
            //    var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();

            //    options.Filters.Add(new AuthorizeFilter(policy));
            //});
            //services.AddTransient<IIdentityService, IdentityService>();
            //services.AddTransient<ILogToDatabaseService, LogToDatabaseService>();
            /*HttpClient*/
            //services.AddHttpClient<IIdentityService, IdentityService>();
            #endregion


            services.AddScoped<AuthorizationFilter>();
            services.AddTransient<IDateTime, DateTimeService>();



            services.AddScoped<IGroupInfoService, GroupInfoService>();
            services.AddScoped<ITaskInfoService, TaskInfoService>();
            services.AddScoped<IProjectInfoService, ProjectInfoService>();
            services.AddScoped<IStatuesService, StatuesService>();
            services.AddScoped<IMemberInfoService, MemberInfoService>();
            services.AddScoped<IUserInfoService, UserInfoService>();
            services.AddScoped<IHistoryInfoService, HistoryInfoService>();
            services.AddScoped<ICommentInfoService, CommentInfoService>();
            // services.AddTransient<IUserInfoService, UserInfoService>();




            return services;
        }
    }
}
