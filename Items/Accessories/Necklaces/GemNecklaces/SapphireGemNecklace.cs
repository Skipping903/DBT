using DBT.Players;
using Terraria;
using Terraria.ID;

namespace DBT.Items.Accessories.Necklaces.GemNecklaces
{
    public sealed class SapphireGemNecklace : GemNecklace
    {
        public SapphireGemNecklace() : base("Sapphire Necklace", "5% increased Ki damage and +100 max Ki.", Item.buyPrice(silver: 65), 0, ItemID.Sapphire)
        {
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            DBTPlayer dbtPlayer = player.GetModPlayer<DBTPlayer>();

            dbtPlayer.KiDamageMultiplier += 0.05f;
            dbtPlayer.MaxKiModifier += 100;
        }
    }
}