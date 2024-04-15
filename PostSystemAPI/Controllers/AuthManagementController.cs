using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PostSystemAPI.DAL.Models;
using PostSystemAPI.Domain;
using PostSystemAPI.Domain.Configuration;
using PostSystemAPI.Domain.DTO.Request;
using PostSystemAPI.Domain.DTO.Response;
using PostSystemAPI.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PostSystemAPI.WebApi.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthManagementController: ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly JwtConfig _jwtConfig;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public AuthManagementController(UserManager<User> userManager, IOptionsMonitor<JwtConfig> optionsMonitor, IMapper mapper, IConfiguration configuration)
        {
            _userManager = userManager;
            _jwtConfig = optionsMonitor.CurrentValue;
            _mapper = mapper;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationRequestDto user)
        {
            if(ModelState.IsValid)
            {
                var existingUser = await _userManager.FindByEmailAsync(user.Email);
                if (existingUser != null)
                {
                    return BadRequest(new RegistrationResponse()
                    {
                        Result = false,
                        Errors = new List<string>()
                        {
                            "Email already exists"
                        }
                    });
                }

                var newUser = new User() { Email=user.Email, UserName=$"{user.FirstName}_{user.LastName}", FirstName = user.FirstName, LastName = user.LastName, Balance = 20000};
                var IsCreated = await _userManager.CreateAsync(newUser, user.Password);
                if(IsCreated.Succeeded)
                {
                    await _userManager.AddToRoleAsync(newUser, "Viewer");
                    var jwtToken = GenerateJwtToken(newUser);
                    return Ok(new RegistrationResponse()
                    {
                        Result = true,
                        Token = await jwtToken
                    });
                }

                return new JsonResult(new RegistrationResponse()
                {
                    Result = false,
                    Errors = IsCreated.Errors.Select(x => x.Description).ToList()
                })
                { StatusCode = 500 };
            }

            return BadRequest(new RegistrationResponse()
            {
                Result = false,
                Errors = new List<string>(){
                                            "Invalid payload"
                                        }
            });
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest user)
        {

            if (ModelState.IsValid)
            {
                var existingUser = await _userManager.FindByEmailAsync(user.Email);

                if(existingUser == null)
                {
                    return BadRequest(new RegistrationResponse()
                    {
                        Result = false,
                        Errors = new List<string>()
                        {
                            "Invalid authentication request"
                        }
                    });
                }

                var IsCorrect = await _userManager.CheckPasswordAsync(existingUser, user.Password);

                if(IsCorrect)
                {
                    var jwtToken = GenerateJwtToken(existingUser);
                    return Ok(new RegistrationResponse()
                    {
                        Result = true,
                        Token = await jwtToken
                    });
                }
                else
                {
                    return BadRequest(new RegistrationResponse()
                    {
                        Result = false,
                        Errors = new List<string>()
                        {
                            "Invalid authentication request"
                        }
                    });
                }

            }

            return BadRequest(new RegistrationResponse()
            {
                Result = false,
                Errors = new List<string>()
                        {
                            "Invalid authentication request"
                        }
            });
        }

        [HttpGet("privacy")]
        [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme, Roles ="Administrator")]
        public async Task<IActionResult> eqPrivacy()
        {
            var claims = User.Claims
                .Select(x => new { x.Type, x.Value })
                .ToList();

            return Ok(claims);
        }

       
        [HttpGet("get-user-info")]
        [Authorize(Policy = "WorkerPolicy", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<UserInfoViewModel>> GetUserInfo()
        {
            var currentUserId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var user = await _userManager.FindByIdAsync(currentUserId);
            if (user == null)
                return BadRequest("Failed to get current user data");

            var userInfo = _mapper.Map<UserInfoViewModel>(user);
            userInfo.Role = User.Claims.FirstOrDefault(u => u.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role").Value;

            return Ok(userInfo);
        }

        [HttpGet("get-receiver-info")]
        public async Task<ActionResult<ReceiverViewModel>> GetReceiverInfo(string firstName, string lastName, string email)
        {
            var user = _userManager.Users.FirstOrDefault(u => u.FirstName == firstName && u.LastName == lastName && u.Email == email);

            if (user == null)
                return BadRequest("Failed to get receiver");

            var userView = _mapper.Map<ReceiverViewModel>(user);
            if(userView != null)
                return Ok(userView);
            return BadRequest("Cannot find receiver");

        }

        private async Task<string> GenerateJwtToken(User user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            var role =roles.FirstOrDefault();

            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", user.Id),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim("role", role)
                }),
                Expires = DateTime.UtcNow.AddHours(6),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = jwtTokenHandler.WriteToken(token);

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, role)
            };
            var jwt = new JwtSecurityToken(
                issuer: _configuration["JwtConfig:validIssuer"],
                audience: _configuration["JwtConfig:validAudience"],
                claims: claims,
                expires: DateTime.UtcNow.Add(TimeSpan.FromHours(2)),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JwtConfig:Secret"])), SecurityAlgorithms.HmacSha256));

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
        
        [HttpPost("register-driver")]
        public async Task<IActionResult> RegisterDriver([FromBody] UserRegistrationRequestDto user)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await _userManager.FindByEmailAsync(user.Email);
                if (existingUser != null)
                {
                    return BadRequest(new RegistrationResponse()
                    {
                        Result = false,
                        Errors = new List<string>()
                        {
                            "Email already exists"
                        }
                    });
                }

                var newUser = new User() { Email = user.Email, UserName = $"{user.FirstName}_{user.LastName}", FirstName = user.FirstName, LastName = user.LastName, Balance = 20000 };
                var IsCreated = await _userManager.CreateAsync(newUser, user.Password);
                if (IsCreated.Succeeded)
                {
                    await _userManager.AddToRoleAsync(newUser, "Driver");
                    var jwtToken = GenerateJwtToken(newUser);
                    return Ok(new RegistrationResponse()
                    {
                        Result = true,
                        Token = await jwtToken
                    });
                }

                return new JsonResult(new RegistrationResponse()
                {
                    Result = false,
                    Errors = IsCreated.Errors.Select(x => x.Description).ToList()
                })
                { StatusCode = 500 };
            }

            return BadRequest(new RegistrationResponse()
            {
                Result = false,
                Errors = new List<string>(){
                                            "Invalid payload"
                                        }
            });
        }
    }
}
