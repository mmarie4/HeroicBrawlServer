using HeroicBrawlServer.Services.Models.Messages;
using HeroicBrawlServer.Services.Models.Rooms.Cache;
using HeroicBrawlServer.Shared.Extensions;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HeroicBrawlServer.Services
{
    public class GameLoopHostedService : IHostedService
    {
        private ILogger<GameLoopHostedService> _logger;
        private Timer _timer;
        private TimeSpan TickDelay = TimeSpan.FromMilliseconds(33);

        private readonly IHubContext<RealtimeHub> _hubContext;

        public GameLoopHostedService(ILogger<GameLoopHostedService> logger, IHubContext<RealtimeHub> hubContext)
        {
            _logger = logger;
            _hubContext = hubContext;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(Broadcast, null, TickDelay, TickDelay);

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        /// <summary>
        ///     For each room, broadcast message for game state
        /// </summary>
        /// <returns></returns>
        private async void Broadcast(object state)
        {
            foreach (var room in Cache.Rooms)
            {
                var stateMessage = new GameStateMessage(room.GameState, room.Map);
                await _hubContext.Clients.SendToRoom(room.Id, stateMessage);
            }
        }
    }
}
