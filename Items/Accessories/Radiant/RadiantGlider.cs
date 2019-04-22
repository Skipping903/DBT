using DBT.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DBT.Items.Accessories.Radiant
{
    [AutoloadEquip(EquipType.Wings)]
    public sealed class RadiantGlider : DBTAccessory
    {
        public RadiantGlider() : base("Radiant Glider", "Allows flight and slow fall", 36, 40, value: Item.buyPrice(gold: 2, silver: 40), rarity: ItemRarityID.Red)
        {
        }

        public override void SetDefaults()
        {
            base.SetDefaults();

            item.UseSound = SoundID.Item24;
        }

        public override bool WingUpdate(Player player, bool inUse)
        {
            bool baseResult = base.WingUpdate(player, inUse);

            if (inUse)
            {
                if (player.wingFrame == 0 || player.wingFrame >= 3)
                    player.wingFrame = 1;

                player.wingFrameCounter++;

                if (player.wingFrameCounter >= 4)
                {
                    player.wingFrameCounter = 0;
                    player.wingFrame++;
                }
            }
            else
                player.wingFrame = 0;

            return baseResult;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            base.UpdateAccessory(player, hideVisual);

            player.wingsLogic = 1;
            player.wingTimeMax = 160;
            player.flapSound = false;

        }

        public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising, ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
        {
            base.VerticalWingSpeeds(player, ref ascentWhenFalling, ref ascentWhenRising, ref maxCanAscendMultiplier, ref maxAscentMultiplier, ref constantAscend);

            ascentWhenFalling = 0.95f;
            ascentWhenRising = 0.20f;

            maxCanAscendMultiplier = 1;
            maxAscentMultiplier = 3;

            constantAscend = 0.135f;
        }

        public override void HorizontalWingSpeeds(Player player, ref float speed, ref float acceleration)
        {
            base.HorizontalWingSpeeds(player, ref speed, ref acceleration);

            speed = 10f;
            acceleration *= 3f;
        }

        public override void AddRecipes()
        {
            base.AddRecipes();

            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(mod, nameof(RadiantFragment), 14);
            recipe.AddIngredient(ItemID.LunarBar, 10);
            recipe.AddTile(TileID.LunarCraftingStation);
            
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}