using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using MyDogApiProject.External;
using Microsoft.Azure.Functions.Worker.Http;
using System.Net;


namespace MyDogApiProject;

public class MyDogApiProject
{
    private readonly ILogger _logger;
    private readonly IDogApiService _dogApiService;

    // ! Prestar aten��o na acessibilidade definida para meus c�digos
    public MyDogApiProject(ILoggerFactory loggerFactory, IDogApiService dogApiService)
    {
        _logger = loggerFactory.CreateLogger<MyDogApiProject>();
        _dogApiService = dogApiService;
    }

    [Function("FuncaoTeste")]
    public async Task<HttpResponseData> RunTextoTeste([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData req)
    {
        _logger.LogInformation("Fun��o inicializada...");
        //N�o conseguia acessar o m�todo pq n�o o referenciei na interface
        var responseMessage = await _dogApiService.TesteTexto();

        var response = req.CreateResponse(HttpStatusCode.OK);

        await response.WriteStringAsync(responseMessage);

        return response;
    }
    //Tamb�m mudamos a nomenclatura de onde era run
    [Function("GetAllBreeds")]
    public async Task<HttpResponseData> RunGetAllBreeds([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData req)
    {
        _logger.LogInformation("Fun��o inicializada...");
        //N�o conseguia acessar o m�todo pq n�o o referenciei na interface
        var responseMessage = await _dogApiService.GetAllBreeds();

        var response = req.CreateResponse(HttpStatusCode.OK);

        await response.WriteAsJsonAsync(responseMessage);

        return response;
    }
}
