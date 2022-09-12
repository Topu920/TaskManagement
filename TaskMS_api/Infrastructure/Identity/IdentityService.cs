//using System;
//using System.Collections.Generic;
//using System.IdentityModel.Tokens.Jwt;
//using System.Linq;
//using System.Net.Http;
//using System.Security.Claims;
//using System.Text;
//using System.Threading.Tasks;
//using Application.Common.Exceptions;
//using  Application.Common.Interfaces;
//using  Application.Common.Models;
////using  Application.Auth;
//using  Domain.Enums;
//using Erp.Infrastructure.Persistence;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Configuration;
//using Microsoft.IdentityModel.Tokens;

//namespace Erp.Infrastructure.Identity
//{
//    public class IdentityService : IIdentityService
//    {
//        private readonly ApplicationDbContext _context;
//        private readonly IConfiguration _config;
//        private readonly UserManager<User> _userManager;
//        private readonly SignInManager<User> _signInManager;
//        private readonly IHelperService _helperService;


//        private const string BaseUrl = "http://192.168.1.86/api/AnotherApps/GetEmployeeChecked?id=1&name=sng&employeeID=";

//        private readonly HttpClient _client;


//        public IdentityService(ApplicationDbContext context,
//            IConfiguration config,
//            UserManager<User> userManager,
//            SignInManager<User> signInManager, 
//            IHelperService helperService,
//            HttpClient client
//        )
//        {
//            _context = context;
//            _config = config;
//            _userManager = userManager;
//            _signInManager = signInManager;
//            _client = client;
//            _helperService = helperService;
//        }



//        public async Task<object> Login(UserForLoginDto userForLogin)
//        {
//            if (!string.IsNullOrWhiteSpace(userForLogin.UserName) && !string.IsNullOrWhiteSpace(userForLogin.Password))
//            {
//                var user = await _context.Users.FirstOrDefaultAsync(c => c.EmployeeId == userForLogin.UserName) ??
//                           (await _userManager.FindByEmailAsync(userForLogin.UserName) ?? await _userManager.FindByNameAsync(userForLogin.UserName));

//                if (user == null)
//                {
//                    throw new UnauthorizedAccessException("User not found! Please register");
//                }

//                var result = await _signInManager.CheckPasswordSignInAsync(user, userForLogin.Password, false);

//                if (result.Succeeded)
//                {
//                    UserForReturnDto appUser = new UserForReturnDto
//                    {
//                        Id = user.Id,
//                        UserName = user.UserName,
//                        Email = user.Email,
//                        EmployeeId = user.EmployeeId,
//                        PhoneNumber = user.PhoneNumber,
//                        HeadOfficeId = user.HeadOfficeId,
//                        BranchOfficeId = user.BranchOfficeId,

//                    };

//                    return new
//                    {
//                        token = GenerateJwtToken(user).Result,
//                        user = appUser
//                    };
//                }

//                throw new UnauthorizedAccessException("Invalid username or password");
//            }


//            throw new NotFoundException(nameof(User), userForLogin.UserName);
//        }



//        //public async Task<object> Login(UserForLoginDto userForLogin)
//        //{
//        //    if (!string.IsNullOrWhiteSpace(userForLogin.TokenNumber))
//        //    {
//        //        string decrypt = _encryptDecrypt.DecryptString(userForLogin.TokenNumber);
//        //        string[] words = decrypt.Split('_');
//        //        string empCode = words[0].ToLower();
//        //        decrypt = words[1];
//        //        var userInfo = _context.Users.Where(x => x.EmployeeId.ToLower() == empCode && x.TokenNumber.ToLower() == decrypt.ToLower()).FirstOrDefaultAsync();

//        //        var user = await _context.Users.FirstOrDefaultAsync(c => c.EmployeeId == empCode) ??
//        //                   (await _userManager.FindByEmailAsync(userForLogin.UserName) ?? await _userManager.FindByNameAsync(empCode));

//        //        if (userInfo == null)
//        //        {
//        //            throw new UnauthorizedAccessException("User not found! Please register");
//        //        }

//        //        //var result = await _signInManager.CheckPasswordSignInAsync(user, userForLogin.Password, false);

//        //        if (userInfo!=null)
//        //        {

//        //            UserForReturnDto appUser = new UserForReturnDto
//        //            {
//        //                Id = user.Id,
//        //                UserName = user.UserName,
//        //                Email = user.Email,
//        //                EmployeeId = user.EmployeeId,
//        //                PhoneNumber = user.PhoneNumber,
//        //                HeadOfficeId = user.HeadOfficeId,
//        //                BranchOfficeId = user.BranchOfficeId,

//        //            };

//        //            return new
//        //            {
//        //                token = GenerateJwtToken(user).Result,
//        //                user = appUser
//        //            };
//        //        }

//        //        throw new UnauthorizedAccessException("Invalid username or password");
//        //    }


//        //    throw new NotFoundException(nameof(User), userForLogin.UserName);
//        //}

//        public async Task<(Result Result, int UserId)> Register(UserForRegisterDto userForRegister)
//        {

//            //API Validation
//            if (userForRegister != null)
//            {
//                /*var httpResponse = await _client.GetAsync(
//                    $"{BaseUrl}{userForRegister.EmployeeId}");

//                var responseMsg = await httpResponse.Content.ReadAsStringAsync();

//                if (responseMsg.Contains("true"))
//                {
//                    var checkUser =
//                        await _context.Users.FirstOrDefaultAsync(c => c.EmployeeId == userForRegister.EmployeeId);

