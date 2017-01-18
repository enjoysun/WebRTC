using SuperWebSocket;
using SuperSocket.SocketBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.WebSockets;

namespace IM.Models
{
    public class websocketcommon
    {
        public string Connect()
        {
            var socket = new WebSocketServer();
            socket.NewSessionConnected += socket_NewSessionConnected;
            socket.NewMessageReceived += socket_NewMessageReceived;
            socket.SessionClosed += socket_SessionClosed;
            try
            {
                socket.Setup("192.168.41.98", 5079);
                socket.Start();
                return "服务器已开启";
            }
            catch (Exception e)
            {

                return e.Message;
            }
        }

        void socket_SessionClosed(WebSocketSession session, CloseReason value)
        {

            System.Web.HttpContext.Current.Response.Write(session.Origin);
        }

        void socket_NewMessageReceived(WebSocketSession session, string value)
        {
            //session.Send(value);单发
            //群发
            string id=session.SessionID;
            SendToAll(session,value);
        }

        void socket_NewSessionConnected(WebSocketSession session)
        {
            System.Web.HttpContext.Current.Response.Write(session.Origin);
        }
        private void SendToAll(WebSocketSession session,string msg)
        {
            foreach (var item in session.AppServer.GetAllSessions())
            {
                item.Send(msg);
            }
        }
    }
}