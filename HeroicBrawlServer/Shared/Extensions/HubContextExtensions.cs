using HeroicBrawlServer.Services;
using HeroicBrawlServer.Services.Models.Messages;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace HeroicBrawlServer.Shared.Extensions
{
    public static class HubContextExtensions
    {
        public static async Task SendToRoom(this IHubContext<RealtimeHub> context,
                                 Guid roomId,
                                 BaseMessage message)
        {
            await context.Clients.Group(roomId.ToString())
                                 .SendAsync(message.ToString());
        }
    }
}
