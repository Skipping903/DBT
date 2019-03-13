using System.Collections.Generic;

namespace DBTR.Players
{
    public interface IPlayerSavable
    {
        KeyValuePair<string, object> ToSavableFormat();
    }
}