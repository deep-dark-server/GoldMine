﻿using StackExchange.Redis;

namespace GoldMine.ServerBase.Redis
{
    /// <summary>
    /// Interface to make arbitrary class with parameterless ctor
    /// compatible with RedisValue, to cache it into Redis
    /// </summary>
    public interface IRedisStorable
    {
        void LoadFrom(RedisValue value);

        RedisValue ToRedisValue();
    }
}