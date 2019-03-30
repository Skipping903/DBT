using System.Collections.Generic;

namespace DBTMod.Players
{
    public interface IPlayerSavable
    {
        KeyValuePair<string, object> ToSavableFormat();
    }
}