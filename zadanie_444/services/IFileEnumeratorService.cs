using zadanie_444.Models;

namespace zadanie_444.services;

public interface IFileEnumeratorService
{
    List<FileInfoData> EnumerateFiles();
}