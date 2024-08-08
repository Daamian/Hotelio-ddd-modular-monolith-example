using MediatR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Hotelio.CrossContext.Infrastructure.Message;

internal class BackgroundServiceChannel: BackgroundService
{
    private readonly IMessageChannel _messageChannel;
    private readonly ILogger<BackgroundServiceChannel> _logger;
    private readonly IMediator _mediator;

    public BackgroundServiceChannel(IMessageChannel messageChannel, ILogger<BackgroundServiceChannel> logger, IMediator mediator)
    {
        _messageChannel = messageChannel;
        _logger = logger;
        _mediator = mediator;
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Running the background service.");

        await foreach (var message in _messageChannel.Reader.ReadAllAsync(stoppingToken))
        {
            try
            {
                _logger.LogInformation($"Publish message { message }");
                await _mediator.Publish(message, stoppingToken);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);
            }
        }
    }
}