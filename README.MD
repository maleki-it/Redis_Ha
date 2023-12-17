REDIS-HA With Sentinels

![plot](./redis.drawio.png)

use a Strong Password 
Port Can be Changed
set IPs in .env file

**Separating Read and Write Redis Connection in .net core application**
in this section, We want to describe how to separate the Redis connection in the .net core application.

When you are using Redis Search you may have a heavy load on your Redis server, in 'sentinel' you only connect with the 'Master' Redis server.
On the other hand, you can't write on 'Replica's, because replicas are read-only. 
In this scenario to balance your Redis server load you have to have a load balancer to read from Redis replicas.

Consider we have the following structure for our Redis servers:



