using DBT.Commons.Items;
using Terraria;
using Terraria.ID;

namespace DBT.Items.Accessories
{
    public sealed class GreenPotara : DBTAccessory, IIsPatreonItem
    {
        public GreenPotara() : base("Green Potara", "'A useful pair of earrings used by the kais.'", 24, 28, value: Item.buyPrice(gold: 2, silver: 2), rarity: ItemRarityID.Pink)
        {
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            base.UpdateAccessory(player, hideVisual);

            item.vanity = true;
        }

        public string PatreonDonor => "FullNovaAlchemist";
    }
}