using Terraria;

namespace DBT.Commons.Players
{
    public interface IHandleOnPlayerHitNPC
    {
        void OnPlayerHitNPC(Item withItem, NPC target, ref int damage, ref float knockback, ref bool crit);
    }
}