namespace Luga.Agents.General;

using OpenAI.GPT3.Interfaces;
using Shared;
using Shared.Interfaces;

public class PortableAgent<T> : AgentBase, IAgent<T> where T : notnull
{
    public PortableAgent(IOpenAIService openAiService) : base(openAiService) { }

    public Task<T> Ask(string message)
    {
        throw new NotImplementedException();
    }
}
