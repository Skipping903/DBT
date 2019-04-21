using Terraria;
using Terraria.ID;

namespace DBT.Items.Materials
{
    public sealed class EarthenShard : DBTMaterial
    {
        public EarthenShard() : base("Earthen Shard", "'A fragment of the land's soul.'",
            18, 30, Item.buyPrice(gold: 7, silver: 20), ItemRarityID.LightPurple)
        {
        }
    }
}