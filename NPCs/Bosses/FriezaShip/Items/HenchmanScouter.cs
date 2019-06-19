using DBT.Items;
using DBT.Items.Consumables;
using DBT.Items.KiStones;
using DBT.Items.Materials;
using DBT.Items.Materials.Metals;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DBT.NPCs.Bosses.FriezaShip.Items
{
    public sealed class HenchmanScouter : DBTConsumable
    {
        public HenchmanScouter() : base("Henchman's Scouter", "A common scouter used by the lower echelons of the frieza force, it appears to have a communicator built into it.", 48, 26, 0, ItemRarityID.Orange, 4, true, null, 20, 20) 
        {
        }
        public override bool CanUseItem(Player player)

        {
            return NPC.downedBoss2 && !NPC.AnyNPCs(mod.NPCType<FriezaShip>()) && !DBTWorld.friezaShipTriggered;
        }

        public override bool UseItem(Player player)
        {
            NPC.SpawnOnPlayer(player.whoAmI, mod.NPCType<FriezaShip>());
            Main.PlaySound(SoundID.Roar, player.position, 0);
            return true;
        }

		public override void AddRecipes()
		{
			ModRecipe r = new ModRecipe(mod);
			r.AddIngredient(mod.ItemType<CyberneticParts>(), 8);
            r.AddIngredient(mod.ItemType<ScrapMetal>(), 5);
            r.AddIngredient(mod.ItemType<KiStoneT1>(), 3);
            r.SetResult(this);
			r.AddRecipe();
		}
    }
}