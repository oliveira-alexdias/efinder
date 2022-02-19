namespace EFinder.Service.Interfaces;

public interface IMailExchangeService
{
    List<string> GetMailExchangeServerBasedOnDomain(string domain);
}