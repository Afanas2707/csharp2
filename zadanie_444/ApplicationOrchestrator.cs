using Microsoft.Extensions.Logging;
using zadanie_444.services;

namespace zadanie_444;

public class ApplicationOrchestrator
{
    private readonly ILogger<ApplicationOrchestrator> _logger;
    private readonly IFileEnumeratorService _fileEnumerator;
    private readonly IStatisticsCalculator _statisticsCalculator;
    private readonly ICsvReportGenerator _csvReportGenerator;

    public ApplicationOrchestrator(
        ILogger<ApplicationOrchestrator> logger,
        IFileEnumeratorService fileEnumerator,
        IStatisticsCalculator statisticsCalculator,
        ICsvReportGenerator csvReportGenerator)
    {
        _logger = logger;
        _fileEnumerator = fileEnumerator;
        _statisticsCalculator = statisticsCalculator;
        _csvReportGenerator = csvReportGenerator;
    }

    public void Run()
    {
        _logger.LogInformation("Приложение запущено.");

        var files = _fileEnumerator.EnumerateFiles();
        if (!files.Any())
        {
            _logger.LogWarning("Файлы для анализа не найдены. Проверьте путь и права доступа.");
            return;
        }

        var buckets = _statisticsCalculator.GetDefaultSizeBuckets();
        var statistics = _statisticsCalculator.Calculate(files, buckets);
        
        _csvReportGenerator.GenerateReport(statistics);

        _logger.LogInformation("Работа приложения завершена.");
    }
}