using StackExchange.Redis;

namespace RediSearch_webapi.Factories;

public class RedisConnectionFactory
{
	private readonly ConnectionMultiplexer _readConnection;
	private readonly ConnectionMultiplexer _writeConnection;

	public RedisConnectionFactory(ConnectionMultiplexer readConnection, ConnectionMultiplexer writeConnection)
	{
		_readConnection = readConnection;
		_writeConnection = writeConnection;
	}
	public ConnectionMultiplexer GetReadConnection()
	{
		return _readConnection;
	}
	public ConnectionMultiplexer GetWriteConnection()
	{
		return _writeConnection;
	}
}