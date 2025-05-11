using Microsoft.Extensions.Logging;
using zadanie_444.Models;

namespace zadanie_444.services;

public class StatisticsCalculator : IStatisticsCalculator
{
    private readonly ILogger<StatisticsCalculator> _logger;

    public StatisticsCalculator(ILogger<StatisticsCalculator> logger)
    {
        _logger = logger;
    }

    public List<SizeBucket> GetDefaultSizeBuckets()
    {
        return new List<SizeBucket>
        {
            new SizeBucket("0 – 1кБ", 0, 1024),
            new SizeBucket("1кБ – 10кБ", 1024, 10 * 1024),
            new SizeBucket("10кБ – 100кБ", 10 * 1024, 100 * 1024),
            new SizeBucket("100кБ – 1МБ", 100 * 1024, 1024 * 1024),
            new SizeBucket("1МБ – 10МБ", 1024 * 1024, 10 * 1024 * 1024),
            new SizeBucket("10МБ – 100МБ", 10 * 1024 * 1024, 100 * 1024 * 1024),
            new SizeBucket("100МБ+", 100 * 1024 * 1024, long.MaxValue)
        };
    }

    public CalculatedStatistics Calculate(IEnumerable<FileInfoData> files, List<SizeBucket> buckets)
    {
        _logger.LogInformation("Начинаем расчет статистики для {FileCount} файлов.", files.Count());
        var statistics = new CalculatedStatistics(buckets);

        foreach (var file in files)
        {
            bool categorized = false;
            foreach (var bucket in buckets)
            {
                if (bucket.IsInRange(file.SizeInBytes))
                {
                    statistics.RecordFile(file.Extension, bucket.Name);
                    categorized = true;
                    break;
                }
            }
            if (!categorized)
            {
                _logger.LogWarning("Файл {Extension} размером {Size} не попал ни в одну категорию. Файл: {FileName}", file.Extension, file.SizeInBytes, "(имя файла не хранится в FileInfoData для экономии памяти)");
            }
        }

        _logger.LogInformation("Расчет статистики завершен.");
        return statistics;
    }
}
