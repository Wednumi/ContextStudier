using AutoMapper;
using ContextStudier.Api.Models;
using ContextStudier.Core.Entitites;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ContextStudier.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]    
    public class UserController : ControllerBase
    {
        private readonly UserManager<User> _userManager;

        private readonly IMapper _mapper;

        public UserController(UserManager<User> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(UserRegistrationModel userRegistration)
        {
            if(ModelState.IsValid is false)
            {
                return BadRequest(ModelState);
            }
            var user = _mapper.Map<User>(userRegistration);
            var result = await _userManager.CreateAsync(user, userRegistration.Password);
            if (result.Succeeded)
            {
                return Ok();
            }
            return BadRequest(result.Errors.FirstOrDefault()?.Description);
        }

    }
}
