namespace Luga.Agents.Shared;

using OpenAI.GPT3.Interfaces;
using OpenAI.GPT3.ObjectModels;
using OpenAI.GPT3.ObjectModels.RequestModels;
using OpenAI.GPT3.ObjectModels.ResponseModels;

public abstract class AgentBase
{
    private readonly IOpenAIService _openAiService;

    protected AgentBase(IOpenAIService openAiService)
    {
        _openAiService = openAiService;
    }

    protected AgentBase(IOpenAIService openAiService, string context)
    {
        _openAiService = openAiService;
        Context = context;
    }

    protected internal float? Temperature { get; set; }

    protected internal string Model { get; set; } = Models.ChatGpt3_5Turbo;

    protected internal string Context { get; set; } = default!;

    protected async Task<string> GetResponse(string message)
    {
        ChatCompletionCreateRequest request = new()
        {
            Messages = new List<ChatMessage>
            {
                ChatMessage.FromSystem(Context),
                ChatMessage.FromUser(message)
            },
            Model = Model,
            Temperature = Temperature
        };

        ChatCompletionCreateResponse completionResult =
            await _openAiService.ChatCompletion.CreateCompletion(request).ConfigureAwait(false);

        return completionResult switch
        {
            { Successful: true } => completionResult.Choices.First().Message.Content,
            var _ => throw new Exception("Failed to get response from OpenAI")
        };
    }

    protected async Task<ChatMessage> GetResponse(IEnumerable<ChatMessage> messages)
    {
        List<ChatMessage> chatMessages = new()
        {
            ChatMessage.FromSystem(Context)
        };

        ChatCompletionCreateRequest request = new()
        {
            Messages = chatMessages
                      .Concat(messages)
                      .ToList(),
            Model = Model,
            Temperature = Temperature
        };

        ChatCompletionCreateResponse completionResult =
            await _openAiService.ChatCompletion.CreateCompletion(request).ConfigureAwait(false);

        return completionResult switch
        {
            { Successful: true } => completionResult.Choices.First().Message,
            var _ => throw new Exception("Failed to get response from OpenAI")
        };
    }
}
