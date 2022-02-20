namespace EFinder.Service.Constants;

public static class SmtpConstants
{
    public const int MailExchangeServerPort = 25;
    public const int EmailNotFoundStatusCode = 550;
    public const string Crlf = "\r\n";
    public const string HeloCommand = "HELO";
    public const string MailFromCommand = "MAIL FROM:";
    public const string RcptToCommand = "RCPT TO:";
    public const string QuiteCommand = "QUITE";
}