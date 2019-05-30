using System.Collections.Generic;
using DBT.Items;
using DBT.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DBT.Skills
{
    public abstract class SkillItem<TProjectile> : DBTItem where TProjectile : SkillProjectile
    {
        protected SkillItem(SkillDefinition definition, int width, int height, int rarity, bool autoReuse) : base(definition.DisplayName, definition.Description, width, height, rarity: rarity)
        {
            Definition = definition;

            AutoReuse = autoReuse;
        }


        public override void SetDefaults()
        {
            base.SetDefaults();

            item.noUseGraphic = true;

            item.shoot = mod.ProjectileType<TProjectile>();
            item.shootSpeed = Definition.Characteristics.BaseShootSpeed;

            item.damage = (int) Definition.Characteristics.BaseDamage;
            item.autoReuse = AutoReuse;

            item.knockBack = Definition.Characteristics.BaseKnockback;
        }

        public override void UpdateInventory(Player player)
        {
            base.UpdateInventory(player);

            DBTPlayer dbtPlayer = player.GetModPlayer<DBTPlayer>();

            // TODO Add charges.
            item.shootSpeed = Definition.Characteristics.GetShootSpeed(dbtPlayer, 1);
        }

        public override void GetWeaponDamage(Player player, ref int damage)
        {
            // TODO Add charges.
            Definition.Characteristics.GetDamage(player.GetModPlayer<DBTPlayer>(), ref damage, 1);

            base.GetWeaponDamage(player, ref damage);
        }

        public override void GetWeaponKnockback(Player player, ref float knockback)
        {
            Definition.Characteristics.GetKnockback(player.GetModPlayer<DBTPlayer>(), ref knockback, 1);

            base.GetWeaponKnockback(player, ref knockback);
        }


        public SkillDefinition Definition { get; }

        public bool AutoReuse { get; }
    }
}