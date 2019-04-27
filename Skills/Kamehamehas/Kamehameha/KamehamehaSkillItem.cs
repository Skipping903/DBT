using Terraria.ID;

namespace DBT.Skills.Kamehamehas.Kamehameha
{
    public sealed class KamehamehaSkillItem : SkillBeamItem
    {
        public KamehamehaSkillItem() : base("Kamehameha", "Maximum Charges = 6\nHold Right Click to Charge\nHold Left Click to Fire", 40, 40, ItemRarityID.Orange, 80, 88, 2f, 0f, DBTMod.Instance.ProjectileType<KamehamehaCharge>(), 100, 100)
        {
        }
    }
}