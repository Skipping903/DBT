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
    public abstract class BaseSkillItem : DBTItem
    {
        internal Player player;
        private NPC _npc;

        public bool isFistWeapon;
        public bool canUseHeavyHit;
        public float kiDrain;
        public string weaponType;
        #region Boss bool checks
        public bool eyeDowned;
        public bool beeDowned;
        public bool wallDowned;
        public bool plantDowned;
        public bool dukeDowned;
        public bool moodlordDowned;

        protected BaseSkillItem(string displayName, string tooltip, int width, int height, int rarity) : base(displayName, tooltip, width, height, 0, 0, rarity)
        {
        }

        public override void PostUpdate()
        {
            if (NPC.downedBoss1)
            {
                eyeDowned = true;
            }
            if (NPC.downedQueenBee)
            {
                beeDowned = true;
            }
            if (Main.hardMode)
            {
                wallDowned = true;
            }
            if (NPC.downedPlantBoss)
            {
                plantDowned = true;
            }
            if (NPC.downedFishron)
            {
                dukeDowned = true;
            }
            if (NPC.downedMoonlord)
            {
                moodlordDowned = true;
            }
            if (item.channel)
            {
                item.autoReuse = false;
            }
        }

        #endregion

        public override void SetDefaults()
        {
            item.melee = false;
            item.ranged = false;
            item.magic = false;
            item.thrown = false;
            item.summon = false;
        }

        public override bool CloneNewInstances => true;

        public override int ChoosePrefix(UnifiedRandom rand)
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
        }

        public override bool ReforgePrice(ref int reforgePrice, ref bool canApplyDiscount) => true;

        public override void GetWeaponKnockback(Player player, ref float knockback)
        {
            knockback = knockback + player.GetModPlayer<DBTPlayer>().KiKnockbackAddition;
        }

        public override void GetWeaponDamage(Player player, ref int damage)
        {
            damage = (int)Math.Ceiling(damage * player.GetModPlayer<DBTPlayer>().KiDamageMultiplier);
        }

        public override void GetWeaponCrit(Player player, ref int crit)
        {
            crit = crit + player.GetModPlayer<DBTPlayer>().KiCritAddition;
        }

        public override float UseTimeMultiplier(Player player)
        {
            return player.GetModPlayer<DBTPlayer>().KiSpeedAddition;
        }

        public int RealKiDrain(Player player)
        {
            return (int)(kiDrain * player.GetModPlayer<DBTPlayer>().KiDrainMultiplier);
        }

        public override bool CanUseItem(Player player)
        {
            if (player.GetModPlayer<DBTPlayer>().Ki >= RealKiDrain(player))
            {
                player.GetModPlayer<DBTPlayer>().ModifyKi(-RealKiDrain(player));
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
            TooltipLine indicate = new TooltipLine(mod, "", "");

            string[] text = indicate.text.Split(' ');
            indicate.text = "Consumes " + RealKiDrain(Main.LocalPlayer) + " Ki ";
            indicate.overrideColor = new Color(34, 232, 222);
            tooltips.Add(indicate);

            TooltipLine tt = tooltips.FirstOrDefault(x => x.Name == "Damage" && x.mod == "Terraria");

            if (tt != null)
            {
                string[] splitText = tt.text.Split(' ');
                string damageValue = splitText.First();
                string damageWord = splitText.Last();
                tt.text = damageValue + " ki " + damageWord;
            }

            TooltipLine indicate2 = new TooltipLine(mod, "", "");
            string[] text2 = indicate.text.Split(' ');
            indicate2.text = weaponType + " Technique ";
            indicate2.overrideColor = new Color(232, 202, 34);
            tooltips.Add(indicate2);

            if (item.damage > 0)
                foreach (TooltipLine line in tooltips)
                    if (line.mod == "Terraria" && line.Name == "Tooltip")
                        line.overrideColor = Color.Cyan;
        }
    }
    public abstract class KiPotion : ModItem
    {
        public int kiHeal;
        public int potioncooldown = 3600;
        public bool isKiPotion;
        public override bool CloneNewInstances
        {
            get { return true; }
        }
        public override bool UseItem(Player player)
        {
            player.GetModPlayer<DBTPlayer>().ModifyKi(kiHeal);

            player.AddBuff(mod.BuffType("KiPotionSickness"), potioncooldown);
            CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y, player.width, player.height), new Color(51, 204, 255), kiHeal);

            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.HasBuff(mod.BuffType("KiPotionSickness")))
                return false;
            else
                return true;
        }
    }
}