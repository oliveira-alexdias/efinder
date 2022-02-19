using EFinder.Service.Factories;
using EFinder.Service.Interfaces;

namespace EFinder.Service.Services;

public class FinderService : IFinderService
{
    private readonly IEmailService _emailService;
    private readonly IMxService _mxService;

    public FinderService(IEmailService emailService, IMxService mxService)
    {
        _emailService = emailService;
        _mxService = mxService;
    }

    public async Task<List<string>> FindValidEmail(string firstName, string lastName, string domain)
    {
        var listOfValidEmails = new List<string>();
        var listOfAllEmails = EmailListFactory.Create(firstName, lastName, domain);

        var mailServer = _mxService.GetMailServerBasedOnDomain(domain);

        foreach (var email in listOfAllEmails)
        {
            var isValid = await _emailService.EmailIsValid(email, mailServer);
            if(isValid) listOfValidEmails.Add(email);
        }

        return listOfValidEmails;
    }
}