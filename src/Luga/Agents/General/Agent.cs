namespace Luga.Agents.General;

using Interfaces;
using OpenAI.GPT3.Interfaces;
using Shared;

public class Agent : AgentBase, IAgent
{
    public Agent(IOpenAIService openAiService) : base(openAiService) { }

    public async Task<string> Ask(string message)
    {
        return await GetResponse(message).ConfigureAwait(false);
    }

    public async Task<TOutput> Ask<TOutput>(string message, Func<string, TOutput> outputProcessor)
        where TOutput : notnull
    {
        string response = await GetResponse(message).ConfigureAwait(false);

        return outputProcessor(response);
    }
}
