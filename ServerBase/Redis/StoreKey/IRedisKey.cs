﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace GoldMine.ServerBase.Redis.StoreKey
{
    public interface IRedisKey
    {
        RedisKey RedisKey { get; }
    }
}
