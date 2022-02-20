using EFinder.Service.Models;

namespace EFinder.Service.Interfaces;

public interface ISmtpService
{
    Task<SmtpResponse> RunEmailCheckCommands(string server, string email);
}