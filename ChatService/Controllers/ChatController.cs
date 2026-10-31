using ChatService.Models;
using ChatService.Services;
using Microsoft.AspNetCore.Mvc;

namespace ChatService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatController : ControllerBase
    {
        private readonly OllamaService _ollamaService;
        private readonly RagService _ragService;
        private readonly AgentService _agentService;

        public ChatController(
            OllamaService ollamaService,
            RagService ragService,
            AgentService agentService)
        {
            _ollamaService = ollamaService;
            _ragService = ragService;
            _agentService = agentService;
        }

        [HttpPost]
        public async Task<IActionResult> Ask(
            ChatRequest request)
        {
            string answer;

            var mode =
                _agentService.Decide(
                    request.Question);

            if (mode == "RAG")
            {
                var prompt =
                    await _ragService
                    .BuildPromptAsync(
                        request.Question);

                answer =
                    await _ollamaService
                    .AskAsync(prompt);
            }
            else
            {
                answer =
                    await _ollamaService
                    .AskAsync(
                        request.Question);
            }

            return Ok(new
            {
                mode,
                answer
            });
        }
    }
}