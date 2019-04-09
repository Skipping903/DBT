using DBT.Commons;

namespace DBT.Traits
{
    public abstract class Trait : IHasUnlocalizedName
    {
        protected Trait(string unlocalizedName)
        {
            UnlocalizedName = unlocalizedName;
        }

        public string UnlocalizedName { get; }
    }
}
