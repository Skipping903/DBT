using DBT.Items.KiStones;
using DBT.Players;
using DBT.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DBT.Items.Consumables.BukuujustuVols
{
    public abstract class BukuujustuVol : DBTConsumable
    {
        protected BukuujustuVol(string displayName, string tooltip, int rarity) :
            base(displayName, tooltip, 40, 40, 0, rarity, ItemUseStyleID.EatingUsing, true, SoundID.Item3, 17, 17)
        {
        }

        public override void SetDefaults()
        {
            base.SetDefaults();

            item.maxStack = 1;
        }
    }
}