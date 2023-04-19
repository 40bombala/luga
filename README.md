# LUGA - Language Understanding Generative Agents

A Generative AI Agent Management framework for .NET.

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
services.ConfigureLugaProviders(Provider.OpenAi, configuration);
```

## Inject Services

Inject the required services into your classes:

```csharp
public class MyClass
{
    private readonly IIntentClassifierAgent _intentClassifierAgent;
    private readonly ITextExtractorAgent _htmlTextExtractorAgent;

    public MyClass(IIntentClassifierAgent intentClassifierAgent, IHtmlTextExtractorAgent htmlTextExtractorAgent)
    {
        _intentClassifierAgent = intentClassifierAgent;
        _htmlTextExtractorAgent = htmlTextExtractorAgent;
    }

    // Your methods here
} 
```

## Use AI Agents

Use the AI agents to perform tasks:

```csharp
var intentResult = await _intentClassifierAgent.Ask("What is the weather like today?");
var extractedText = await _htmlTextExtractorAgent.Ask("<html><body><p>Hello, World!</p></body></html>");
``` 