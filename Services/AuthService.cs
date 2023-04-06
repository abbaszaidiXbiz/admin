using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using admin.Dtos;
using admin.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Exception = admin.Dtos.Exception;

namespace admin.Services
{
    public class AuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        public AuthService(UserManager<ApplicationUser> userManager,
                            RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }


        public async Task<ApiResponse> CreateRoleAsync(RoleRequest request)
        {
            var role = new ApplicationRole { Name = request.RoleName };

            await _roleManager.CreateAsync(role);
            return new ApiResponse { Success = true, Message = "Successfully created role." };
        }
        public async Task<ApiResponse> LoginAsync(LoginRequest request)
        {
            
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user is null) return new ApiResponse { Success = false, ResponseCode = HttpStatusCode.BadRequest, Message = $"User with email {request.Email} does not exist!" };

            //check for credentials before sign in ..    
            var validCredentials = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!validCredentials)
            {
                return new ApiResponse
                {
                    Success = false,
                    ResponseCode = HttpStatusCode.BadRequest,
                    Message = "Invalid password!",
                };
            }


            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub,user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),

            };

            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = roles.Select(x => new Claim(ClaimTypes.Role, x));
            claims.AddRange(roleClaims);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ba877439-8a16-441c-8031-abe3f0013872"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddMinutes(10);

            var token = new JwtSecurityToken(
                issuer: "https://localhost:7064",
               audience: "http://localhost:5179",
               claims: claims,
               expires: expires,
               signingCredentials: creds
            );

            return new ApiResponse
            {
                Success = true,
                ResponseCode = HttpStatusCode.OK,
                Message = "Login Successful!.",
                Data = new
                {
                    AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                    UserId = user.Id.ToString(),
                    Email = user.Email,
                    SkyMilesId =  user.SkyMilesId

                },
            };

        }

        public async Task<ApiResponse> RegisterAsync(RegisterRequest request)
        {
            var userExist = await _userManager.FindByEmailAsync(request.Email);


            if (userExist is not null)
            {
                return new ApiResponse
                {
                    Success = false,
                    ResponseCode = HttpStatusCode.BadRequest,
                    Message = "User already exists!",
                };

            }

            userExist = new ApplicationUser
            {
                FullName = request.FullName,
                Email = request.Email,
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                UserName = request.UserName,
                SkyMilesId = Utils.IdGenerator.GenerateID(),
            };

            await _userManager.SetPhoneNumberAsync(userExist,request.PhoneNumber);
            var userCreateResult = await _userManager.CreateAsync(user: userExist, password: request.Password);
            if (!userCreateResult.Succeeded)
            {
                return new ApiResponse
                {
                    Success = false,
                    ResponseCode = HttpStatusCode.BadRequest,
                    Message = "User creation failed!",
                    Exception = new Exception
                    {
                        ExceptionMessage = userCreateResult?.Errors?.First()?.Description,
                        ExceptionType = userCreateResult.Errors.First().Code
                    }
                };
            }
            var addUserToRoleResult = await _userManager.AddToRoleAsync(userExist, "USER");
            if (!addUserToRoleResult.Succeeded)
            {
                return new ApiResponse
                {
                    Success = false,
                    ResponseCode = HttpStatusCode.BadRequest,
                    Message = "Adding user role failed!",
                    Exception = new Exception
                    {
                        ExceptionMessage = addUserToRoleResult?.Errors?.First()?.Description,
                        ExceptionType = addUserToRoleResult.Errors.First().Code
                    }
                };

            }
            
            return new ApiResponse
            {
                Success = true,
                ResponseCode = HttpStatusCode.OK,
                Message = "User Registered Successfully!.",
                Data = new
                {
                    User = userExist
                },
            };

        }
    }
}