using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;

namespace DBT.Items.Guardian.Accessories.Chakras
{
    public class ThroatChakra : GuardianItem
    {
        public ThroatChakra() : base("Throat Chakra", "Grants the compelling voice buff to players in the same party which increases their ki charge rate, ki damage by 10% and movement speed by 20% and increases all healing by the player by 10%.\n'Your voice innervates the energy of those around you.'")
        {
        }

        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 30;
            item.value = 74500;
            item.rare = ItemRarityID.LightRed;
            item.accessory = true;
        }
    }
}
