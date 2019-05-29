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
        protected SkillItem(SkillDefinition definition, int width, int height, int rarity, bool autoReuse) : base(definition.DisplayName, definition.Description, width, height, rarity)
        {
            Definition = definition;

            AutoReuse = autoReuse;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();

            DBTPlayer dbtPlayer = Main.LocalPlayer.GetModPlayer<DBTPlayer>();

            item.shoot = mod.ProjectileType<TProjectile>();
            item.shootSpeed = Definition.Characteristics.GetShootSpeed(dbtPlayer);

            item.damage = (int) Definition.Characteristics.GetDamage(dbtPlayer, 1);
            item.autoReuse = AutoReuse;
        }

        public SkillDefinition Definition { get; }

        public bool AutoReuse { get; }
    }
}