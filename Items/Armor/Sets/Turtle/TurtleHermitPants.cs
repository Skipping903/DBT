using DBT.Items.Materials;
using DBT.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DBT.Items.Armor.Sets.Turtle
{
    [AutoloadEquip(EquipType.Legs)]
    public class TurtleHermitPants : DBTItem
    {
        public TurtleHermitPants() : base("Turtle Hermit Pants", 
            "4% Increased Ki damage" +
            "\n4% Increased Ki knockback" +
            "\n+10% Increased movement speed",
            28, 18, value: Item.buyPrice(silver: 22), defense: 6, rarity: ItemRarityID.Orange)
        {
        }

        public override void UpdateEquip(Player player)
        {
            DBTPlayer dbtPlayer = player.GetModPlayer<DBTPlayer>();

            dbtPlayer.KiDamageMultiplier += 0.04f;
            dbtPlayer.KiKnockbackAddition += 0.04f;

            player.moveSpeed += 0.10f;
        }

        public override void AddRecipes()
        {
            base.AddRecipes();
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(ItemID.Silk, 16);
            recipe.AddIngredient(mod, nameof(EarthenShard), 10);
            recipe.AddTile(TileID.Anvils);

            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}