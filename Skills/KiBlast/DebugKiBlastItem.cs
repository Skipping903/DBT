using Terraria.ID;

namespace DBT.Skills.KiBlast
{
    public sealed class DebugKiBlastItem : SkillItem<DebugKiBlastProjectile>
    {
        public DebugKiBlastItem() : base(SkillDefinitionManager.Instance.DebugKiBlast, 20, 20, ItemRarityID.Blue, false)
        {
        }

        public override void SetDefaults()
        {
            base.SetDefaults();

            item.useAnimation = 22;
            item.useTime = 22;
            item.useStyle = ItemUseStyleID.Stabbing;
        }
    }
}