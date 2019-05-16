using Terraria;

namespace DBT.Skills.Beams
{
    public abstract class BaseBeamItem<T> : BaseSkillItem
    {
        protected BaseBeamItem(string displayName, string tooltip, int width, int height, int rarity) : base(displayName, tooltip, width, height, rarity)
        {
        }

        public override bool? CanHitNPC(Player ply, NPC target)
        {
            return false;
        }
    }
}