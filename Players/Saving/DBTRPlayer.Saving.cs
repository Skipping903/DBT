using Terraria.ModLoader.IO;

namespace DBTMod.Players
{
    public sealed partial class DBTRPlayer
    {
        public override TagCompound Save()
        {
            ClearTransformations();

            TagCompound tag = new TagCompound();

            tag.Add(nameof(PlayerInitialized), PlayerInitialized);

            SaveMastery(tag);
            SaveKi(tag);

            ForAllAcquiredTransformations(t => t.Definition.OnPlayerSaving(this, tag));

            return tag;
        }

        public override void Load(TagCompound tag)
        {
            PlayerInitialized = tag.GetBool(nameof(PlayerInitialized));

            LoadMastery(tag);
            LoadKi(tag);

            ForAllAcquiredTransformations(t => t.Definition.OnPlayerLoading(this, tag));
        }
    }
}
