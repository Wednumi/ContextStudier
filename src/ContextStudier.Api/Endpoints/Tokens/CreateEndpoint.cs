using Ardalis.ApiEndpoints;
using ContextStudier.Api.Models.AccountModels;
using ContextStudier.Presentation.Core.AccountModels;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ContextStudier.Api.Endpoints.Tokens
{
    public class CreateEndpoint : EndpointBaseAsync
        .WithRequest<CredentialModel>
        .WithActionResult<AuthenticatedUserModel>
    {
        private readonly CredentialMatcher _credentialMatcher;

        private readonly JwtGenerator _jwtGenerator;

        public CreateEndpoint(CredentialMatcher credentialMatcher, JwtGenerator jwtGenerator)
        {
            _credentialMatcher = credentialMatcher;
            _jwtGenerator = jwtGenerator;
        }

        [HttpPost("api/token")]
        [SwaggerOperation(Tags = new[] { "Tokens" })]
        public override async Task<ActionResult<AuthenticatedUserModel>> HandleAsync(
            CredentialModel credential,
            CancellationToken cancellationToken = default)
        {
            try
            {
                return await Task.Run(async () =>
                {
                    var user = await _credentialMatcher.GetValidUserAsync(credential);
                    return await _jwtGenerator.GenerateAsync(user);
                }, cancellationToken);
                
            }
            catch (UserNotFoundException)
            {
                return BadRequest("Wrong login");
            }
            catch (WrongPasswordException)
            {
                return BadRequest($"Wrong password");
            }
        }
    }
}
