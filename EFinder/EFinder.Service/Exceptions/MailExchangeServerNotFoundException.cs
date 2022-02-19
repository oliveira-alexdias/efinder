namespace EFinder.Service.Exceptions;

public class MailExchangeServerNotFoundException : Exception
{
    public MailExchangeServerNotFoundException(string domain) 
    : base($"Mail Exchange Server for domain '{domain}' was not found.") { }
}