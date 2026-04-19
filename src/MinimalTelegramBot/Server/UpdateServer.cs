using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MinimalTelegramBot.Client;
using MinimalTelegramBot.Logging;
using MinimalTelegramBot.Settings;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace MinimalTelegramBot.Server;

internal sealed partial class UpdateServer
{
    private readonly BotRequestConfiguration _requestConfiguration;
    private readonly ITelegramBotClient _client;
    private readonly ILogger _logger;
    private readonly BotRequestDelegate _pipeline;
    private readonly IServiceProvider _services;

    public UpdateServer(IServiceProvider services, BotRequestDelegate pipeline, BotRequestConfiguration requestConfiguration)
    {
        _services = services;
        _pipeline = pipeline;
        _requestConfiguration = requestConfiguration;
        var loggerFactory = _services.GetRequiredService<ILoggerFactory>();
        _logger = InfrastructureLog.CreateLogger(loggerFactory);
        _client = _services.GetRequiredService<ITelegramBotClient>();
    }

    public UpdateServerPollingInvocationContext CreatePollingInvocationContext(Update update)
    {
        var scope = _services.CreateAsyncScope();
        var context = new BotRequestContext(scope.ServiceProvider, update, _client, _requestConfiguration);
        return new UpdateServerPollingInvocationContext(scope, context);
    }

    public UpdateServerWebhookInvocationContext CreateWebhookInvocationContext(Update update)
    {
        var scope = _services.CreateAsyncScope();
        var client = new WebhookTelegramBotClient(_client);
        var context = new BotRequestContext(scope.ServiceProvider, update, client, _requestConfiguration);
        return new UpdateServerWebhookInvocationContext(scope, context, client);
    }

    public async Task Serve(UpdateServerInvocationContext invocationContext)
    {
        await using (invocationContext)
        {
            try
            {
                await _pipeline(invocationContext.BotRequestContext);
            }
            catch (Exception ex)
            {
                InfrastructureLog.ApplicationError(_logger, ex);
            }
        }
    }
}
