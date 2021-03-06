﻿using Terraria;
using Terraria.ID;

namespace DBT.Items.Accessories.Infusers
{
    public sealed class AmethystInfuser : Infuser
    {
        public AmethystInfuser() : base("Amethyst Ki Infuser", "Hitting enemies with Ki attacks inflicts shadowflame", Item.buyPrice(gold:1, silver: 31), ItemID.Amethyst)
        {
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            base.OnHitNPC(player, target, damage, knockBack, crit);

            target.AddBuff(BuffID.ShadowFlame, 300);
        }
    }
}