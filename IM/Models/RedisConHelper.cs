using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace IM.Models
{
    public static class RedisConHelper
    {
        //系统自定义Key前缀
        public static readonly string SysCustomKey = ConfigurationManager.AppSettings["redisKey"] ?? "";
        private static readonly string redisconstring = ConfigurationManager.ConnectionStrings["redishost"].ConnectionString;
        private static object Lock = new object();
        private static ConnectionMultiplexer _instance = null;

        public static ConnectionMultiplexer Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (Lock)
                    {
                        if (_instance == null || !_instance.IsConnected)
                        {
                            _instance = Getmanager();
                        }
                    }
                }
                return _instance;
            }
        }

        public static ConnectionMultiplexer Getmanager(string connstring = null)
        {
            connstring = connstring ?? redisconstring;
            ConnectionMultiplexer con = ConnectionMultiplexer.Connect(connstring);
            //连接失败事件
            con.ConnectionFailed += con_ConnectionFailed;
            //重新建立连接之前的错误事件
            con.ConnectionRestored += con_ConnectionRestored;
            //发生错误事件
            con.ErrorMessage += con_ErrorMessage;
            //配置更改事件
            con.ConfigurationChanged += con_ConfigurationChanged;
            //更改集群事件
            con.HashSlotMoved += con_HashSlotMoved;
            //类库错误事件
            con.InternalError += con_InternalError;
            return con;
        }

        static void con_InternalError(object sender, InternalErrorEventArgs e)
        {
            Console.WriteLine("类库错误信息:" + e.Exception.Message);
        }

        static void con_HashSlotMoved(object sender, HashSlotMovedEventArgs e)
        {
            Console.WriteLine("更改集群信息:" + e.OldEndPoint + "为" + e.NewEndPoint);
        }

        static void con_ConfigurationChanged(object sender, EndPointEventArgs e)
        {
            Console.WriteLine("更改后端口:" + e.EndPoint);
        }

        static void con_ErrorMessage(object sender, RedisErrorEventArgs e)
        {
            Console.WriteLine("错误信息:" + e.Message);
        }

        static void con_ConnectionRestored(object sender, ConnectionFailedEventArgs e)
        {
            Console.WriteLine("重连信息:" + e.EndPoint);
        }

        static void con_ConnectionFailed(object sender, ConnectionFailedEventArgs e)
        {
            Console.WriteLine("失败信息:" + e.EndPoint + ", " + e.FailureType + (e.Exception == null ? "" : (", " + e.Exception.Message)));
        }
    }
}