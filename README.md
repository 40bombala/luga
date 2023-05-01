# LUGA - Language Understanding Generative Agents

A Generative AI Agent Management framework for .NET.

Github: [luga](https://github.com/40bombala/luga)

## Installation

### NuGet

```bash
dotnet add package Luga
```

## Configuration

1. Add your OpenAI API key and (optionally) organization ID in `appsettings.json` or use .NET user secrets:

```json
{
  "OpenAIServiceOptions": {
    "ApiKey": "Your API key goes here",
    "Organization": "Your Organization ID goes here (optional)"
  }
}
``` 

### For Azure OpenAI

```json
{
  "OpenAIServiceOptions": {
    "ApiKey": "Your API key goes here",
    "Organization": "Your Organization ID goes here",
    "ResourceName": "Your Azure Resource Name goes here",
    "DeploymentId": "Your Azure Deployment ID goes here"
  }
}
``` 

## Register AI Agents

Register the AI agents using the `ConfigureLugaProviders` extension method:

```csharp
services.ConfigureLuga(Provider.OpenAi);

// To use pre-built agents
services.AddLugaAgents()
```

## Inject Services

Inject the required services into your classes:

```csharp
public class MyClass
{
    private readonly IHtmlTextExtractorAgent _htmlTextExtractorAgent;

    public MyClass(IHtmlTextExtractorAgent htmlTextExtractorAgent)
    {
        _htmlTextExtractorAgent = htmlTextExtractorAgent;
    }
} 
```

## Use AI Agents

Use the AI agents to perform tasks:

```csharp
var extractedText = await _htmlTextExtractorAgent.Ask("<html><body><p>Hello, World!</p></body></html>");
``` 

## Build Agents Dynamically

Use the `IAgentBuilder` builder to build agents dynamically:

```csharp
var agentBuilder = host.Services.GetRequiredService<IAgentBuilder>();

Agent agent = agentBuilder!
             .WithContext(
                  """
                  You are a GPT-based Language Detection Agent, specifically designed to identify the language of a given text.
                  Your primary function is to analyze the text and return the language it is written in, using ISO 639-1 language codes (e.g., 'en' for English, 'es' for Spanish, 'fr' for French).

                  When you receive a text input, carefully examine its content and determine the language with high accuracy.
                  Use your extensive knowledge of various languages and linguistic patterns to identify the correct language code.
                  Remember, your main goal is to provide accurate and efficient language detection for any text input you receive.
                  """)
             .WithModel(Models.Gpt4)
             .Build();

var response = await agent.Ask(
    message: "Bonjour, comment ça va?",
    output => new
    {
        Language = output
    });
```

## References

This project utilizes [Betalgo.OpenAI](https://github.com/betalgo/openai) to communicate with the OpenAI API.