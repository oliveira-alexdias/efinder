using EFinder.Service.Models;

namespace EFinder.Service.Interfaces;

public interface ISmtpService
{
    Task<SmtpResponseModel> RunEmailCheckCommands(string server, string email);
}