namespace EFinder.Service.Exceptions;

public class MailServerNotFoundException : Exception
{
    public MailServerNotFoundException(string domain) 
    : base($"Mail Server for domain '{domain}' was not found.") { }
}