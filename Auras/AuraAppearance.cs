using DBTR.Players;
using DBTR.Transformations;

namespace DBTR.Auras
{
    public class AuraAppearance
    {
        public AuraAppearance(AuraAnimationInformation information, LightingAppearance lighting)
        {
            Information = information;
            Lighting = lighting;
        }


        public virtual int GetTicksPerFrameTimerTick(DBTRPlayer dbtrPlayer)
        {
            if (dbtrPlayer.IsCharging)
                return Information.TicksPerFrameTimerTick * 2;

            return Information.TicksPerFrameTimerTick;
        }

        public AuraAnimationInformation Information { get; }

        public LightingAppearance Lighting { get; }
    }
}
