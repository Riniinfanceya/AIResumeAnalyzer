namespace DocumentService.Core.Entities
{
    public class Document : BaseEntity
    {
        public string FileName { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty;

        public string? Summary { get; set; }
    }
}
