using DBT.Commons.Items.SenzuBeans;
using DBT.Players;
using Terraria;
using Terraria.ID;

namespace DBT.Items.Accessories
{
    public sealed class SenzuBag : DBTAccessory, IAffectSenzuBeanCooldown
    {
        public SenzuBag() : base("Senzu Bag", "Reduces cooldown on senzu bean consumption", 22, 16, value: Item.buyPrice(silver: 16), rarity: ItemRarityID.Orange)
        {
        }

        public void AffectSenzuBeanCooldown(DBTPlayer dbtPlayer, DBTItem dbtItem, ref float cooldown)
        {
            cooldown -= 3600;
        }
    }
}