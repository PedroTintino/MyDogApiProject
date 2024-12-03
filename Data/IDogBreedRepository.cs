
namespace MyDogApiProject.Data;
public interface IDogBreedRepository
{
    Task<List<string>> GetAllBreedsAsync();
    Task<bool> BreedExistsAsync(string breedName);
    
    //"task" equivalente a void
    Task AddBreedAsync(string breedName);
}
