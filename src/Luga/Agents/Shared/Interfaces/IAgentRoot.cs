namespace Luga.Agents.Shared.Interfaces;

public interface IAgentRoot<TOutput> where TOutput : notnull
{
    public Task<TOutput> Ask(string message);
}
