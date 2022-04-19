using HeroicBrawlServer.Services.Models.Settings;
using System.Collections.Generic;

namespace HeroicBrawlServer.Services.Abstractions;

public interface ISettingsService
{
    ICollection<HeroSettings> GetHeroesSettings();
    HeroSettings GetHeroSettings(string name);
}
