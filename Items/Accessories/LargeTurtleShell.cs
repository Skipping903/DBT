using DBT.Items.KiStones;
using DBT.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DBT.Items.Accessories
{
    [AutoloadEquip(EquipType.Back)]
    public sealed class LargeTurtleShell : DBTAccessory
    {
        public LargeTurtleShell() : base("Large Turtle Shell", 
            "'A turtle shell with an odd resemblence to the turtle hermit.'" + 
            "\n7% increased ki damage, 8% increased ki knockback.",
            20, 30, value: Item.buyPrice(gold: 1, silver: 40), defense: 9, rarity: ItemRarityID.Cyan)
        {
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            base.UpdateAccessory(player, hideVisual);
            DBTPlayer dbtPlayer = player.GetModPlayer<DBTPlayer>();

            dbtPlayer.KiDamageMultiplier += 0.07f;
            dbtPlayer.KiKnockbackAddition += 0.08f;
        }

        public override void AddRecipes()
        {
            base.AddRecipes();

            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(mod, nameof(KiStoneT3), 2);
            recipe.AddIngredient(ItemID.TurtleShell, 3);
            recipe.AddTile(TileID.MythrilAnvil);

            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}