namespace EFinder.Service.Models;

public class FinderModel
{
    public List<string> Emails { get; set; } = new();
    public List<string> MailExchangeServers { get; set; } = new();
}