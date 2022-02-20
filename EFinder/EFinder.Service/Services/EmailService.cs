using EFinder.Service.Interfaces;
using Microsoft.Extensions.Configuration;

namespace EFinder.Service.Services;

public class EmailService : IEmailService
{
    private readonly ISmtpService _smtpService;

    public EmailService(ISmtpService smtpService)
    {
        this._smtpService = smtpService;
    }

    public async Task<bool> EmailIsValid(string email, string mailServer)
    {
        var response = await _smtpService.RunEmailCheckCommands(mailServer, email);
        return response.Status != Constants.Constants.EmailNotFoundStatusCode;
    }

    public List<string> GetAllPossibleEmails(string firstName, string lastName, string domain)
    {
        var path = Path.Combine(Directory.GetCurrentDirectory(), Constants.Constants.EmailAddressPatterns);
        var patternsAvailable = File.ReadAllLines(path).ToList();
        var emails = patternsAvailable.Select(s => s.Replace("{domain}", domain)
                                                    .Replace("{FN}", firstName)
                                                    .Replace("{FI}", firstName[0].ToString())
                                                    .Replace("{LN}", lastName)
                                                    .Replace("{LI}", lastName[0].ToString())).ToList();
        return emails;
    }
}