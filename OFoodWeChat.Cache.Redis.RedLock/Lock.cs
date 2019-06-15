/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc

    文件名：Lock.cs
    文件功能描述：Redis 锁
----------------------------------------------------------------*/

using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redlock.CSharp
{
    public class Lock
    {

        public Lock(RedisKey resource, RedisValue val, TimeSpan validity)
        {
            this.resource = resource;
            this.val = val ;
            this.validity_time = validity;
        }

        private RedisKey resource;

        private RedisValue val;

        private TimeSpan validity_time;

        public RedisKey Resource { get { return resource; } }

        public RedisValue Value { get { return val; } }

        public TimeSpan Validity { get { return validity_time; } }
    }
}
