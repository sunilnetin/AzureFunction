using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Company.Function;

public class HttpTrigger1
{
    private readonly ILogger<HttpTrigger1> _logger;

    public HttpTrigger1(ILogger<HttpTrigger1> logger)
    {
        _logger = logger;
    }

    [Function("HttpTrigger1")]
    public IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest req)
    {
        /*
        _logger.LogInformation("C# HTTP trigger function processed a request.");
        return new OkObjectResult("Welcome to Azure Functions!");
        */

        _logger.LogInformation("Sunil- C# HTTP trigger function processed a request.");

        string? name = req.Query["name"];

        if (string.IsNullOrEmpty(name))
        {
            return new BadRequestObjectResult("Please pass a name on the query string or in the request body");
        }

        return new OkObjectResult($"Hello, {name}");
    }
}