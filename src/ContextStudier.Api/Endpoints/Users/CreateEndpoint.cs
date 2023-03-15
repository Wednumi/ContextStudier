using Ardalis.ApiEndpoints;
using AutoMapper;
using ContextStudier.Api.Models.AccountModels;
using ContextStudier.Core.Entitites;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ContextStudier.Api.Endpoints.Users
{
    public class CreateEndpoint : EndpointBaseAsync
        .WithRequest<UserRegistrationModel>
        .WithActionResult
    {
        private readonly UserManager<User> _userManager;

        private readonly IMapper _mapper;

        public CreateEndpoint(UserManager<User> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        [HttpPost("api/user/create")]
        [SwaggerOperation(Tags = new[] { "Users" })]
        public override async Task<ActionResult> HandleAsync(UserRegistrationModel userRegistration, 
            CancellationToken cancellationToken = default)
        {
            return await Task.Run<ActionResult>(async () =>
            {
                var user = _mapper.Map<User>(userRegistration);

                var result = await _userManager.CreateAsync(user, userRegistration.Password);

                if (result.Succeeded)
                {
                    return Ok();
                }

                return BadRequest(result.Errors.Select(e => e.Description));
            }, cancellationToken);
        }
    }
}
