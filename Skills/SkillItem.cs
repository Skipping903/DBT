using System;
using System.Collections.Generic;
using System.Linq;
using DBT.Items;
using DBT.Players;
using DBT.Skills.Beams;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace DBT.Skills
{
    public abstract class SkillItem : DBTItem
    {
        protected SkillItem(string displayName, string tooltip, int width, int height, int rarity, string displayWeaponType, float kiDrain) : base(displayName, tooltip, width, height, 0, 0, rarity)
        {
            DisplayWeaponType = displayWeaponType;

            KiDrain = kiDrain;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();

            item.melee = false;
            item.ranged = false;
            item.magic = false;
            item.thrown = false;
            item.summon = false;
        }

        public override bool CloneNewInstances => true;

        /*public override int ChoosePrefix(UnifiedRandom rand)
        {
            WeightedRandom<int> prefixChooser = new WeightedRandom<int>();

            prefixChooser.Add(mod.PrefixType("BalancedPrefix"), 3); // 3 times as likely
            prefixChooser.Add(mod.PrefixType("CondensedPrefix"), 3);
            prefixChooser.Add(mod.PrefixType("MystifyingPrefix"), 3);
            prefixChooser.Add(mod.PrefixType("UnstablePrefix"), 3);
            prefixChooser.Add(mod.PrefixType("FlawedPrefix"), 3);
            prefixChooser.Add(mod.PrefixType("BoostedPrefix"), 3);
            prefixChooser.Add(mod.PrefixType("NegatedPrefix"), 3);
            prefixChooser.Add(mod.PrefixType("OutrageousPrefix"), 3);
            prefixChooser.Add(mod.PrefixType("PoweredPrefix"), 2);
            prefixChooser.Add(mod.PrefixType("FlashyPrefix"), 2);
            prefixChooser.Add(mod.PrefixType("InfusedPrefix"), 2);
            prefixChooser.Add(mod.PrefixType("DistractingPrefix"), 2);
            prefixChooser.Add(mod.PrefixType("DestructivePrefix"), 2);
            prefixChooser.Add(mod.PrefixType("MasteredPrefix"), 1);
            prefixChooser.Add(mod.PrefixType("TranscendedPrefix"), 1);

            int choice = prefixChooser;

            if ((item.damage > 0) && item.maxStack == 1)
                return choice;

            return -1;
        }*/

        public override bool ReforgePrice(ref int reforgePrice, ref bool canApplyDiscount) => true;

        public override void GetWeaponKnockback(Player player, ref float knockback) => knockback += player.GetModPlayer<DBTPlayer>().KiKnockbackAddition;
        public override void GetWeaponDamage(Player player, ref int damage) => damage = (int)Math.Ceiling(damage * player.GetModPlayer<DBTPlayer>().KiDamageMultiplier);
        public override void GetWeaponCrit(Player player, ref int crit) => crit += crit + player.GetModPlayer<DBTPlayer>().KiCritAddition;

        public override float UseTimeMultiplier(Player player) => player.GetModPlayer<DBTPlayer>().KiSpeedAddition;

        public override bool CanUseItem(Player player)
        {
            float realKiDrain = player.GetModPlayer<DBTPlayer>().GetKiDrain(KiDrain);

            if (player.GetModPlayer<DBTPlayer>().Ki >= realKiDrain)
            {
                player.GetModPlayer<DBTPlayer>().ModifyKi(-realKiDrain);
                return true;
            }

            return false;
        }

        public override bool UseItem(Player player)
        {
            // TODO Replace this with some kind of interface inside of any transformations.
            /*if (TransformationHelper.IsLSSJ(player) && !TransformationHelper.IsSSJ1(player))
            {
                int i = Main.rand.Next(1, 4);
                player.GetModPlayer<DBTPlayer>().overloadCurrent += i;
            }*/

            return true;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            TooltipLine indicate = new TooltipLine(mod, "", "")
            {
                text = "Consumes " + Main.LocalPlayer.GetModPlayer<DBTPlayer>().GetKiDrain(KiDrain) + " Ki",
                overrideColor = new Color(34, 232, 222)
            };

            tooltips.Add(indicate);

            TooltipLine tt = tooltips.FirstOrDefault(x => x.Name == "Damage" && x.mod == "Terraria");

            if (tt != null)
            {
                string[] splitText = tt.text.Split(' ');
                string damageValue = splitText.First();
                string damageWord = splitText.Last();
                tt.text = damageValue + " ki " + damageWord;
            }

            indicate = new TooltipLine(mod, "", "")
            {
                text = DisplayWeaponType + " Technique ",
                overrideColor = new Color(232, 202, 34)
            };

            tooltips.Add(indicate);

            foreach (TooltipLine line in tooltips)
                if (line.mod == nameof(Terraria) && line.Name == nameof(Tooltip))
                    line.overrideColor = Color.Cyan;
        }

        public string DisplayWeaponType { get; }

        public float KiDrain { get; }
    }
}