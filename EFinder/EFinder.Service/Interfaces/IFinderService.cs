using EFinder.Service.Models;

namespace EFinder.Service.Interfaces;

public interface IFinderService
{
    Task<FinderModel> FindValidEmail(string firstName, string lastName, string domain);
}