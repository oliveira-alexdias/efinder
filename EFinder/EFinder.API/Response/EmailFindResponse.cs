using EFinder.Service.Models;

namespace EFinder.API.Response;

public class EmailFindResponse
{
    public List<string> Emails { get; set; } = new();
    public List<string> MailExchangeServers { get; set; } = new();

    public static explicit operator EmailFindResponse(FinderModel model)
    {
        return new EmailFindResponse
        {
            Emails = model.Emails,
            MailExchangeServers = model.MailExchangeServers
        };
    }
}