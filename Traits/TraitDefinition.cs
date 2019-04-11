using DBT.Commons;

namespace DBT.Traits
{
    public abstract class TraitDefinition : IHasUnlocalizedName
    {
        protected TraitDefinition(string unlocalizedName)
        {
            UnlocalizedName = unlocalizedName;
        }

        public string UnlocalizedName { get; }
    }
}
