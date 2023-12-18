using System.Net;
using Microsoft.AspNetCore.Connections;
using RediSearch_webapi.Configs;
using RediSearch_webapi.Factories;
using StackExchange.Redis;

namespace RediSearch_webapi;

public static class RedisConnectionRegistrationExtenstion
{
	
	public static IServiceCollection AddRedisConnection(this IServiceCollection services,IConfiguration configuration)
	{
		var redisConnectionConfig =
			configuration.GetSection("RedisConnectionConfig").Get<RedisConnectionConfig>();

		var replicaConfig = new ConfigurationOptions()
		{
			Password = redisConnectionConfig.Password,
		};
		foreach (var endPoint in redisConnectionConfig.ReplicaAddresses)
		{
			replicaConfig.EndPoints.Add(endPoint);
		}
		ConnectionMultiplexer readConnectionMux = ConnectionMultiplexer.Connect(replicaConfig);

		var sentinelConfig = new ConfigurationOptions()
		{

			Password = redisConnectionConfig.Password,

		};
		foreach (var endPoint in redisConnectionConfig.SentinelAddresses)
		{
			sentinelConfig.EndPoints.Add(endPoint);
		}
		var sentinelConnection = ConnectionMultiplexer.SentinelConnect(sentinelConfig);
		var masterConfig = new ConfigurationOptions()
		{
			ServiceName = "mymaster",
			Password = redisConnectionConfig.Password,
			AllowAdmin = true
		};
		var masterConnection = sentinelConnection.GetSentinelMasterConnection(masterConfig);
		var factory = new RedisConnectionFactory(readConnectionMux, masterConnection);
		services.AddSingleton(factory);
		return services;
	} 
}