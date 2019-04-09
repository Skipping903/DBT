using DBT.Network;
using DBT.Transformations;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DBT.Players
{
    public sealed partial class DBTPlayer : ModPlayer
    {
        public void OnKilledNPC(NPC npc)
        {
            TransformationDefinitionManager.Instance.ForAllItems(t => t.OnPreAcquirePlayerKilledNPC(this, npc));
            ForAllActiveTransformations(t => t.OnActivePlayerKilledNPC(this, npc));
        }

        public bool PlayerInitialized { get; private set; }
    }
}
