using DBT.Transformations;
using Terraria.ModLoader.IO;

namespace DBT.Players
{
    public sealed partial class DBTPlayer
    {
        public override TagCompound Save()
        {
            ClearTransformations();

            TagCompound tag = new TagCompound();

            tag.Add(nameof(PlayerInitialized), PlayerInitialized);

            SaveMastery(tag);
            SaveTransformations(tag);
            SaveKi(tag);
            SaveGuardian(tag);

            TransformationDefinitionManager.Instance.ForAllItems(t => t.OnPreAcquirePlayerSaving(this, tag));
            ForAllAcquiredTransformations(t => t.Definition.OnPlayerSaving(this, tag));

            return tag;
        }

        public override void Load(TagCompound tag)
        {
            PlayerInitialized = tag.GetBool(nameof(PlayerInitialized));

            LoadMastery(tag);
            LoadTransformations(tag);
            LoadKi(tag);
            LoadGuardian(tag);

            TransformationDefinitionManager.Instance.ForAllItems(t => t.OnPreAcquirePlayerLoading(this, tag));
            ForAllAcquiredTransformations(t => t.Definition.OnPlayerLoading(this, tag));
        }
    }
}
