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
                JumpForce = 200,
                MovingSpeed = 10.0,
                AirMovingSpeed = 5.0,

                GroundAttackDamages = 10,
                GroundAttackPushForceY = 20.0,
                GroundAttackPushForceZ = 50.0,
                GroundAttackMovementForceY = 0.0,
                GroundAttackMovementForceZ = 0.0,

                AerialAttackDamages = 20,
                AerialAttackPushForceY = 0.0,
                AerialAttackPushForceZ = 0.0,
                AerialAttackMovementForceY = -500.0,
                AerialAttackMovementForceZ = 0.0,

                DieAnimationDuration = 1.5
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
