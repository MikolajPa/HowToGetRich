using EnovationAssignment.Helpers;
using EnovationAssignment.IServices;
using EnovationAssignment.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EnovationAssignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService service)
        {
            _userService = service;
        }
        /// <summary>
        /// Function for login
        /// </summary>
        /// <returns>
        /// </returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [Route("Login")]
        public async Task<ActionResult> GetUser(UserLogin user)
        {
            try
            {
                string Animal = await _userService.GetUserAsync(user);
                return Ok(Animal);
            }
            catch (AppException ex)
            {
                return Forbid(ex.Message);
            }
            
        }
    }
}
