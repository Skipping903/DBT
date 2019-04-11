using System.Collections.Generic;
using System.IO;
using DBT.Network;
using DBT.UserInterfaces.CharacterMenus;
using DBT.UserInterfaces.KiBar;
using DBT.Utilities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace DBT
{
	public sealed class DBTMod : Mod
	{
	    internal ModHotKey characterMenuKey, energyChargeKey, transformDownKey, speedToggleKey, transformUpKey;

	    internal KiBar KiBar;
	    internal UserInterface KiBarInterface;

	    internal DBTMenu dbtMenu;
	    internal UserInterface characterMenuInterface;

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

        #region Load/Unloading

	    public override void Load()
	    {
	        if (!Main.dedServ)
	        {
	            SteamHelper.Initialize();

                #region HotKeys

                characterMenuKey = RegisterHotKey("Character Menu", "K");
	            energyChargeKey = RegisterHotKey("Energy Charge", "C");
	            speedToggleKey = RegisterHotKey("Speed Toggle", "Z");
	            transformDownKey = RegisterHotKey("Transform Down", "V");
	            transformUpKey = RegisterHotKey("Transform Up", "X");

	            #endregion

	            #region Ki Bar

	            KiBar = new KiBar();
	            KiBar.Activate();

	            KiBarInterface = new UserInterface();
	            KiBarInterface.SetState(KiBar);

	            KiBar.Visible = true;

                #endregion

                dbtMenu = new DBTMenu(this);
                dbtMenu.Activate();
                characterMenuInterface = new UserInterface();
                characterMenuInterface.SetState(dbtMenu);
	        }
	    }

	    public override void Unload()
	    {
	        if (!Main.dedServ)
	        {
	            KiBar.Visible = false;

	            dbtMenu.Visible = false;
	        }

	        Instance = null;
	    }

        #endregion

	    public override void UpdateUI(GameTime gameTime)
	    {
	        if (characterMenuInterface != null && dbtMenu.Visible)
                characterMenuInterface.Update(gameTime);
	    }


	    public override void HandlePacket(BinaryReader reader, int whoAmI)
	    {
	        NetworkPacketManager.Instance.HandlePacket(reader, whoAmI);
	    }

	    public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
	    {
	        int
	            resourcesLayerIndex = layers.FindIndex(l => l.Name.Contains("Resource Bars")),
	            characterMenuIndex = layers.FindIndex(l => l.Name.Contains("Hotbar"));

            if (resourcesLayerIndex != -1)
                layers.Insert(resourcesLayerIndex, new KiBarLayer());

            if (characterMenuIndex != -1)
                layers.Insert(characterMenuIndex, new DBTMenuLayer(dbtMenu, characterMenuInterface));
        }


        internal static DBTMod Instance { get; private set; }
    }
}
