using System.Text.Json;

namespace ChatService.Services
{
    public class OllamaService
    {
        private readonly HttpClient _httpClient;
        private readonly string _model;
        private readonly int _contextSize;
        private readonly int _batchSize;

        public OllamaService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(
                configuration["Ollama:BaseUrl"] ?? "http://localhost:11434");
            _model = configuration["Ollama:Model"] ?? "qwen2.5:0.5b";
            _contextSize = configuration.GetValue("Ollama:NumCtx", 1024);
            _batchSize = configuration.GetValue("Ollama:NumBatch", 64);

            _httpClient.Timeout =
                TimeSpan.FromMinutes(5);
        }

        public async Task<string> AskAsync(string prompt)
        {
            var response =
                await _httpClient.PostAsJsonAsync(
                    "/api/generate",
                    new
                    {
                        model = _model,
                        prompt = prompt,
                        stream = false,
                        options = new
                        {
                            num_ctx = _contextSize,
                            num_batch = _batchSize
                        }
                    });

            var json =
                await response.Content.ReadAsStringAsync();

            using var result =
                JsonDocument.Parse(json);

            if (result.RootElement.TryGetProperty(
                    "response",
                    out var responseText))
            {
                return responseText.GetString()
                       ?? string.Empty;
            }

            if (result.RootElement.TryGetProperty(
                    "error",
                    out var errorText))
            {
                return $"Ollama Error: {errorText.GetString()}";
            }

            return json;
        }
    }
}