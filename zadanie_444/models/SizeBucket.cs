namespace zadanie_444.Models
{
    public class SizeBucket
    {
        public string Name { get; }
        public long MinBytesInclusive { get; }
        public long MaxBytesExclusive { get; }

        public SizeBucket(string name, long minBytes, long maxBytes)
        {
            Name = name;
            MinBytesInclusive = minBytes;
            MaxBytesExclusive = maxBytes;
        }

        public bool IsInRange(long size)
        {
            if (MaxBytesExclusive == long.MaxValue)
            {
                return size >= MinBytesInclusive;
            }
            return size >= MinBytesInclusive && size < MaxBytesExclusive;
        }
    }

}