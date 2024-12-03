using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http;
using Microsoft.Extensions.Hosting;
using MyDogApiProject.External;
using MyDogApiProject.Data;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices(s =>
    {
        s.AddSingleton<IDogApiService, DogApiService>();
        s.AddSingleton<IDogBreedRepository, DogBreedRepository>();
        s.AddHttpClient<IDogApiService, DogApiService>(client =>
        {
            client.BaseAddress = new Uri("https://api.thedogapi.com/v1/");
        });
    })
    .Build();

host.Run();
