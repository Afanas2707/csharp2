using zadanie_444.Models;

namespace zadanie_444.services;

public interface IStatisticsCalculator
{
    List<SizeBucket> GetDefaultSizeBuckets();
    CalculatedStatistics Calculate(IEnumerable<FileInfoData> files, List<SizeBucket> buckets);
}