using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace GitHubMonitorApp
{
    public class GitHubMonitorFunction
    {
        private readonly ILogger<GitHubMonitorFunction> _logger;

        public GitHubMonitorFunction(ILogger<GitHubMonitorFunction> logger)
        {
            _logger = logger;
        }

        [Function("GitHubMonitorFunction")]
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req)
        {
            _logger.LogInformation("Our GitHub monitor processed an action");
            return new OkObjectResult("Welcome to Azure Functions!");
        }
    }
}
