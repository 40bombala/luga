namespace Luga;

using Agents.Chat;
using Agents.Chat.Interfaces;
using Agents.General;
using Agents.General.Interfaces;
using Agents.Text;
using Agents.Text.Interfaces;
using LanguageModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenAI.GPT3;
using OpenAI.GPT3.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection ConfigureLuga(
        this IServiceCollection services,
        Provider provider)
    {
        switch (provider)
        {
            case Provider.OpenAi:
                services.AddOpenAIService();

                break;
            case Provider.AzureOpenAi:
                var configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();

                services.AddOpenAIService(
                    options =>
                    {
                        options.ProviderType = ProviderType.Azure;
                        options.ResourceName = configuration["OpenAIServiceOptions:ResourceName"];
                        options.DeploymentId = configuration["OpenAIServiceOptions:DeploymentId"];
                        options.ApiVersion = "2023-03-15-preview";
                        options.ApiKey = configuration["OpenAIServiceOptions:ApiKey"];
                    });

                break;
            default:
                throw new ArgumentOutOfRangeException(
                    nameof(provider),
                    provider,
                    message: "OpenAI Provider not supported");
        }

        services.AddScoped<IAgentBuilder, AgentBuilder>();

        return services;
    }

    public static IServiceCollection AddLugaAgents(this IServiceCollection services)
    {
        services.AddScoped<IHtmlTextExtractorAgent, HtmlTextExtractorAgent>();
        services.AddScoped<ISentimentAnalyserAgent, SentimentAnalyserAgent>();

        return services;
    }
}
