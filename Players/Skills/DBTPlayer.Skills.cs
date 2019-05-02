namespace DBT.Players
{
    public sealed partial class DBTPlayer
    {
        internal void ResetSkillEffects()
        {
            SkillChargeLimitModifier = 0;
        }

        public int SkillChargeLimitModifier { get; set; }
    }
}
