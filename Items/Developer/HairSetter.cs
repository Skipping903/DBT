using DBT.HairStyles;
using DBT.Players;
using Terraria;

namespace DBT.Items.Developer
{
    public sealed class HairSetter : DeveloperItem
    {
        public HairSetter() : base(40, 40)
        {
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hair Setter");
            Tooltip.SetDefault("Sets the player's hair to the code.");
        }

        public override bool UseItem(Player player)
        {
            DBTPlayer dbtPlayer = player.GetModPlayer<DBTPlayer>();

            HairStyle currentHairStyle = dbtPlayer.ChosenHairStyle;
            int nextIndex = HairStyleManager.Instance.GetIndex(currentHairStyle) + 1;

            if (nextIndex >= HairStyleManager.Instance.Count)
                nextIndex = 0;

            dbtPlayer.ChosenHairStyle = HairStyleManager.Instance[nextIndex];

            return true;
        }
    }
}
