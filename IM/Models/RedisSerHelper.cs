using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IM.Models
{
    public class RedisSerHelper
    {
        public string BaseInfo;
        private readonly ConnectionMultiplexer _conn;
        public int databaseNumber;
        public RedisSerHelper(int datanumber,string host)
        {
            _conn = RedisConHelper.Getmanager(host);
        }

        /// <summary>
        /// 为redis的key加上前缀并区分
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string SysCusKey(string key)
        {
            return key + (RedisConHelper.SysCustomKey ?? BaseInfo);
        }
    }
}