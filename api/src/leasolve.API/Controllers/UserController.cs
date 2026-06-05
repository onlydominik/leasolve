using leasolve.API.Extensions;
using leasolve.API.Results;
using leasolve.Application.Features.Users.Users.Commands.CreateOwner;
using leasolve.Domain.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace leasolve.API.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly ISender _sender;

    public UserController(ISender sender) =>
        _sender = sender;
    
    [HttpPost]
    public async Task<ActionResult<long>> PostOwnerUser(CreateOwnerCommand command)
    {
        Result<long> result = await _sender.Send(command);

        return await this.MatchAsync(result, ApiResults.Problem);
    }  
}