using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBT.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using DBT.Projectiles;

namespace DBT.NPCs.Bosses.FriezaShip.Items
{
    public class BeamRifle : DBTItem
    {
        public BeamRifle() : base("Beam Rifle", "A simple laser rifle used by the lower echelons of the frieza force.", 44, 18, Item.buyPrice(gold: 1, silver: 40), 0, 4)
        {
        }

        public override void SetDefaults()
        {
            base.SetDefaults();

            item.ranged = true;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType<FFBeamProjectile>();
            item.shootSpeed = 20f;
            item.useTime = 9;
            item.damage = 15;
            item.useAnimation = 12;
            item.useStyle = 5;
            item.noMelee = true;
            item.knockBack = 8f;
            if (!Main.dedServ)
            {
                item.UseSound = mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/FFRiflefire").WithVolume(0.5f).WithPitchVariance(0.1f);
            }
        }
    }
}
