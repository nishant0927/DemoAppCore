namespace ASPCoreWebApp.Models
{
    public class FileViewModel
    {
        public Guid EmpGuid { get; set; }

        public Guid FileGuid { get; set; }= Guid.NewGuid();

        public string FileName { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string? FilePath { get; set; }
        public IFormFile File { get; set; } = null!;
    }
}
