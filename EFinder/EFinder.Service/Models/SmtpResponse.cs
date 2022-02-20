namespace EFinder.Service.Models;

public class SmtpResponse
{
    public SmtpResponse(int status, string message)
    {
        Status = status;
        Message = message;
    }

    public SmtpResponse(string fullMessage)
    {
        Status = int.Parse(fullMessage[..3]);
        Message = fullMessage;
    }

    public int Status { get; set; }
    public string Message { get; set; }
}