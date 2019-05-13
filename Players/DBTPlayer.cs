using DBT.Transformations;
using Terraria;
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

        public void ApplySkillChargeSlowdown()
        {
            if (Flying)
            {
                float chargeMoveSpeedBonus = SkillChargeMoveSpeedModifier / 10f;
                float yVelocity = player.gravity + 0.001f;

                if ()
            }
        }

        public bool PlayerInitialized { get; private set; }

        public float HealthDrainMultiplier { get; set; }
    }
}