//                    if (checkUser != null)
//                        return (Result.Failure(new List<string> { "User Already Exist" }), checkUser.Id);

//                    if (!string.IsNullOrWhiteSpace(userForRegister.EmployeeId))
//                    {
//                        checkUser = await _context.Users.FirstOrDefaultAsync(c =>
//                            c.EmployeeId == userForRegister.EmployeeId);
//                        if (checkUser != null)
//                            return (Result.Failure(new List<string> { "User Already Exist" }), checkUser.Id);
//                    }

//                    //var userInfo =
//                    //    await _context.EmployeeProfiles.FirstOrDefaultAsync(c =>
//                    //        c.EmployeeId == userForRegister.EmployeeId) ?? null;

//                    //if (userInfo != null)
//                    //{
//                        var user = new User
//                    {
//                        UserName = userForRegister.EmployeeId,
//                        EmployeeId = userForRegister.EmployeeId,
//                        HeadOfficeId = 331,
//                        BranchOfficeId = 4
//                    };

//                    var result = await _userManager.CreateAsync(user, userForRegister.Password);

//                    if (result.Succeeded)
//                    {
//                        var userForRole = await _userManager.FindByNameAsync(user.UserName);
//                        await _userManager.AddToRolesAsync(userForRole, new[] { UsersRole.Employee.ToString() });
//                    }

//                    return (result.ToApplicationResult(), user.Id);
//                    //}
//                }*/

//                //normal validation

//                //else
//                //{
//                        if (await _helperService.GetEmployeeIdValidation(userForRegister.EmployeeId))
//                        {
//                            var checkUser =
//                                await _context.Users.FirstOrDefaultAsync(
//                                    c => c.EmployeeId == userForRegister.EmployeeId);

//                            if (checkUser != null)
//                                return (Result.Failure(new List<string> {"User Already Exist"}), checkUser.Id);

//                            if (!string.IsNullOrWhiteSpace(userForRegister.EmployeeId))
//                            {
//                                checkUser = await _context.Users.FirstOrDefaultAsync(c =>
//                                    c.EmployeeId == userForRegister.EmployeeId);
//                                if (checkUser != null)
//                                    return (Result.Failure(new List<string> {"User Already Exist"}), checkUser.Id);
//                            }

//                            var userInfo =
//                                await _context.EmployeeProfiles.FirstOrDefaultAsync(c =>
//                                    c.EmployeeId == userForRegister.EmployeeId) ?? null;

//                            if (userInfo != null)
//                            {
//                                var user = new User
//                                {
//                                    UserName = userForRegister.EmployeeId,
//                                    EmployeeId = userInfo.EmployeeId,
//                                    HeadOfficeId = userInfo.HeadOfficeId,
//                                    BranchOfficeId = userInfo.BranchOfficeId
//                                };

//                                var result = await _userManager.CreateAsync(user, userForRegister.Password);

//                                if (result.Succeeded)
//                                {
//                                    var userForRole = await _userManager.FindByNameAsync(user.UserName);
//                                    await _userManager.AddToRolesAsync(userForRole,
//                                        new[] {UsersRole.Employee.ToString()});
//                                }

//                                return (result.ToApplicationResult(), user.Id);
//                            }

//                        }
//                //}
//            }
//            return (Result.Failure(new List<string> { "Employee Id not found" }),1);
//            //return (Result.Failure(new List<string> { "No User Found" }), checkUser.Id);
//        }


//        public async Task<Result> DeleteUser(int id)
//        {

//            if (id > 0)
//            {

//                var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id && !x.Deleted);
//                user.Deleted = true;
//                await _context.SaveChangesAsync();
//                return (Result.Success("User Deleted Successfully.."));
//            }
//            else
//            {
//                return (Result.Failure(new List<string> { "User Not Found!!" }));
//            }

//        }
//        private async Task<string> GenerateJwtToken(User user)
//        {
//            var claims = new List<Claim>
//            {
//                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
//                new Claim(ClaimTypes.Name, user.UserName),
//                new Claim(ClaimTypes.GroupSid, user.HeadOfficeId.ToString()),
//                new Claim(ClaimTypes.PrimarySid, user.BranchOfficeId.ToString())

//            };

//            if (!string.IsNullOrWhiteSpace(user.EmployeeId))
//            {
//                claims.Add(new Claim(ClaimTypes.SerialNumber, user.EmployeeId));
//            }

//            var roles = await _userManager.GetRolesAsync(user);

//            foreach (var role in roles)
//            {
//                claims.Add(new Claim(ClaimTypes.Role, role));
//            }

//            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));

//            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

//            var tokenDescriptor = new SecurityTokenDescriptor
//            {
//                Subject = new ClaimsIdentity(claims),
//                Expires = DateTime.Now.AddHours(9),
//                SigningCredentials = creds
//            };

//            var tokenHandler = new JwtSecurityTokenHandler();

//            var token = tokenHandler.CreateToken(tokenDescriptor);

//            return tokenHandler.WriteToken(token);
//        }

//    }
//}

////string[] words = tokenNumber.Split('_');
////string empCode = words[0].ToLower();
////tokenNumber = words[1];
////_obj.MgsRead = _obj.Cmn.FindMgsSingle(0);

////var userInfo = _obj.Db.AdminUserDatas.Where(l => l.EmpCode.ToLower() == empCode && l.Active == 1).FirstOrDefault();
////if (userInfo != null && !string.IsNullOrEmpty(userInfo.MacAddress) && userInfo.MacAddress.ToLower() == tokenNumber.ToLower())
////{