using EFinder.Service.Helpers;
using EFinder.Service.Interfaces;
using EFinder.Service.Services;
using Microsoft.Extensions.DependencyInjection;

namespace EFinder.Infra.IoC.DI;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddDependecyInjection(this IServiceCollection service)
    {
        service.AddScoped<IEmailService, EmailService>();
        service.AddScoped<IFinderService, FinderService>();
        service.AddScoped<IMxService, MxService>();
        service.AddScoped<ITcpClientHelper, TcpClientHelper>();
        return service;
    }
}