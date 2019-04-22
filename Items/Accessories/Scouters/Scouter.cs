using DBT.Players;
using Terraria;
using Terraria.ModLoader;

namespace DBT.Items.Accessories.Scouters
{
    [AutoloadEquip(EquipType.Head, EquipType.Face)]
    public abstract class Scouter : DBTAccessory
    {
        protected Scouter(string displayName, string tooltip, int value, int rarity, float kiDamageMultiplier) : base(displayName, tooltip, 24, 28, value, 0, rarity)
        {
            KiDamageMultiplier = kiDamageMultiplier;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();

            item.accessory = true;
        }

        public override void UpdateEquip(Player player)
        {
            base.UpdateEquip(player);

            GivePlayerBonuses(player);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            base.UpdateAccessory(player, hideVisual);

            GivePlayerBonuses(player);
        }

        public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
        {
            drawHair = true;
            drawAltHair = true;
        }

        private void GivePlayerBonuses(Player player)
        {
            DBTPlayer dbtPlayer = player.GetModPlayer<DBTPlayer>();

            dbtPlayer.KiDamageMultiplier += KiDamageMultiplier;
            player.detectCreature = true;
        }

        public float KiDamageMultiplier { get; }
    }
}