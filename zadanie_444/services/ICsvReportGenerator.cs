using zadanie_444.Models;

namespace zadanie_444.services;

public interface ICsvReportGenerator
{
    void GenerateReport(CalculatedStatistics statistics);
}
