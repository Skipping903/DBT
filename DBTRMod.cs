using Terraria;
using Terraria.ModLoader;

namespace DBTRMod
{
	public sealed class DBTRMod : Mod
	{
	    internal ModHotKey characterMenuKey, energyChargeKey, transformDownKey, speedToggleKey, transformUpKey;

        public DBTRMod()
		{
            Properties = new ModProperties()
            {
                Autoload = true,
                AutoloadGores = true,
                AutoloadSounds = true
            };

		    Instance = this;
		}

	    public override void Load()
	    {
	        if (!Main.dedServ)
	        {
	            #region HotKeys

	            characterMenuKey = RegisterHotKey("Character Menu", "K");
	            energyChargeKey = RegisterHotKey("Energy Charge", "C");
                speedToggleKey = RegisterHotKey("Speed Toggle", "Z");
	            transformDownKey = RegisterHotKey("Transform Down", "V");
	            transformUpKey = RegisterHotKey("Transform Up", "X");

                #endregion
            }
	    }

	    public override void Unload()
	    {
	        if (!Main.dedServ)
	        {

	        }

	        Instance = null;
	    }

	    internal static DBTRMod Instance { get; private set; }
    }
}
