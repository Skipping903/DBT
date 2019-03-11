using System.Collections.Generic;
using DBTRMod.Players;

namespace DBTRMod.Transformations
{
    public class PlayerTransformation : IPlayerSavable
    {
        internal const string MASTERY_PREFIX = DBTModPlayer.DBTMOD_PREFIX + "Mastery_";

        public PlayerTransformation(TransformationDefinition definition, float currentMastery = 0f)
        {
            Definition = definition;
            CurrentMastery = currentMastery;
        }

        #region Player Hooks

        public void OnPlayerMasteryGain(DBTModPlayer player, float gain)
        {
            CurrentMastery += gain;
            Definition.OnPlayerMasteryGain(player, gain, CurrentMastery);
        }

        #endregion

        public KeyValuePair<string, object> ToSavableFormat() => new KeyValuePair<string, object>(MASTERY_PREFIX + Definition.UnlocalizedName, CurrentMastery);

        public TransformationDefinition Definition { get; }

        public float CurrentMastery { get; set; }
    }
}
