using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Expro.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;

namespace Expro.Hubs
{
    public class UserTrackerHub : Hub
    {
        private static ConcurrentDictionary<string, string> onlineUsers = new ConcurrentDictionary<string, string>();

        private readonly UserManager<ApplicationUser> _userManager;
        public UserTrackerHub(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public override async Task OnConnectedAsync()
        {
            //var context = Context;
            if (Context.User.Identity.IsAuthenticated)
            {
                if (UserHasNoConnections(Context.UserIdentifier))
                {
                    var user = await _userManager.FindByIdAsync(Context.UserIdentifier);
                    if (user != null)
                    {
                        user.IsOnline = true;
                        user.DateLastSeen = DateTime.Now;
                        await _userManager.UpdateAsync(user);
                    }
                }

                onlineUsers[Context.ConnectionId] = Context.UserIdentifier;
            }

            //await Groups.AddToGroupAsync(Context.ConnectionId, "SignalR Users");
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await Task.Delay(5000);

            if (Context.User.Identity.IsAuthenticated)
            {
                string userID;
                onlineUsers.TryRemove(Context.ConnectionId, out userID);

                if (!string.IsNullOrWhiteSpace(userID)
                    && UserHasNoConnections(userID))
                {
                    var user = await _userManager.FindByIdAsync(userID);
                    if (user != null)
                    {
                        user.IsOnline = false;
                        user.DateLastSeen = DateTime.Now;
                        await _userManager.UpdateAsync(user);
                    }
                }
            }
                

            //await Groups.RemoveFromGroupAsync(Context.ConnectionId, "SignalR Users");
            await base.OnDisconnectedAsync(exception);
        }

        private static bool UserHasNoConnections(string userID)
        {
            return !onlineUsers.Values.Contains(userID);
        }
    }
}
