using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DBT.Items.Consumables.ITTomes
{
    public abstract class ITTomeVol : DBTConsumable
    {
        protected ITTomeVol(string displayName, string tooltip, int rarity) :
            base(displayName, tooltip, 28, 36, 0, rarity, ItemUseStyleID.EatingUsing, true, null, 17, 17)
        {
        }

        public override void SetDefaults()
        {
            base.SetDefaults();

            item.maxStack = 1;

            if (!Main.dedServ)
                item.UseSound = mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Bookread").WithPitchVariance(0.1f);
        }
    }
}