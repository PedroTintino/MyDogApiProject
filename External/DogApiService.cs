using System.Text.Json;

namespace MyDogApiProject.External;

public class DogApiService : IDogApiService
{
    private readonly HttpClient _httpClient;

    public DogApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public Task<string> TesteTexto()
    {
        return Task.FromResult("Estou rodando até aqui...");
    }

    public async Task<List<DogBreed>> GetAllBreeds()
    {
        try
        {
            var response = await _httpClient.GetAsync("breeds");

            var responseContent = await response.Content.ReadAsStringAsync();

            //método de desserialização baseado na documentação através do namespace System.Text.Json
            var breeds = JsonSerializer.Deserialize<List<DogBreed>>(responseContent, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            //Como lidar com a possibilidade de um retorno null?
            return breeds;

        }
        catch (Exception ex)
        {
            throw new Exception("Erro ao obter raças: " + ex.Message);
        }
    }


}

public record DogBreed(int Id, string Name);
