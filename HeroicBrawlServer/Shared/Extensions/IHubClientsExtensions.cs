using HeroicBrawlServer.Services;
using HeroicBrawlServer.Services.Models.Messages;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace HeroicBrawlServer.Shared.Extensions
{
    public static class IHubClientsExtensions
    {

        public static async Task SendToRoom(this IHubClients clients,
                                         Guid roomId,
                                         BaseMessage message)
        {
            await clients.Group(roomId.ToString())
                         .SendAsync(message.ToString());
        }
    }
}
