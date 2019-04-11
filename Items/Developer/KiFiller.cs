using DBT.Players;
using Terraria;

namespace DBT.Items.Developer
{
    public sealed class KiFiller : DeveloperItem
    {
        public KiFiller() : base(24, 24)
        {
        }

        public override bool UseItem(Player player)
        {
            DBTPlayer dbtPlayer = player.GetModPlayer<DBTPlayer>();

            dbtPlayer.ModifyKi(dbtPlayer.MaxKi - dbtPlayer.Ki);
            return true;
        }
    }
}