namespace zadanie_444.configuration;

public class AppOptions
{
    public const string SectionName = "AppConfig";

    public string TargetDirectory { get; set; } = ".";
    public string OutputCsvFile { get; set; } = "file_statistics.csv";
    public bool RecursiveScan { get; set; } = true;
}