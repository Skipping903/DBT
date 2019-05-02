using Terraria;

namespace DBT.Items.Armor
{
    public abstract class DBTArmorPiece : DBTItem
    {
        protected DBTArmorPiece(string displayName, string tooltip, int width, int height, int value, int defense, int rarity) : base(displayName, tooltip, width, height, value, defense, rarity)
        {
        }

        public virtual bool IsArmorSet(Player player) => IsArmorSet(player.armor[0], player.armor[1], player.armor[2]);
    }
}