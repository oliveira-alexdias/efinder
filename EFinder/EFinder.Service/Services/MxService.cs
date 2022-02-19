using ARSoft.Tools.Net.Dns;
using EFinder.Service.Exceptions;
using EFinder.Service.Interfaces;

namespace EFinder.Service.Services;

public class MxService : IMxService
{
    public string GetMailServerBasedOnDomain(string domain)
    {
        var resolver = new DnsStubResolver();
        var firstServer = resolver.Resolve<MxRecord>(domain, RecordType.Mx).FirstOrDefault();
        if (firstServer is null) throw new MailServerNotFoundException(domain);
        return firstServer.ExchangeDomainName.ToString();
    }
}