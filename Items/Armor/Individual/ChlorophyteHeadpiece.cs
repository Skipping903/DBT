using DBT.Commons.Players;
using DBT.Players;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace DBT.Items.Armor.Individual
{
    [AutoloadEquip(EquipType.Head)]
    public sealed class ChlorophyteHeadpiece : DBTArmorPiece, IHandleOnPlayerPreHurt
    {
        public ChlorophyteHeadpiece() : base("Chlorophyte Headpiece",
            "16% Increased Ki Damage"
            + "\n12% Increased Ki Crit Chance" +
            "\nMaximum Ki increased by 500",
            24, 16, Item.buyPrice(silver: 60), 11, ItemRarityID.Lime)
        {
        }

        public override bool IsArmorSet(Item head, Item body, Item legs) => base.IsArmorSet(head, body, legs) && body.type == ItemID.ChlorophytePlateMail && legs.type == ItemID.ChlorophyteGreaves;

        public override void UpdateArmorSet(Player player)
        {
            base.UpdateArmorSet(player);

            player.setBonus = "Getting hit gives greatly increased life regen and ki regen.";
        }

        public override void UpdateEquip(Player player)
        {
            base.UpdateEquip(player);
            DBTPlayer dbtPlayer = player.GetModPlayer<DBTPlayer>();

            dbtPlayer.KiDamageMultiplier += 0.16f;
            dbtPlayer.KiCritAddition += 12;
            dbtPlayer.MaxKiModifier += 500;
        }

        public bool OnPlayerPreHurt(DBTPlayer dbtPlayer, bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit, ref bool customDamage, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
        {
            int buffType = mod.BuffType(nameof(ChlorophyteHeadpiece));

            if (!dbtPlayer.player.HasBuff(buffType))
                dbtPlayer.player.AddBuff(buffType, 180);

            return true;
        }

        public override void AddRecipes()
        {
            base.AddRecipes();

            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(ItemID.ChlorophyteBar, 12);
            recipe.AddTile(TileID.MythrilAnvil);

            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}