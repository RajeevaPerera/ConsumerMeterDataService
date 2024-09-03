namespace Ensek.Models.ConsumerMeterDataService.Internal;

public enum FileType : ushort
{
    Cvs = 0
}

public class FileDetails
{
    public Guid Id { get; set; }
    
    public string FileName { get; set; }
    
    public byte[] FileData { get; set; }
    
    public FileType FileType { get; set; }
}