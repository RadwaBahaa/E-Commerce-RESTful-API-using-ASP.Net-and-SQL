using DTOs.DTOs.Roles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models.Models;

namespace Presentation.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class RolesController : ControllerBase
    {
        public RoleManager<IdentityRole> roleManager { get; set; }
        public UserManager<User> userManager { get; set; }
        public RolesController(RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        // _____________________________________ Get All Roles _____________________________________
        [HttpGet]
        public async Task<IActionResult> GetAllRoles()
        {
            var roles = roleManager.Roles.ToList();
            return Ok(roles);
        }

        // _____________________________________ Add a new Role _____________________________________
        [HttpPost]
        public async Task<IActionResult> CreateRole(AddNewRoleDTO roleDTO)
        {
            roleDTO.RoleName = char.ToUpper(roleDTO.RoleName[0]) + roleDTO.RoleName.Substring(1).ToLower();
            var getRole = await roleManager.FindByNameAsync(roleDTO.RoleName);
            if (getRole == null)
            {
                IdentityRole newRole = new IdentityRole()
                {
                    Name = roleDTO.RoleName
                };
                await roleManager.CreateAsync(newRole);
                return Ok(newRole);
            }
            else
                return BadRequest("This role already exists !.... ");
        }

        // _____________________________________ Add Role to a user _____________________________________
        [HttpPost]
        public async Task<IActionResult> AddRoleToUser(AddRoleToUser roleDTO)
        {
            roleDTO.RoleName = char.ToUpper(roleDTO.RoleName[0]) + roleDTO.RoleName.Substring(1).ToLower();
            var user = await userManager.FindByNameAsync(roleDTO.UserName);
            if (user == null)
                return BadRequest("There is no user with this name !....");
            else
            {
                var userRoles = await userManager.GetRolesAsync(user);
                foreach (var role in userRoles)
                {
                    if (role.ToLower() == roleDTO.RoleName.ToLower())
                        return BadRequest($"The user '{user.UserName}' already has this role !....");
                }
                await userManager.AddToRoleAsync(user, roleDTO.RoleName);
                return Ok($"The role '{roleDTO.RoleName}' was added to the user '{user.UserName}' successfully !....");
            }
        }
    }
}
