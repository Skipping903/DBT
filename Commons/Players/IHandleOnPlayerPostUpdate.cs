using DBT.Players;

namespace DBT.Commons.Players
{
    public interface IHandleOnPlayerPostUpdate
    {
        void OnPlayerPostUpdate(DBTPlayer dbtPlayer);
    }
}