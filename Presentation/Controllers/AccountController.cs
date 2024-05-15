﻿using DTOs.DTOs.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Models.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Presentation.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        public UserManager<User> userManager { get; set; }
        public RoleManager<IdentityRole> roleManager { get; set; }
        public IConfiguration configuration { get; set; }
        public AccountController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserDTO registerDTO)
        {
            var findUser = await userManager.FindByEmailAsync(registerDTO.Email);
            if (findUser != null)
                return BadRequest("This User already exists !....");
            else
            {
                User newUser = new User()
                {
                    UserName = char.ToUpper(registerDTO.UserName[0]) + registerDTO.UserName.Substring(1),
                    Email = registerDTO.Email,
                    EmailConfirmed = registerDTO.Email == registerDTO.EmailConfirmed,
                    PhoneNumber = registerDTO.PhoneNumber,
                    Address = registerDTO.Address,
                };
                if (newUser.EmailConfirmed)
                {
                    await userManager.CreateAsync(newUser, registerDTO.Password);

                    var getRole = await roleManager.FindByNameAsync(registerDTO.Role);
                    if (getRole == null)
                        return BadRequest("The role is not exist; please try again....");
                    else
                        await userManager.AddToRoleAsync(newUser, getRole.Name);
                    return Ok($"The User '{newUser.UserName}' signed up successfully.....");
                }
                else
                    return BadRequest("There is a problem in email confirmation; please try again....");
            }
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginUserDTO loginDTO)
        {
            var User = await userManager.FindByNameAsync(loginDTO.Name);
            if (User == null)
                return BadRequest("This user was not found !....");
            else
            {
                if (!await userManager.CheckPasswordAsync(User, loginDTO.Password))
                    return BadRequest("The Password is not correct !....");
                else
                {
                    List<Claim> claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.DenyOnlySid,User.Id)
                    };
                    var UserRoles = await userManager.GetRolesAsync(User);
                    foreach (var role in UserRoles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, role));
                    }
                    var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]));
                    var setToken = new JwtSecurityToken(
                        expires: DateTime.Now.AddHours(1),
                        claims: claims,
                        signingCredentials: new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256),
                        issuer: configuration["JWT:issuer"],
                        audience: configuration["JWT:audience"]);
                    var token = new JwtSecurityTokenHandler().WriteToken(setToken);
                    return Ok("Bearer " + token);
                }
            }
        }
    }
}
