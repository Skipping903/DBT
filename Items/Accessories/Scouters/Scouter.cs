﻿using System;
using DBT.Commons.Items;
using DBT.Items.Materials.Metals;
using DBT.Players;
using DBT.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DBT.Items.Accessories.Scouters
{
    [AutoloadEquip(EquipType.Head, EquipType.Face)]
    public abstract class Scouter : DBTItem, IHasValue, IHasRarity
    {
        protected Scouter(string displayName, string tooltip, int value, int rarity, float kiDamageMultiplier, int width = 24, int height = 28) : base(displayName, tooltip, width, height, value, 0, rarity)
        {
            Value = value;
            Rarity = rarity;
            KiDamageMultiplier = kiDamageMultiplier;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();

            item.accessory = true;
        }

        public override void UpdateEquip(Player player)
        {
            base.UpdateEquip(player);

            GivePlayerBonuses(player);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            base.UpdateAccessory(player, hideVisual);

            GivePlayerBonuses(player);
        }

        public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
        {
            drawHair = true;
            drawAltHair = true;
        }

        private void GivePlayerBonuses(Player player)
        {
            DBTPlayer dbtPlayer = player.GetModPlayer<DBTPlayer>();

            dbtPlayer.KiDamageMultiplier += KiDamageMultiplier;
            player.detectCreature = true;
        }

        public int Value { get; }

        public int Rarity { get; }

        public float KiDamageMultiplier { get; }
    }
}