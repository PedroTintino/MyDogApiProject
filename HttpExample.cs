using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using MyDogApiProject.External;
using Microsoft.Azure.Functions.Worker.Http;
using System.Net;
using MyDogApiProject.Data;


namespace MyDogApiProject;

public class MyDogApiProject
{
    private readonly ILogger _logger;
    private readonly IDogApiService _dogApiService;
    private readonly IDogBreedRepository _dogBreedRepository;

    // ! Prestar atenção na acessibilidade definida para meus códigos
    public MyDogApiProject(ILoggerFactory loggerFactory, IDogApiService dogApiService, IDogBreedRepository dogBreedRepository)
    {
        _logger = loggerFactory.CreateLogger<MyDogApiProject>();
        _dogApiService = dogApiService;
        _dogBreedRepository = dogBreedRepository;
        
    }

    [Function("FuncaoTeste")]
    public async Task<HttpResponseData> RunTextoTeste([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData req)
    {
        _logger.LogInformation("Função inicializada...");
        //Não conseguia acessar o método pq não o referenciei na interface
        var responseMessage = await _dogApiService.TesteTexto();

        var response = req.CreateResponse(HttpStatusCode.OK);

        await response.WriteStringAsync(responseMessage);

        return response;
    }
    //Também mudamos a nomenclatura de onde era run
    [Function("GetAllBreeds")]
    public async Task<HttpResponseData> RunGetAllBreeds([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData req)
    {
        _logger.LogInformation("Função inicializada...");
        //Não conseguia acessar o método pq não o referenciei na interface
        var responseMessage = await _dogApiService.GetAllBreeds();

        var response = req.CreateResponse(HttpStatusCode.OK);

        await response.WriteAsJsonAsync(responseMessage);

        return response;
    }

    [Function("SaveBreed")]
    public async Task<HttpResponseData> RunSaveBreed(
    [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = "dogs/{breedName}")] HttpRequestData req,
    string breedName)
    {
        _logger.LogInformation($"Checando se a raça em questão ({breedName}) existe");

        var response = req.CreateResponse(HttpStatusCode.OK);

        if (await _dogBreedRepository.BreedExistsAsync(breedName))
        {
            await response.WriteStringAsync("Raça já registrada");
            return response;
        }

        var breeds = await _dogBreedRepository.GetAllBreedsAsync();

        var breed = breeds.FirstOrDefault(b => b.Equals(breedName, StringComparison.OrdinalIgnoreCase));

        //if (breed == null)
        //{
        //    response = req.CreateResponse(HttpStatusCode.NotFound);
        //    await response.WriteStringAsync("Raça não encontrada!");
        //    return response;
        //}

        await _dogBreedRepository.AddBreedAsync(breedName);
        response = req.CreateResponse(HttpStatusCode.Created);
        await response.WriteStringAsync("Raça registrada com sucesso!");
        return response;
    }
}
