using ARSoft.Tools.Net.Dns;
using EFinder.Service.Exceptions;
using EFinder.Service.Interfaces;

namespace EFinder.Service.Services;

public class MailExchangeService : IMailExchangeService
{
    public List<string> GetMailExchangeServerBasedOnDomain(string domain)
    {
        var resolver = new DnsStubResolver();
        var mxServers = resolver.Resolve<MxRecord>(domain, RecordType.Mx).Select(r => r.ExchangeDomainName.ToString()).ToList();
        if (!mxServers.Any()) throw new MailExchangeServerNotFoundException(domain);
        return mxServers;
    }
}