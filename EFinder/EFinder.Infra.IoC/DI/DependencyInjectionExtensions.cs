using EFinder.Infra.File.Core;
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
        service.AddScoped<IMailExchangeService, MailExchangeService>();
        service.AddScoped<ISmtpService, SmtpService>();
        service.AddScoped<IFiles, Files>();
        return service;
    }
}