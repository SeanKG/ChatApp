using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace App.ChatApp.SignalR
{
    [CLSCompliant(false)]
    public class ChatHub : Hub
    {
        public void Hello(string message)
        {
            Clients.All.hello(message);
        }
    }
}