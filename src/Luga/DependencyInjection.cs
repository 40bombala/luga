namespace Luga;

using Agents.Chat;
using Agents.Chat.Interfaces;
using Agents.Text;
using Agents.Text.Interfaces;
using LanguageModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenAI.GPT3;
using OpenAI.GPT3.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection ConfigureLugaProviders(
        this IServiceCollection services,
        Provider provider,
        IConfiguration configuration)
    {
        switch (provider)
        {
            case Provider.OpenAi:
                services.AddOpenAIService();

                break;
            case Provider.AzureOpenAi:
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
                throw new ArgumentOutOfRangeException(nameof(provider), provider, message: "OpenAI Provider not supported");
        }

        return services;
    }

    public static IServiceCollection ConfigureLugaServices(this IServiceCollection services)
    {
        services.AddScoped<IIntentClassifierAgent, IntentClassifierAgent>();
        services.AddScoped<IHtmlTextExtractorAgent, HtmlTextExtractorAgent>();
        services.AddScoped<IKnowledgeOracleAgent, KnowledgeOracleAgent>();
        services.AddScoped<ISentimentAnalyserAgent, SentimentAnalyserAgent>();
        services.AddScoped<ISupportAgent, SupportAgent>();

        return services;
    }
}
