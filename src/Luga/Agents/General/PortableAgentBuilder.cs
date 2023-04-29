namespace Luga.Agents.General;

using OpenAI.GPT3.Interfaces;

public class PortableAgentBuilder<T> where T : notnull
{
    private readonly PortableAgent<T> _portableAgent;

    public PortableAgentBuilder(IOpenAIService openAiService)
    {
        _portableAgent = new PortableAgent<T>(openAiService);
    }

    public PortableAgentBuilder<T> WithPrompt(string prompt)
    {
        _portableAgent.Context = prompt;

        return this;
    }

    public PortableAgentBuilder<T> WithTemperature(float temperature)
    {
        _portableAgent.Temperature = temperature;

        return this;
    }
}
