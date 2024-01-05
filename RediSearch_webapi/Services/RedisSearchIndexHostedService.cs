namespace RediSearch_webapi.Services;

public class RedisSearchIndexHostedService:BackgroundService
{
	private int executionCount = 0;
	private readonly ILogger<RedisSearchIndexHostedService> _logger;
	public IServiceProvider Services { get; }
	public RedisSearchIndexHostedService(ILogger<RedisSearchIndexHostedService> logger, IServiceProvider services)
	{
		_logger = logger;
		Services = services;
	}

	protected override async Task ExecuteAsync(CancellationToken stoppingToken)
	{
		_logger.LogInformation("Timed Hosted Service running.");
		using var scope = Services.CreateScope();
		var scopedProcessingService =
			scope.ServiceProvider
				.GetRequiredService<ProductService>();

		await scopedProcessingService.CreateProductIndex(stoppingToken);
	}

}