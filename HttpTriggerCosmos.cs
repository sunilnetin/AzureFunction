using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Azure.Cosmos;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Company.Function;

public class HttpTriggerCosmos
{
    private readonly ILogger<HttpTriggerCosmos> _logger;
    private readonly CosmosClient _cosmosClient;
    private readonly CosmosContainer _container;

    public HttpTriggerCosmos(ILogger<HttpTriggerCosmos> logger, IConfiguration config)
    {
        _logger = logger;
        var connectionString = config["CosmosDBConnectionString"];
        var databaseName = config["DatabaseName"];
        var containerName = config["ContainerName"];
        // Replace with your actual connection string and container info
        // _cosmosClient = new CosmosClient("CosmosDBConnectionString");
        // _container = _cosmosClient.GetDatabase("DatabaseName").GetContainer("ContainerName");
         _cosmosClient = new CosmosClient(connectionString);
        _container = _cosmosClient.GetDatabase(databaseName).GetContainer(containerName);
    }

    [Function("HttpTriggerCosmos")]
    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest req)
    {
        string? id = req.Query["id"];
        if (string.IsNullOrEmpty(id))
        {
            return new BadRequestObjectResult("Please pass an id on the query string");
        }

        try
        {
            var response = await _container.ReadItemAsync<dynamic>(id, new PartitionKey(id));
            return new OkObjectResult(response.Value);
        }
        catch (CosmosException ex)
        {
            _logger.LogError($"CosmosDB error: {ex.Message}");
            return new NotFoundObjectResult("Item not found");
        }
    }
}