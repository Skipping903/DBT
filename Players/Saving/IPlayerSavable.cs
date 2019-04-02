using System.Collections.Generic;

namespace DBT.Players
{
    public interface IPlayerSavable
    {
        KeyValuePair<string, object> ToSavableFormat();
    }
}