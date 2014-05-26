using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Web.Caching;

using System.Threading.Tasks;

namespace App.ChatApp.SignalR
{
    [CLSCompliant(false)]
    public class ChatHub : Hub
    {

        public class message 
        {
            public string userName;
            public string connectionID;
            public string messageText;
        }

        public class user
        {
            public string userName;
            public string connectionID;
        }

        public static List<message> messageLog {
            get {
                if (HttpContext.Current.Cache["messageLog"] == null)
                {
                    HttpContext.Current.Cache["messageLog"] = new List<message>();
                }
                return (List<message>)HttpContext.Current.Cache["messageLog"]; 
            }
            //set { HttpContext.Current.Cache["ChatLog"]; }
        }

        public static List<user> userList {
            get {
                if (HttpContext.Current.Cache["userList"] == null)
                {
                    HttpContext.Current.Cache["userList"] = new List<user>();
                }
                return (List<user>)HttpContext.Current.Cache["userList"];
            }
            //set { HttpContext.Current.Cache["ChatLog"]; }
        }

        public void newMessage(string message, string userName)
        {
            string connectionID = Context.ConnectionId;
            message messageObj = new message() { 
                messageText = message,
                userName = userName,
                connectionID = connectionID
            };
            
            //userList.Single(
            //        s => s.connectionID == connectionID
            //    ).messages.Add(messageObj);

            messageLog.Add(messageObj);
            
            Clients.All.newMessage(messageObj);
        }

        public void SetUserName(string name)
        {
            string connectionID = Context.ConnectionId;
            
            userList.Single(
                    s => s.connectionID == connectionID
                ).userName = name;

            Clients.All.SetUserName(name, connectionID);
        }

        public void privateMessage(string connectionID) 
        { 
            
        }

        public override Task OnConnected()
        {

            Clients.All.NewConnectionID(Context.ConnectionId);
            Clients.Caller.SetConnectionID(Context.ConnectionId);

            //string connectionID = Context.ConnectionId;

            userList.Add(new user() { connectionID = Context.ConnectionId });

            return base.OnConnected();
        }


        public override Task OnDisconnected()
        {
            //string connectionID = Context.ConnectionId;

            Clients.All.UserDisconnected(Context.ConnectionId);

            //userList.Remove(userList.Single(
            //        s => s.connectionID == Context.ConnectionId
            //    ));
            
            return base.OnDisconnected();
        }


        public override Task OnReconnected()
        {
            //string connectionID = Context.ConnectionId;

            Clients.All.UserReconnected(Context.ConnectionId);

            return base.OnReconnected();
        }
    }
}