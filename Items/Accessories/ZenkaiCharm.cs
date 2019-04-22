using DBT.Buffs;
using DBT.Commons.Players;
using DBT.Items.KiStones;
using DBT.Items.Materials;
using DBT.Players;
using DBT.Tiles;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace DBT.Items.Accessories
{
    public sealed class ZenkaiCharm : DBTAccessory, IHandleOnPlayerPreKill
    {
        public ZenkaiCharm() : base("Zenkai Charm",
            "'A charm that harnesses the true power of a saiyan.'" +
            "\n8% increased ki damage" +
            "\nTaking fatal damage will instead return you to 50 hp" +
            "\nand grant x2 damage for a short time." +
            "\n2 Minute cooldown",
            18, 30, value: Item.buyPrice(gold: 2, silver: 70), rarity: ItemRarityID.Pink)
        {
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            base.UpdateAccessory(player, hideVisual);
            player.GetModPlayer<DBTPlayer>().KiDamageMultiplier += 0.08f;
        }

        public bool OnPlayerPreKill(DBTPlayer dbtPlayer, ref double damage, ref int hitDirection, ref bool pvp, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
        {
            dbtPlayer.player.statLife = 50;
            dbtPlayer.player.HealEffect(dbtPlayer.player.statLifeMax + dbtPlayer.player.statLifeMax2);
            dbtPlayer.player.AddBuff(mod.BuffType<ZenkaiCharmBuff>(), 600);

            return false;
        }

        public override void AddRecipes()
        {
            base.AddRecipes();

            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(mod, nameof(KiStoneT4), 3);
            recipe.AddIngredient(mod, nameof(SoulofEntity), 16);
            recipe.AddIngredient(ItemID.GoldBar, 12);
            recipe.AddIngredient(ItemID.Obsidian, 8);
            recipe.AddTile(mod, nameof(ZTableTile));

            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}