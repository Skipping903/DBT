using DBT.Items.Materials;
using DBT.Players;
using DBT.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DBT.Items.Accessories.Earthens
{
    public sealed class EarthenScarab : EarthenItem
    {
        public EarthenScarab() : base("Earthen Scarab",
            "'The soul of the land seems to give it life.'" +
            "\n4% Increased ki damage" +
            "\nIncreased flight speed" +
            "\nThe longer you charge the more ki you charge, limits at +500%",
            22, 34, value: Item.buyPrice(gold: 7, silver: 20), defense: 4, rarity: ItemRarityID.LightRed)
        {
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            base.UpdateAccessory(player, hideVisual);
            DBTPlayer dbtPlayer = player.GetModPlayer<DBTPlayer>();

            dbtPlayer.KiDamageMultiplier += 0.04f;
            dbtPlayer.FlightSpeedModifier += 0.1f;
        }

        public override void AddRecipes()
        {
            base.AddRecipes();

            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(ItemID.StoneBlock, 100);
            recipe.AddIngredient(mod, nameof(AstralEssentia), 10);
            
            recipe.AddTile(mod, nameof(ZTableTile));
            recipe.SetResult(this);

            recipe.AddRecipe();
        }
    }
}