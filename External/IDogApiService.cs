namespace MyDogApiProject.External;

public interface IDogApiService
{
    Task<string> TesteTexto();
    Task<List<DogBreed>> GetAllBreeds();
}
