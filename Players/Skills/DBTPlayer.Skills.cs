namespace DBT.Players
{
    public sealed partial class DBTPlayer
    {
        internal void ResetSkillEffects()
        {
            SkillChargeLevelLimitModifier = 0;
        }

        public int SkillChargeLevelLimitModifier { get; set; }
        public int SkillChargeLevelLimitMultiplier { get; set; }
    }
}
