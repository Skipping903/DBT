using System.Collections.Generic;
using Terraria;

namespace DBT.Players
{
    public sealed partial class DBTPlayer
    {
        private List<NPC> _aliveBosses;

        public List<NPC> AliveBosses
        {
            get
            {
                if (_aliveBosses == null)
                {
                    for (int i = 0; i < Main.npc.Length; i++)
                        if (Main.npc[i].boss && Main.npc[i].active)
                            _aliveBosses.Add(Main.npc[i]);
                }

                return _aliveBosses;
            }
        }
    }
}