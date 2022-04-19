using HeroicBrawlServer.Services.Models.Settings;

namespace HeroicBrawlServer.Controllers.Models.Settings
{
    public class HeroSettingsResponse
    {
        public string Name { get; set; }
        public int BaseHp { get; set; }
        public double MovingSpeed { get; set; }
        public double AirMovingSpeed { get; set; }
        public double GroundAttackDamages { get; set; }
        public double AerialAttackDamages { get; set; }
        public double GroundAttackPushForceZ { get; set; }
        public double GroundAttackPushForceY { get; set; }
        public double AerialAttackPushForceZ { get; set; }
        public double AerialAttackPushForceY { get; set; }
        public double DieAnimationDuration { get; set; }

        public static HeroSettingsResponse FromEntity(HeroSettings entity)
        {
            return new HeroSettingsResponse()
            {
                Name = entity.Name,
                BaseHp = entity.BaseHp,
                MovingSpeed = entity.MovingSpeed,
                AirMovingSpeed = entity.AirMovingSpeed,
                GroundAttackDamages = entity.GroundAttackDamages,
                AerialAttackDamages = entity.AerialAttackDamages,
                GroundAttackPushForceY = entity.GroundAttackPushForceZ,
                GroundAttackPushForceZ = entity.GroundAttackPushForceY,
                AerialAttackPushForceY = entity.AerialAttackPushForceY,
                AerialAttackPushForceZ = entity.AerialAttackPushForceZ,
                DieAnimationDuration = entity.DieAnimationDuration,
            };
        }
    }
}
