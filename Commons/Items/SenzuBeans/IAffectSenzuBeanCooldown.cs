using DBT.Items;
using DBT.Players;

namespace DBT.Commons.Items.SenzuBeans
{
    public interface IAffectSenzuBeanCooldown
    {
        void AffectSenzuBeanCooldown(DBTPlayer dbtPlayer, DBTItem dbtItem, ref float cooldown);
    }
}