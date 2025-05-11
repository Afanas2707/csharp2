namespace zadanie_444.Models;

public class CalculatedStatistics
{
    public Dictionary<string, Dictionary<string, int>> Data { get; }

    public List<SizeBucket> Buckets { get; }

    public CalculatedStatistics(List<SizeBucket> buckets)
    {
        Data = new Dictionary<string, Dictionary<string, int>>();
        Buckets = buckets;
    }

    public void RecordFile(string extensionKey, string bucketName)
    {
        if (!Data.ContainsKey(extensionKey))
        {
            Data[extensionKey] = new Dictionary<string, int>();
            foreach (var bucket in Buckets)
            {
                Data[extensionKey][bucket.Name] = 0;
            }
        }
        Data[extensionKey][bucketName]++;
    }
}