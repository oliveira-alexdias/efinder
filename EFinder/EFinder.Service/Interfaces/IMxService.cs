namespace EFinder.Service.Interfaces;

public interface IMxService
{
    List<string> GetMailExchangeServerBasedOnDomain(string domain);
}