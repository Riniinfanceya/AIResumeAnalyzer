namespace ChatService.Entities
{
    public class Document
    {
        public Guid Id { get; set; }

        public string FileName { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty;

        public string? Summary { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public bool IsActive { get; set; }
    }
}