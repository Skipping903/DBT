using DBT.Commons.Items;
using DBT.Players;
using DBT.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DBT.Items.Accessories
{
    public sealed class TimeRing : DBTAccessory, IIsPatreonItem
    {
        public TimeRing() : base("Time Ring",
            "'The sacred ring of the kais.'" +
            "\nDrastically increased health regen" +
            "\nDrastically increased ki regen",
            26, 26, value: Item.buyPrice(gold: 1, silver: 20), rarity: ItemRarityID.LightPurple)
        {
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            base.UpdateAccessory(player, hideVisual);
            DBTPlayer dbtPlayer = player.GetModPlayer<DBTPlayer>();

            player.lifeRegen += 4;
            dbtPlayer.ExtraKiRegeneration += 3;
        }

        public override void AddRecipes()
        {
            base.AddRecipes();

            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(ItemID.TitaniumBar, 8);
            recipe.AddIngredient(ItemID.SoulofLight, 5);
            recipe.AddIngredient(ItemID.SoulofNight, 5);
            recipe.AddTile(mod, nameof(KaiTableTile));
            
            recipe.SetResult(this);
            recipe.AddRecipe();

            recipe.AddIngredient(ItemID.AdamantiteBar, 8);
            recipe.AddIngredient(ItemID.SoulofLight, 5);
            recipe.AddIngredient(ItemID.SoulofNight, 5);

            recipe.AddTile(mod, nameof(KaiTableTile));

            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public string PatreonDonor => "Lethaius";
    }
}