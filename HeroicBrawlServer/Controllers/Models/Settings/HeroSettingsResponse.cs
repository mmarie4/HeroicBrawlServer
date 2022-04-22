using HeroicBrawlServer.Services.Models.Settings;

namespace HeroicBrawlServer.Controllers.Models.Settings
{
    public class HeroSettingsResponse
    {
        public string Name { get; set; }
        public int BaseHp { get; set; }

        public double JumpForce { get; set; }

        public double MovingSpeed { get; set; }
        public double AirMovingSpeed { get; set; }

        public double GroundAttackDamages { get; set; }
        public double GroundAttackPushForceZ { get; set; }
        public double GroundAttackPushForceY { get; set; }
        public double GroundAttackMovementForceY { get; set; }
        public double GroundAttackMovementForceZ { get; set; }

        public double AerialAttackDamages { get; set; }
        public double AerialAttackPushForceZ { get; set; }
        public double AerialAttackPushForceY { get; set; }
        public double AerialAttackMovementForceY { get; set; }
        public double AerialAttackMovementForceZ { get; set; }

        public double DieAnimationDuration { get; set; }

        public static HeroSettingsResponse FromEntity(HeroSettings entity)
        {
            return new HeroSettingsResponse()
            {
                Name = entity.Name,
                BaseHp = entity.BaseHp,

                JumpForce = entity.JumpForce,
                MovingSpeed = entity.MovingSpeed,
                AirMovingSpeed = entity.AirMovingSpeed,

                GroundAttackDamages = entity.GroundAttackDamages,
                GroundAttackPushForceY = entity.GroundAttackPushForceZ,
                GroundAttackPushForceZ = entity.GroundAttackPushForceY,
                GroundAttackMovementForceY = entity.GroundAttackMovementForceY,
                GroundAttackMovementForceZ = entity.GroundAttackMovementForceZ,

                AerialAttackDamages = entity.AerialAttackDamages,
                AerialAttackPushForceY = entity.AerialAttackPushForceY,
                AerialAttackPushForceZ = entity.AerialAttackPushForceZ,
                AerialAttackMovementForceY = entity.AerialAttackMovementForceY,
                AerialAttackMovementForceZ = entity.AerialAttackMovementForceZ,

                DieAnimationDuration = entity.DieAnimationDuration,
            };
        }
    }
}
