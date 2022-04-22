namespace HeroicBrawlServer.Services.Models.Settings
{
    public class HeroSettings
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
    }
}
