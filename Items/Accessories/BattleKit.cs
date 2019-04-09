﻿using DBT.Items.Accessories.Scouters;
using DBT.Players;
using DBT.Tiles;
using Terraria;
using Terraria.ModLoader;

namespace DBT.Items.Accessories
{
    public sealed class BattleKit : DBTItem
    {
        public BattleKit() : base("Battle Kit", "The essence of strong defense.\nCharging grants a protective barrier that grants drastically increased defense.")
        {
        }

        public override void SetDefaults()
        {
            base.SetDefaults();

            item.width = 18;
            item.height = 30;
            item.value = 40 * Constants.SILVER_VALUE_MULTIPLIER;
            item.rare = 4;
            item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            base.UpdateAccessory(player, hideVisual);
            DBTPlayer dbtPlayer = player.GetModPlayer<DBTPlayer>();

            player.detectCreature = true;

            dbtPlayer.KiDamageMultiplier += 0.06f;
            dbtPlayer.KiChargeRate += 1f;
            dbtPlayer.
        }

        public override void AddRecipes()
        {
            base.AddRecipes();

            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(mod, nameof(ScouterMK1));
            recipe.AddIngredient(mod, nameof(WornGloves));
            recipe.AddIngredient(mod, nameof(ArmCannon));
            
            recipe.AddIngredient(mod, nameof(ZTableTile));
            recipe.SetResult(this);

            recipe.AddRecipe();
        }
    }
}