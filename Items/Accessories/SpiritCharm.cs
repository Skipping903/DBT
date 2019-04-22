using DBT.Items.Accessories.Necklaces.GemNecklaces;
using DBT.Players;
using DBT.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DBT.Items.Accessories
{
    public sealed class SpiritCharm : DBTAccessory
    {
        public SpiritCharm() : base("Spirit Charm",
            "'An emblem enscribed with markings of the dragon'"
            + "\n20% Increased Ki Damage" +
            "\nAll damage increased by 14%" +
            "\n+500 Maximum ki" +
            "\n12% reduced damage" +
            "\n+3 max minions" +
            "\nGreatly increased life regen" +
            "\nAll crit increased by 12%",
            24, 28, value: Item.buyPrice(gold: 2, silver: 80), defense: 4, rarity: ItemRarityID.Pink)
        {
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            base.UpdateAccessory(player, hideVisual);
            DBTPlayer dbtPlayer = player.GetModPlayer<DBTPlayer>();

            dbtPlayer.KiDamageMultiplier += 0.20f;
            dbtPlayer.KiCritAddition += 12;
            dbtPlayer.MaxKiModifier += 500;

            player.endurance += 0.12f;
            player.meleeDamage += 0.14f;
            player.meleeSpeed += 0.14f;
            player.meleeCrit += 12;

            player.magicDamage += 0.14f;
            player.magicCrit += 12;

            player.rangedDamage += 0.14f;
            player.rangedCrit += 12;

            player.lifeRegen += 3;
            player.minionDamage += 0.012f;
            player.maxMinions += 3;
        }

        public override void AddRecipes()
        {
            base.AddRecipes();

            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(mod, nameof(SpiritualEmblem));
            recipe.AddIngredient(mod, nameof(DragonGemNecklace));
            recipe.AddIngredient(ItemID.Ectoplasm, 12);
            recipe.AddTile(mod, nameof(KaiTableTile));

            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}