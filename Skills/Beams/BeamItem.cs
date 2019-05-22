using DBT.Helpers;
using Terraria;
using Terraria.ID;

namespace DBT.Skills.Beams
{
    public abstract class BeamItem<T> : SkillItem
    {
        protected BeamItem(string displayName, string tooltip, int width, int height, int rarity, string displayedWeaponType, float kiDrain) : base(displayName, tooltip, width, height, rarity, displayedWeaponType, kiDrain)
        {
        }

        public override bool? CanHitNPC(Player ply, NPC target)
        {
            return false;
        }

        public override bool AltFunctionUse(Player player)
        {
            player.channel = true;

            if (Main.netMode != NetmodeID.MultiplayerClient || Main.myPlayer == player.whoAmI)
            {
                if (ProjectileHelper.)
            }

            return base.AltFunctionUse(player);
        }
    }
}