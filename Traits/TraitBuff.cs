using DBT.Buffs;

namespace DBT.Traits
{
    public abstract class TraitBuff : DBTBuff
    {
        protected TraitBuff(string displayName, string tooltip) : base(displayName, tooltip)
        {
        }
    }
}