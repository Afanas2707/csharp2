using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using zadanie_444.configuration;
using zadanie_444.Models;

namespace zadanie_444.services;

public class CsvReportGenerator : ICsvReportGenerator
{
    private readonly ILogger<CsvReportGenerator> _logger;
    private readonly AppOptions _options;

    public CsvReportGenerator(ILogger<CsvReportGenerator> logger, IOptions<AppOptions> options)
    {
        _logger = logger;
        _options = options.Value;
    }

    public void GenerateReport(CalculatedStatistics statistics)
    {
        _logger.LogInformation("Начинаем генерацию CSV отчета в файл: {FilePath}", _options.OutputCsvFile);
        var sb = new StringBuilder();
        
        sb.Append("Расширение;");
        sb.AppendLine(string.Join(";", statistics.Buckets.Select(b => b.Name)));
        
        foreach (var extEntry in statistics.Data.OrderBy(e => e.Key))
        {
            sb.Append(extEntry.Key);
            sb.Append(';');
            var countsInBuckets = new List<string>();
            foreach (var bucket in statistics.Buckets)
            {
                countsInBuckets.Add(extEntry.Value.TryGetValue(bucket.Name, out int count) ? count.ToString() : "0");
            }
            sb.AppendLine(string.Join(";", countsInBuckets));
        }

        try
        {
            File.WriteAllText(_options.OutputCsvFile, sb.ToString(), Encoding.UTF8);
            _logger.LogInformation("CSV отчет успешно сохранен: {FilePath}", _options.OutputCsvFile);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при сохранении CSV отчета в файл: {FilePath}", _options.OutputCsvFile);
        }
    }
}