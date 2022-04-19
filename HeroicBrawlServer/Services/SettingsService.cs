using HeroicBrawlServer.Services.Abstractions;
using HeroicBrawlServer.Services.Models.Settings;
using System.Collections.Generic;
using System.Linq;

namespace HeroicBrawlServer.Services;

public class SettingsService : ISettingsService
{

    private ICollection<HeroSettings> _heroesSettings;

    public SettingsService()
    {
        // TODO: Store settings in database
        _heroesSettings = new List<HeroSettings>()
        {
            new HeroSettings()
            {
                Name = "Knight",
                BaseHp = 100,
                GroundAttackDamages = 10,
                AerialAttackDamages = 20,
                GroundAttackPushForceY = 5.0,
                GroundAttackPushForceZ = 20.0,
                AerialAttackPushForceY = 0.0,
                AerialAttackPushForceZ = 0.0,
                AirMovingSpeed = 5.0,
                DieAnimationDuration = 0.2,
                MovingSpeed = 10.0
            }
        };
    }

    public ICollection<HeroSettings> GetHeroesSettings()
    {
        return _heroesSettings;
    }

    public HeroSettings GetHeroSettings(string name)
    {
        var heroSettings = _heroesSettings.FirstOrDefault(x => x.Name == name);
        if (heroSettings == null)
        {
            throw new System.Exception($"Hero {name} not found");
        }

        return heroSettings;
    }
}
