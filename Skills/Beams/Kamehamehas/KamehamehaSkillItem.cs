using Terraria.ID;

namespace DBT.Skills.Beams.Kamehamehas
{
    public sealed class KamehamehaSkillItem : BaseBeamItem<KamehamehaCharge>
    {
        public KamehamehaSkillItem() : base("Kamehameha", "Maximum Charges = 6\nRight Click Hold to Charge\nLeft Click to Fire", 40, 40, ItemRarityID.Orange)
        {
        }

        public override void SetDefaults()
        {
            base.SetDefaults();

            item.shoot = mod.ProjectileType<>()
        }
    }
}