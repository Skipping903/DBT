using Terraria;
using Terraria.ModLoader;
using DBT.Items;
using DBT.NPCs.Bosses.FriezaShip.Items;

namespace DBT.NPCs.Bosses.FriezaShip.Items
{
    public class FFShipBag : DBTItem
    {
        public FFShipBag() : base("Treasure Bag", "{$CommonItemTooltip.RightClickToOpen}", 32, 32, 0, 0, 4)
        {
        }

        public override void SetDefaults()
        {
            item.maxStack = 999;
            item.consumable = true;
            item.expert = true;
            bossBagNPC = mod.NPCType("FriezaShip");
        }

        public override bool CanRightClick()
        {
            return true;
        }

        public override void OpenBossBag(Player player)
        {
            player.TryGettingDevArmor();
            int choice = Main.rand.Next(1);

            if (choice == 0)
            {
                //player.QuickSpawnItem(mod.ItemType<BeamRifle>());
            }
            if (choice == 1)
            {
                //player.QuickSpawnItem(mod.ItemType<HenchBlast>());
            }
            //player.QuickSpawnItem(mod.ItemType<CyberneticParts>(), Main.rand.Next(7, 18));
            player.QuickSpawnItem(mod.ItemType<ArmCannonMK2>());
        }
    }
}