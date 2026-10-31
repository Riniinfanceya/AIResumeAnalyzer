# AI Resume Analyzer

AI Resume Analyzer is a .NET 10 solution composed of ASP.NET Core services. The chat service uses Ollama for AI responses and can use stored documents to build retrieval-augmented prompts. The API Gateway routes chat and document requests to their services.

## Solution projects

- **ApiGateway** — YARP reverse proxy for chat and document APIs.
- **ChatService** — chat API, Ollama integration, and document-based RAG.
- **DocumentService** — API for creating and retrieving stored documents.
- **UserService** — user service project (currently contains the ASP.NET Core starter endpoints).

## Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- SQL Server. The checked-in development configuration uses a local SQL Server Express instance; change the connection strings if your server differs.
- [Ollama](https://ollama.com/) for local AI responses.

Pull the default chat model before starting ChatService:

```powershell
ollama pull qwen2.5:0.5b
```

## Configuration

Before running the services, configure `ConnectionStrings:DefaultConnection` in `ChatService/appsettings.json` and `DocumentService/appsettings.json` for your SQL Server instance. Do not commit credentials or secrets; use .NET user secrets or environment variables for sensitive values.

ChatService reads the Ollama URL, model, and generation limits from its `Ollama` settings. Defaults are `http://localhost:11434`, `qwen2.5:0.5b`, a context size of 1024, and a batch size of 64. Ensure the Ollama service is running and update these settings if needed.

## Build

From the repository root:

```powershell
dotnet restore AIResumeAnalyzer.slnx
dotnet build AIResumeAnalyzer.slnx
```

## Run locally

Start SQL Server and Ollama first. Then run each project in its own terminal from the repository root:

```powershell
dotnet run --project ChatService/ChatService.csproj --launch-profile https
dotnet run --project DocumentService/DocumentService.csproj --launch-profile https
dotnet run --project ApiGateway/ApiGateway.csproj --launch-profile https
dotnet run --project UserService/UserService.csproj --launch-profile https
```

The gateway's checked-in configuration forwards chat requests to `https://localhost:7125` and document requests to `https://localhost:7187`. These match the ChatService and DocumentService HTTPS launch profiles. The gateway listens at `https://localhost:7298`.

## API examples

Ask a question through the gateway:

```powershell
Invoke-RestMethod `
  -Uri 'https://localhost:7298/api/chat' `
  -Method Post `
  -ContentType 'application/json' `
  -Body '{"question":"What is a resume?"}'
```

Create a document through the gateway (the request accepts `fileName`, `content`, and `summary`):

```powershell
Invoke-RestMethod `
  -Uri 'https://localhost:7298/api/document' `
  -Method Post `
  -ContentType 'application/json' `
  -Body '{"fileName":"resume.txt","content":"Resume text goes here","summary":"Candidate experience and skills"}'
```

Retrieve stored documents with `GET https://localhost:7298/api/document`.

## Development notes

- Chat and document services expose OpenAPI documents in the Development environment.
- The database schema for DocumentService is maintained with Entity Framework Core migrations.
- HTTPS development certificates may need to be trusted on your machine. If the browser or client reports a certificate error, see the [.NET HTTPS development certificate documentation](https://learn.microsoft.com/aspnet/core/security/enforcing-ssl).
