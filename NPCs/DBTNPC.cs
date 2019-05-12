using DBT.Commons.Items;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace DBT.NPCs
{
    public abstract class DBTNPC : ModNPC
    {
        private readonly string _displayName;

        protected DBTNPC(string displayName, int width, int height, int health, LegacySoundStyle hitSound, LegacySoundStyle deathSound, int aistyle, int aitype = 0, int defense = 0, int damage = 0, float value = 0f, float knockbackResist = 0f, int frameCount = 0)
        {
            _displayName = displayName;
            FrameCount = frameCount;

            Health = health;
            Defense = defense;
            Damage = damage;
            FrameCount = frameCount;
            HitSound = hitSound;
            DeathSound = deathSound;
            Value = value;
            KnockbackResist = knockbackResist;
            AiStyle = aistyle;
            AiType = aitype;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault(_displayName);
            Main.npcFrameCount[npc.type] = FrameCount;
        }

        public override void SetDefaults()
        {
            npc.defense = Defense;
            npc.lifeMax = Health;
            npc.damage = Damage;
            npc.HitSound = HitSound;
            npc.DeathSound = DeathSound;
            npc.value = Value;
            npc.knockBackResist = KnockbackResist;
            npc.aiStyle = AiStyle;
            aiType = AiType;
        }

        public int Health { get; }
        public int Defense { get; }
        public int FrameCount { get; }
        public int Damage { get; }
        public LegacySoundStyle HitSound { get; }
        public LegacySoundStyle DeathSound { get; }
        public float Value { get; }
        public float KnockbackResist { get; }
        public int AiStyle { get; }
        public int AiType { get; }
    }
}
