namespace EFinder.Service.Interfaces;

public interface IFinderService
{
    Task<List<string>> FindValidEmail(string firstName, string lastName, string domain);
}