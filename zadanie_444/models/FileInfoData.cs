namespace zadanie_444.Models
{
    public class FileInfoData
    {
        public string Extension { get; set; }
        public long SizeInBytes { get; set; }

        public FileInfoData(string extension, long sizeInBytes)
        {
            Extension = string.IsNullOrEmpty(extension) ? "(без расширения)" : extension.ToLowerInvariant();
            SizeInBytes = sizeInBytes;
        }
    }
}