namespace Luga.Agents.General.Interfaces;

using Shared.Interfaces;

public interface IAgent : IAgentRoot<string>
{
    public Task<TOutput> Ask<TOutput>(string message, Func<string, TOutput> outputProcessor) where TOutput : notnull;
}
