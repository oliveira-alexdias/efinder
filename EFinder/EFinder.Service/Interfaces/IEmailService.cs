namespace EFinder.Service.Interfaces;

public interface IEmailService
{
    Task<bool> EmailIsValid(string email, string mailServer);
    List<string> EmailListBuilder(string firstName, string lastName, string domain);
}