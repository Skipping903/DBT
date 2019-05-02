using Terraria;
using Terraria.ID;

namespace DBT.Items.Materials
{
    public sealed class SkeletalEssence : DBTMaterial
    {
        public SkeletalEssence() : base("Skeletal Essence", "'A chunk of the dungeon's inhabitants.'", 
            24, 24, Item.buyPrice(copper: 60), ItemRarityID.Orange)
        {
        }
    }
}