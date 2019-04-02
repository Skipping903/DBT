using DBT.Players;
using Terraria;
using Terraria.ID;

namespace DBT.Items.Accessories.Necklaces.GemNecklaces
{
    public sealed class SapphireGemNecklace : GemNecklace
    {
        public SapphireGemNecklace() : base("Sapphire Necklace", "9% increased ki damage and +100 max ki.", 65 * Constants.SILVER_VALUE_MULTIPLIER, 0, ItemID.Sapphire)
        {
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            DBTPlayer dbtPlayer = player.GetModPlayer<DBTPlayer>();

            dbtPlayer.KiDamage += 0.09f;
            dbtPlayer.MaxKiModifier += 100;
        }
    }
}