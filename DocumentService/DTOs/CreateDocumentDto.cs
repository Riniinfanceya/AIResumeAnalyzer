namespace DocumentService.DTOs
{
    public class CreateDocumentDto
    {
        public string FileName { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty;

        public string? Summary { get; set; }
    }
}
