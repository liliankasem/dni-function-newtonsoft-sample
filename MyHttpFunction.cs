using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyFunctionApp
{
    public class MyHttpFunction
    {
        private readonly ILogger<MyHttpFunction> _logger;

        public MyHttpFunction(ILogger<MyHttpFunction> logger)
        {
            _logger = logger;
        }

        [Function(nameof(MyHttpFunction))]
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            return new OkObjectResult(new { Name = "Error" });
        }
    }
}