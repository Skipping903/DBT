using System;
using Terraria;

namespace DBT.Players
{
    public sealed partial class DBTPlayer
    {
        internal void ResetEffectsGuardian()
        {
            BaseHealingBonus = 0;
        }

        public int BaseHealingBonus { get; set; } = 0;

        public void TeamBuffAdd(int buffid = 0, string modbuffid = null, int duration = 0)
        {
            for (int playerIndex = 0; playerIndex < 255; playerIndex++)
            {
                Player player = Main.player[playerIndex];

                if(player != null && !player.dead && player.active)
                {
                    bool isSameTeam = Main.player[Main.myPlayer].team == player.team && player.team != 0;
                    if(isSameTeam)
                    {
                        if(buffid != 0)
                        {
                            player.AddBuff(buffid, duration);
                        }
                        else if(modbuffid != null)
                        {
                            player.AddBuff(mod.BuffType(modbuffid), duration);
                        }
                    }
                }
            }
        }
    }
}
