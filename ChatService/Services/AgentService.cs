namespace ChatService.Services
{
    public class AgentService
    {
        public string Decide(string question)
        {
            question = question.ToLower();

            if (question.Contains("angular")
                || question.Contains("asp.net")
                || question.Contains("document")
                || question.Contains("resume")
                || question.Contains("notes"))
            {
                return "RAG";
            }

            return "AI";
        }
    }
}