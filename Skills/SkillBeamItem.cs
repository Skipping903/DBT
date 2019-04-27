using DBT.Items;
using DBT.Players;
using Terraria;
using Terraria.ID;

namespace DBT.Skills
{
    public abstract class SkillBeamItem : DBTItem
    {
        private readonly int _damage, _projectile, _useAnimation, _useTime;
        private readonly float _knockBack, _shootSpeed;

        protected SkillBeamItem(string displayName, string tooltip, int width, int height, int rarity, int kiCost, int damage, float knockBack, float shootSpeed, int projectile, int useAnimation, int useTime) : 
            base(displayName, tooltip, width, height, 0, 0, rarity)
        {
            BaseKiCost = kiCost;

            _damage = damage;
            _knockBack = knockBack;
            _shootSpeed = shootSpeed;

            _projectile = projectile;

            _useAnimation = useAnimation;
            _useTime = useTime;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();

            item.damage = _damage;
            item.knockBack = _knockBack;
            item.shootSpeed = _shootSpeed;

            item.shoot = _projectile;

            item.useStyle = ItemUseStyleID.HoldingOut;
            item.UseSound = SoundID.Item12;
            item.useAnimation = _useAnimation;
            item.useTime = _useTime;

            item.noUseGraphic = true;
            item.autoReuse = false;
            item.channel = true;
        }

        public override void HoldItem(Player player)
        {
            // Set the ki weapon held var
            // TODO Figure out if this is required.
            // player.GetModPlayer<DBTPlayer>().isHoldingKiWeapon = true;
        }

        public override bool CanUseItem(Player player)
        {
            player.channel = true;

            if (Main.netMode != NetmodeID.MultiplayerClient || Main.myPlayer == player.whoAmI)
            {
                int weaponDamage = item.damage;
                GetWeaponDamage(player, ref weaponDamage);

                Projectile.NewProjectileDirect(player.position, player.position, item.shoot, weaponDamage, item.knockBack, player.whoAmI);
            }

            return false;
        }

        public virtual float GetKiCost(DBTPlayer dbtPlayer)
        {
            return BaseKiCost;
        }

        public float BaseKiCost { get; }
    }
}