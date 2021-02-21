using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace SERVER
{
    public class ChatHub : Hub
    {
        public void Broadcast(string text)
        {
            Clients.Others.ReceiveMessage(text);
        }
    }
}
