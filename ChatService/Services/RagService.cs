using ChatService.Data;
using Microsoft.EntityFrameworkCore;

namespace ChatService.Services
{
    public class RagService
    {
        private readonly ChatDbContext _context;

        public RagService(ChatDbContext context)
        {
            _context = context;
        }

        public async Task<string> BuildPromptAsync(
            string question)
        {
            var docs = await _context.Documents
                .Where(x => x.IsActive)
                .OrderByDescending(x => x.CreatedAt)
                .Take(3)
                .ToListAsync();

            if (!docs.Any())
            {
                return question;
            }

            var context = string.Join(
                "\n\n",
                docs.Select(x =>
                    $"Title: {x.FileName}\nContent: {x.Content}"));

            return $"""
                Use the following context to answer.

                Context:
                {context}

                Question:
                {question}
                """;
        }
    }
}