using System.Collections.Generic;
using DBT.Players;

namespace DBT.Transformations
{
    public class PlayerTransformation : IPlayerSavable
    {
        internal const string MASTERY_PREFIX = "Mastery_";

        public PlayerTransformation(TransformationDefinition definition, float currentMastery = 0f)
        {
            Definition = definition;
            CurrentMastery = currentMastery;
        }


        #region Player Hooks

        public void OnPlayerMasteryGain(DBTPlayer player, float gain)
        {
            CurrentMastery += gain;
            Definition.OnPlayerMasteryGain(player, gain, CurrentMastery);
        }

        #endregion


        public bool HasPlayerMastered(DBTPlayer dbtPlayer) => CurrentMastery >= Definition.GetMaxMastery(dbtPlayer);


        public KeyValuePair<string, object> ToSavableFormat() => new KeyValuePair<string, object>(MASTERY_PREFIX + Definition.UnlocalizedName, CurrentMastery);


        public TransformationDefinition Definition { get; }

        public float CurrentMastery { get; set; }

        public Dictionary<string, object> ExtraInformation { get; } = new Dictionary<string, object>();
    }
}
