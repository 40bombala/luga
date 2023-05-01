namespace Luga.Agents.General.Interfaces;

using Contracts;
using LanguageModels.GPT;

public interface IAgentBuilder
{
    IAgentBuilder WithContext(string context);

    IAgentBuilder WithTemperature(float temperature);

    IAgentBuilder WithSettings(AgentBuilderSettings settings);

    IAgentBuilder WithModel(Models model);

    Agent Build();
}
