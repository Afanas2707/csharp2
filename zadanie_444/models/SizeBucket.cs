namespace zadanie_444.Models
{
    public class SizeBucket
    {
        public string Name { get; }
        public long MinBytesInclusive { get; }
        public long MaxBytesExclusive { get; } // Эксклюзивно, кроме последнего бакета

        public SizeBucket(string name, long minBytes, long maxBytes)
        {
            Name = name;
            MinBytesInclusive = minBytes;
            MaxBytesExclusive = maxBytes;
        }

        public bool IsInRange(long size)
        {
            // Для последнего бакета MaxBytesExclusive может быть long.MaxValue,
            // тогда условие size < MaxBytesExclusive всегда будет true для положительных size.
            // Если это не последний бакет, то правая граница не включается.
            // Если это последний бакет, то он должен включать все, что больше или равно MinBytesInclusive.
            if (MaxBytesExclusive == long.MaxValue)
            {
                return size >= MinBytesInclusive;
            }
            return size >= MinBytesInclusive && size < MaxBytesExclusive;
        }
    }

}