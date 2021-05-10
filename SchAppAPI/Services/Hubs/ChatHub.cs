using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SchAppAPI.Services.Hubs
{
    public static class ConnectedUsers
    {
        public static List<string> Users = new List<string>();
    }
    public class ChatHub: Hub
    {
        public override async Task OnConnectedAsync()
        {
            ConnectedUsers.Users.Add(Context.User?.FindFirst(ClaimTypes.Name)?.Value);
            await base.OnConnectedAsync();
        }

        //SignalR Verions 1 Signature
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            ConnectedUsers.Users.Remove(Context.User?.FindFirst(ClaimTypes.Name)?.Value);
            await base.OnDisconnectedAsync(exception);
        }

    }
}
