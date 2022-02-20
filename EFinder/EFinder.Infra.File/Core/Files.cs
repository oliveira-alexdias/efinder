using EFinder.Service.Interfaces;

namespace EFinder.Infra.File.Core;

public class Files : IFiles
{
    public List<string> ReadFileAsStringList(string path)
    {
        return System.IO.File.ReadAllLines(path).ToList();
    }
}