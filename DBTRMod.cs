using System.Collections.Generic;
using System.IO;
using DBTR.Network;
using DBTR.UserInterfaces.KiBar;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace DBTR
{
	public sealed class DBTRMod : Mod
	{
	    internal ModHotKey characterMenuKey, energyChargeKey, transformDownKey, speedToggleKey, transformUpKey;

	    internal static KiBar kiBar;
	    internal static UserInterface kiBarInterface;

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

        #region Load/Unloading

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

	            #region Ki Bar

	            kiBar = new KiBar();
	            kiBar.Activate();

	            kiBarInterface = new UserInterface();
	            kiBarInterface.SetState(kiBar);

	            kiBar.Visible = true;

	            #endregion
	        }
	    }

	    public override void Unload()
	    {
	        if (!Main.dedServ)
	        {
	            #region Ki Bar

	            kiBar.Visible = false;

	            #endregion
	        }

	        Instance = null;
	    }

        #endregion

	    public override void HandlePacket(BinaryReader reader, int whoAmI)
	    {
	        NetworkPacketManager.Instance.HandlePacket(reader, whoAmI);
	    }

	    public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int resourcesLayerIndex = layers.FindIndex(l => l.Name.Contains("Resource Bars"));

            if (resourcesLayerIndex != -1)
                layers.Insert(resourcesLayerIndex, new KiBarLayer());
        }


        internal static DBTRMod Instance { get; private set; }
    }
}
