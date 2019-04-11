using DBT.Players;
using Terraria;

namespace DBT.Items.Developer
{
    public sealed class KiAdder : DeveloperItem
    {
        public KiAdder() : base(24, 24)
        {
        }

        public override bool UseItem(Player player)
        {
            DBTPlayer dbtPlayer = player.GetModPlayer<DBTPlayer>();

            dbtPlayer.BaseMaxKi += 1000;
            return true;
        }
    }
}