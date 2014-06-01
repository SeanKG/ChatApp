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


        /// <summary>
        /// The model for a message beingsent out to other users
        /// </summary>
        public class message 
        {
            public string userName;
            public string connectionID;
            public string messageText;
        }


        /// <summary>
        /// the model for a user connceted to the hub
        /// </summary>
        public class user
        {
            public string userName;
            public string connectionID;
        }


        /// <summary>
        /// Stores a list of messages in cache
        /// </summary>
        public static List<message> messageLog {
            get {
                // set cache item if null
                if (HttpRuntime.Cache["messageLog"] == null) { HttpRuntime.Cache["messageLog"] = new List<message>(); }
                return (List<message>)HttpRuntime.Cache["messageLog"]; 
            }
        }


        /// <summary>
        /// Stores a list of connected users in cache
        /// </summary>
        public static List<user> userList {
            get {
                // set cache item if null
                if (HttpRuntime.Cache["userList"] == null) { HttpRuntime.Cache["userList"] = new List<user>(); }
                return (List<user>)HttpRuntime.Cache["userList"];
            }
        }


        /// <summary>
        /// Called from client - 
        /// Creates a message object with the params and connectionID,
        /// saves the message to messageLog and sends it out to connected clients
        /// </summary>
        /// <param name="message">The message the user typed.</param>
        /// <param name="userName">The username of the sender.</param>
        public void newMessage(string message, string userName)
        {
            string connectionID = Context.ConnectionId;
            message messageObj = new message() { 
                messageText = message,
                userName = userName,
                connectionID = connectionID
            };
            messageLog.Add(messageObj);
            Clients.All.newMessage(messageObj);
        }


        /// <summary>
        /// Sets the senders username
        /// </summary>
        /// <param name="name"></param>
        public void SetUserName(string name)
        {
            string connectionID = Context.ConnectionId;
            userList.Single(
                    s => s.connectionID == connectionID
                ).userName = name;
            Clients.All.SetUserName(name, connectionID);
        }


        /// <summary>
        /// Not implemented
        /// </summary>
        /// <param name="connectionID"></param>
        public void privateMessage(string connectionID) 
        { 
            
        }


        /// <summary>
        /// When someone connects, send their connectionID to other users.
        /// Also send the connectionID to the sender you know? so they like, know who they are. and stuff.
        /// </summary>
        /// <returns></returns>
        public override Task OnConnected()
        {
            Clients.All.NewConnectionID(Context.ConnectionId);
            Clients.Caller.SetConnectionID(Context.ConnectionId);
            userList.Add(new user() { connectionID = Context.ConnectionId });
            return base.OnConnected();
        }


        /// <summary>
        /// How dare they disconnect on us!
        /// Let all the other users know so they can talk behind the quitters back.   
        /// </summary>
        /// <returns></returns>
        public override Task OnDisconnected()
        {
            string connectionID = Context.ConnectionId;
            Clients.All.UserDisconnected(Context.ConnectionId);
            userList.Remove(userList.Single(
                    s => s.connectionID == Context.ConnectionId
                ));
            return base.OnDisconnected();
        }


        /// <summary>
        /// Let everyone know the quitter didn't quit for reals
        /// </summary>
        /// <returns></returns>
        public override Task OnReconnected()
        {
            Clients.All.UserReconnected(Context.ConnectionId);
            return base.OnReconnected();
        }
    }
}