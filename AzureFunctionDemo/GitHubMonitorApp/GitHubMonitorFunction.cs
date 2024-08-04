using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

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
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequest req)
        {
            _logger.LogInformation("Our GitHub monitor processed an action");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);

            _logger.LogInformation(requestBody);

            return new OkResult();
        }
    }
}