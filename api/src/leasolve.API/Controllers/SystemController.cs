using leasolve.API.Extensions;
using leasolve.API.Results;
using leasolve.Domain.Abstractions;
using leasolve.Domain.Errors;
using Microsoft.AspNetCore.Mvc;

namespace leasolve.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SystemController : ControllerBase
    {
        // TEST
        [HttpGet(Name = "Environment")]
        public Task<ActionResult<string>> GetEnvironment()
        {
            string? environment = Environment.GetEnvironmentVariable("ASPNeETCORE_ENVIRONMENT");
            Result<string> result = string.IsNullOrWhiteSpace(environment)
                ? Result.Failure<string>(SystemError.InvalidEnvironment)
                : Result.Success(environment);

            return this.MatchAsync(result, ApiResults.Problem);
        }  
    }
}