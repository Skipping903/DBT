using DBT.Items.KiStones;
using DBT.Players;
using DBT.Tiles.Stations;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DBT.Items.Accessories.Earthens
{
    public sealed class EarthenSigil : EarthenItem
    {
        public EarthenSigil() : base("Earthen Sigil",
            "'The soul of the land lives within'" +
            "\n6% Increased ki damage" +
            "\nIncreased ki regen" +
            "\nReduced flight ki usage" +
            "\n+1 Max Charges",
            22, 34, value: Item.buyPrice(gold: 6), defense: 4, rarity: ItemRarityID.LightRed)
        {
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            base.UpdateAccessory(player, hideVisual);
            DBTPlayer dbtPlayer = player.GetModPlayer<DBTPlayer>();

            dbtPlayer.KiDamageMultiplier += 0.06f;
            dbtPlayer.ExternalKiRegeneration += 1;
            dbtPlayer.FlightKiUsageModifier += 1;
            dbtPlayer.KiChargeRateMultiplierLimit += 1;
        }

        public override void AddRecipes()
        {
            base.AddRecipes();

            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(ItemID.StoneBlock, 50);
            //recipe.AddIngredient(mod, nameof(EarthenShard), 10);
            recipe.AddIngredient(mod, nameof(KiStoneT3), 3);

            recipe.AddTile(mod, nameof(ZTableTile));
            recipe.SetResult(this);
            
            recipe.AddRecipe();
        }
    }
}