using Terraria.ModLoader;

namespace DBTMod
{
	public sealed class DBTMod : Mod
	{
		public DBTMod()
		{
            Properties = new ModProperties()
            {
                Autoload = true,
                AutoloadGores = true,
                AutoloadSounds = true
            };

		    Instance = this;
		}

	    internal static DBTMod Instance { get; private set; }
    }
}
