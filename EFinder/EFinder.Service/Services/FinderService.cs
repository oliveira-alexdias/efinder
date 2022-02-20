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
        var listOfAllEmails = _emailService.GetAllPossibleEmails(firstName, lastName, domain);
        var mailServers = _mailExchangeService.GetMailExchangeServerBasedOnDomain(domain);

        foreach (var email in listOfAllEmails)
        {
            var emailIsValid = await _emailService.EmailIsValid(email, mailServers.First());
            if (emailIsValid) listOfValidEmails.Add(email);
        }

        return new FinderModel
        {
            Emails = listOfValidEmails,
            MailExchangeServers = mailServers
        };
    }
}