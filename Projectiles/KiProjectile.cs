using System.Collections.Generic;
using DBT.Players;
using Terraria;
using Terraria.ID;

namespace DBT.Projectiles
{
    public abstract class KiProjectile : DBTProjectile
    {
        private static readonly List<int> _damageHalvedAgainst = new List<int>();

        static KiProjectile()
        {
            AddNPCToHalfDamageAgainst(NPCID.EaterofWorldsTail, NPCID.EaterofWorldsHead, NPCID.EaterofWorldsBody);
            AddNPCToHalfDamageAgainst(NPCID.TheDestroyerTail, NPCID.TheDestroyer, NPCID.TheDestroyerBody);
        }

        public static void AddNPCToHalfDamageAgainst(params int[] npcIDs)
        {
            for (int i = 0; i < npcIDs.Length; i++)
                _damageHalvedAgainst.Add(npcIDs[i]);
        }

        protected KiProjectile(int damage) : base(damage)
        {
        }

        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (Owner == null)
                Owner = Main.LocalPlayer.GetModPlayer<DBTPlayer>();

            if (Main.expertMode && _damageHalvedAgainst.Contains(target.type))
                damage /= 2;
        }
    }
}
