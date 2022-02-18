namespace EFinder.API.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureServices(this IServiceCollection service)
    {
        service.AddControllers();
        service.AddEndpointsApiExplorer();
        service.AddSwaggerGen();
        return service;
    }
}