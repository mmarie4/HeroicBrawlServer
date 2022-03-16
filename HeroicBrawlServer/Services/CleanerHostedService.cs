using HeroicBrawlServer.Services.Models.Rooms.Cache;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HeroicBrawlServer.Services
{
    public class CleanerHostedService : IHostedService
    {
        private ILogger<CleanerHostedService> _logger;
        private Timer _timer;
        private TimeSpan _emptyRoomTTL;

        public CleanerHostedService(ILogger<CleanerHostedService> logger)
        {
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("CleanerHostedService: Starting timer");
            _timer = new Timer(Clean, null, 0, 10000);
            _emptyRoomTTL = new TimeSpan(0, 1, 0);

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("CleanerHostedService: Stopping timer");
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        /// <summary>
        ///     Delete rooms with no players inside
        ///     Allow TTL after creation, to avoid deleting rooms just created before the creator joins it
        /// </summary>
        private void Clean(object state)
        {
            foreach (var room in Cache.Rooms)
            {
                if (!room.GameState.Players.Any() &&
                    (DateTime.UtcNow - room.CreatedAt) > _emptyRoomTTL)
                {
                    Cache.RemoveRoom(room);
                };
            }
        }
    }
}
