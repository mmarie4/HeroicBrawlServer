using HeroicBrawlServer.Controllers.Models.Settings;
using HeroicBrawlServer.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeroicBrawlServer.Controllers
{
    [ApiController]
    [Route("api/settings")]
    [Authorize]
    public class SettingsController : Controller
    {

        private readonly ISettingsService _settingsService;

        public SettingsController(ISettingsService settingsService)
        {
            _settingsService = settingsService;
        }

        /// <summary>
        ///     Logs in an user
        /// </summary>
        /// <param name="loginRequest"></param>
        /// <returns></returns>
        [HttpGet("heroes")]
        public async Task<IEnumerable<HeroSettingsResponse>> GetHeroesSettings()
        {
            var heroesSettings = _settingsService.GetHeroesSettings();

            return heroesSettings.Select(x => HeroSettingsResponse.FromEntity(x));
        }
    }
}
