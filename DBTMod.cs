using System.Collections.Generic;
using System.IO;
using DBTMod.Network;
using DBTMod.UserInterfaces.CharacterMenus;
using DBTMod.UserInterfaces.KiBar;
using DBTMod.Utilities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace DBTMod
{
	public sealed class DBTMod : Mod
	{
	    internal ModHotKey characterMenuKey, energyChargeKey, transformDownKey, speedToggleKey, transformUpKey;

	    internal KiBar kiBar;
	    internal UserInterface kiBarInterface;

	    internal CharacterMenu characterMenu;
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

	            kiBar = new KiBar();
	            kiBar.Activate();

	            kiBarInterface = new UserInterface();
	            kiBarInterface.SetState(kiBar);

	            kiBar.Visible = true;

                #endregion

                characterMenu = new CharacterMenu(this);
                characterMenu.Activate();
                characterMenuInterface = new UserInterface();
                characterMenuInterface.SetState(characterMenu);
	        }
	    }

	    public override void Unload()
	    {
	        if (!Main.dedServ)
	        {
	            kiBar.Visible = false;

	            characterMenu.Visible = false;
	        }

	        Instance = null;
	    }

        #endregion

	    public override void UpdateUI(GameTime gameTime)
	    {
	        if (characterMenuInterface != null && characterMenu.Visible)
                characterMenu.Update(gameTime);
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

            //if (resourcesLayerIndex != -1)
            //    layers.Insert(resourcesLayerIndex, new KiBarLayer());

            if (characterMenuIndex != -1)
                layers.Insert(characterMenuIndex, new CharacterMenuLayer(characterMenu, characterMenuInterface));
        }


        internal static DBTMod Instance { get; private set; }
    }
}
