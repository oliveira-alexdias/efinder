using EFinder.Service.Factories;
using EFinder.Service.Interfaces;
using EFinder.Service.Models;

namespace EFinder.Service.Services;

public class FinderService : IFinderService
{
    private readonly IEmailService _emailService;
    private readonly IMailExchangeService _mailExchangeService;

    public FinderService(IEmailService emailService, IMailExchangeService mailExchangeService)
    {
        _emailService = emailService;
        _mailExchangeService = mailExchangeService;
    }

    public async Task<FinderModel> FindValidEmail(string firstName, string lastName, string domain)
    {
        var listOfValidEmails = new List<string>();
        var listOfAllEmails = EmailListFactory.Create(firstName, lastName, domain);

        var mailServer = _mailExchangeService.GetMailExchangeServerBasedOnDomain(domain);

        foreach (var email in listOfAllEmails)
        {
            var emailIsValid = await _emailService.EmailIsValid(email, mailServer.First());
            if (!emailIsValid) continue;
            listOfValidEmails.Add(email);
            break;
        }

        return new FinderModel
        {
            Emails = listOfValidEmails,
            MailExchangeServers = mailServer
        };
    }
}