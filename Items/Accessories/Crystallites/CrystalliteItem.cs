using DBT.Players;
using Terraria;

namespace DBT.Items.Accessories.Crystallites
{
    public abstract class CrystalliteItem : DBTAccessory
    {
        protected CrystalliteItem(string displayName, string tooltip, int width, int height, int value, int rarity, float extraKi) : base(displayName, tooltip, width, height, value, 0, rarity)
        {
            ExtraKi = extraKi;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            base.UpdateAccessory(player, hideVisual);
            DBTPlayer dbtPlayer = player.GetModPlayer<DBTPlayer>();

            // TODO Add charging speed buff.
            dbtPlayer.MaxKiModifier += ExtraKi;
        }

        public float ExtraKi { get; }
    }
}