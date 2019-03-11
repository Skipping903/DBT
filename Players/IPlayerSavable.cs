using System.Collections.Generic;

namespace DBTRMod.Players
{
    public interface IPlayerSavable
    {
        KeyValuePair<string, object> ToSavableFormat();
    }
}