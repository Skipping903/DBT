using DBT.Buffs;
using DBT.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace DBT.Items.Consumables.KiPotions
{
    public abstract class KiPotion : DBTConsumable
    {
        protected KiPotion(string displayName, string tooltip, int restoredKi) : base(displayName, tooltip, 16, 24, Item.buyPrice(copper: 40), ItemRarityID.Orange, 2, true, SoundID.Item3, 12, 12)
        {
            RestoredKi = restoredKi;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();

            item.maxStack = 30;
        }

        public override bool CanUseItem(Player player) => base.CanUseItem(player) && !player.HasBuff(mod.BuffType(nameof(KiPotionSicknessBuff)));

        public override bool UseItem(Player player)
        {
            player.GetModPlayer<DBTPlayer>().ModifyKi(RestoredKi);

            player.AddBuff(mod.BuffType("KiPotionSickness"), 3600);
            CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y, player.width, player.height), new Color(51, 204, 255), RestoredKi, false, false);
            return true;
        }

        public int RestoredKi { get; }
    }
}