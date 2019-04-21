﻿using Terraria.ID;

namespace DBT.Items.Accessories
{
    public abstract class DBTAccessory : DBTItem
    {
        protected DBTAccessory(string displayName, string tooltip, int width, int height, int value = 0, int defense = 0, int rarity = ItemRarityID.White): base(displayName, tooltip, width, height, value, defense, rarity)
        {   
        }

        public override void SetDefaults()
        {
            base.SetDefaults();

            item.accessory = true;
        }
    }
}