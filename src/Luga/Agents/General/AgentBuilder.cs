namespace Luga.Agents.General;

using Contracts;
using Interfaces;
using LanguageModels.GPT;
using OpenAI.GPT3.Interfaces;

public class AgentBuilder : IAgentBuilder
{
    private readonly IOpenAIService _openAiService;
    private Agent _agent;

    public AgentBuilder(IOpenAIService openAiService)
    {
        _openAiService = openAiService;
        _agent = new Agent(openAiService);
    }

    public IAgentBuilder WithContext(string context)
    {
        _agent.Context = context;

        return this;
    }

    public IAgentBuilder WithTemperature(float temperature)
    {
        _agent.Temperature = temperature;

        return this;
    }

    public IAgentBuilder WithSettings(AgentBuilderSettings settings)
    {
        _agent.Context = settings.Context;
        _agent.Temperature = settings.Temperature;

        return this;
    }

    public IAgentBuilder WithModel(Models model)
    {
        _agent.Model = model switch
        {
            Models.Gpt4 => OpenAI.GPT3.ObjectModels.Models.Gpt_4,
            var _ => OpenAI.GPT3.ObjectModels.Models.ChatGpt3_5Turbo
        };

        return this;
    }

    public Agent Build()
    {
        Agent builtAgent = _agent;
        _agent = new Agent(_openAiService);

        return builtAgent;
    }
}
