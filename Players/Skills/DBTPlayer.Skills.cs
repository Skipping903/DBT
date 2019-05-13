namespace DBT.Players
{
    public sealed partial class DBTPlayer
    {
        internal void ResetSkillEffects()
        {
            SkillChargeLevelLimitModifier = 0;
            SkillChargeLevelLimitMultiplier = 1;

            SkillChargeMoveSpeedModifier = 1;
        }

        public int SkillChargeLevelLimitModifier { get; set; }
        public int SkillChargeLevelLimitMultiplier { get; set; }

        public float SkillChargeMoveSpeedModifier { get; set; }
    }
}
