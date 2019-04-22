using DBT.Buffs;
using DBT.Players;
using Terraria;
using Terraria.ID;

namespace DBT.Items.Accessories
{
    public sealed class GoblinKiEnhancer : DBTAccessory
    {
        public GoblinKiEnhancer() : base("Goblin Ki Enhancer",
            "'A relic of the ancient goblins.'" +
            "\n+500 Max ki" +
            "\nGetting hit grants massively increased ki regen for a short time.",
            18, 30, value: Item.buyPrice(silver: 60), rarity: ItemRarityID.Orange)
        {
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            base.UpdateAccessory(player, hideVisual);
            DBTPlayer dbtPlayer = player.GetModPlayer<DBTPlayer>();

            dbtPlayer.MaxKiModifier += 500;

            if (!player.HasBuff(mod.BuffType(nameof(EnhancedReservesBuff))))
                player.AddBuff(mod.BuffType(nameof(EnhancedReservesBuff)), 180);
        }
    }
}