using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using zadanie_444.configuration;
using zadanie_444.Models;

namespace zadanie_444.services;

public class FileEnumeratorService : IFileEnumeratorService
{
    private readonly ILogger<FileEnumeratorService> _logger;
    private readonly AppOptions _options;

    public FileEnumeratorService(ILogger<FileEnumeratorService> logger, IOptions<AppOptions> options)
    {
        _logger = logger;
        _options = options.Value;
    }

    public List<FileInfoData> EnumerateFiles()
    {
        _logger.LogInformation("Начинаем сканирование директории: {Directory}", _options.TargetDirectory);
        var fileDataList = new List<FileInfoData>();
        var searchOption = _options.RecursiveScan ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;

        try
        {
            var files = Directory.EnumerateFiles(_options.TargetDirectory, "*.*", searchOption);
            long count = 0;
            foreach (var filePath in files)
            {
                try
                {
                    var fileInfo = new FileInfo(filePath);
                    var extension = Path.GetExtension(filePath);
                    fileDataList.Add(new FileInfoData(extension, fileInfo.Length));
                    count++;
                    if (count % 1000 == 0) _logger.LogDebug("Обработано {Count} файлов...", count);

                }
                catch (FileNotFoundException fnfEx)
                {
                    _logger.LogWarning(fnfEx, "Файл не найден (возможно, удален во время сканирования): {FilePath}", filePath);
                }
                catch (UnauthorizedAccessException uaEx)
                {
                    _logger.LogWarning(uaEx, "Нет доступа к файлу: {FilePath}", filePath);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Ошибка при обработке файла: {FilePath}", filePath);
                }
            }
        }
        catch (UnauthorizedAccessException uaEx)
        {
            _logger.LogError(uaEx, "Нет доступа к директории: {Directory}", _options.TargetDirectory);
        }
        catch (DirectoryNotFoundException dnfEx)
        {
            _logger.LogError(dnfEx, "Директория не найдена: {Directory}", _options.TargetDirectory);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Критическая ошибка при сканировании директории: {Directory}", _options.TargetDirectory);
        }

        _logger.LogInformation("Сканирование завершено. Найдено {Count} файлов.", fileDataList.Count);
        return fileDataList;
    }
}