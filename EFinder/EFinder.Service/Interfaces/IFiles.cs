namespace EFinder.Service.Interfaces;

public interface IFiles
{
    List<string> ReadFileAsStringList(string path);
}