using Microsoft.AspNetCore.Mvc;

namespace leasolve.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SystemController : ControllerBase
    {
        [HttpGet(Name = "Environment")]
        public string? GetEnvironment()
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            return environment;
        }
    }
}