namespace DBT.Items.Accessories.Crystallites
{
    public abstract class Crystallite : DBTItem
    {
        protected Crystallite(string displayName, string tooltip, int width, int height, int value, int defense, int rarity) : base(displayName, tooltip, width, height, value, defense, rarity)
        {
        }
    }
}