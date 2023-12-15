namespace RediSearch_webapi.Configs;

public class RedisConnectionConfig
{
	public string Password{ get; set; }
	public string[] SentinelAddresses { get; set; }
	public string[] ReplicaAddresses { get; set; }

}