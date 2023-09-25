using Microsoft.AspNetCore.SignalR;

namespace Hub.Hubs
{
    public class ChatHub : Microsoft.AspNetCore.SignalR.Hub
    {
        public void Send(string name, string message)
        {
            // Call the broadcastMessage method to update clients.
            Clients.All.SendAsync("broadcastMessage", name, message);
        }
    }
}
