using EFinder.Service.Extensions;
using EFinder.Service.Interfaces;

namespace EFinder.Service.Services;

public class EmailService : IEmailService
{
    private readonly ISmtpService _smtpService;
    private readonly IFiles _files;

    public EmailService(ISmtpService smtpService, IFiles files)
    {
        this._smtpService = smtpService;
        _files = files;
    }

    public async Task<bool> EmailIsValid(string email, string mailServer)
    {
        var response = await _smtpService.RunEmailCheckCommands(mailServer, email);
        return response.Status != Constants.Constants.EmailNotFoundStatusCode;
    }

    public List<string> GetAllPossibleEmails(string firstName, string lastName, string domain)
    {
        var basePath = Directory.GetParent(Directory.GetCurrentDirectory())?.FullName;
        var fullPath = Path.Combine(basePath, Constants.Constants.ResourceFolder, Constants.Constants.EmailAddressPatterns);
        var patternsAvailable = _files.ReadFileAsStringList(fullPath);
        var emails = patternsAvailable.Select(s => s.ReplaceForEmail(firstName, lastName, domain)).ToList();
        return emails;
    }
}