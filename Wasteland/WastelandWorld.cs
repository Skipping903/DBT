using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent.Generation;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.World.Generation;

namespace DBT.Wasteland
{
    public class WastelandWorld : ModWorld
    {
        public static int wastelandTiles = 0;

        public override void TileCountsAvailable(int[] tileCounts)
        {
            wastelandTiles = tileCounts[mod.TileType("CoarseRock")];
        }
        public override void ModifyWorldGenTasks(List<GenPass> tasks, ref float totalWeight)
        {
            int shiniesIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Shinies"));
            if (shiniesIndex == -1)
            {
                return;
            }
            tasks.Insert(shiniesIndex + 4, new PassLegacy("Mod Biomes", WastelandGen));
        }

        private void WastelandGen(GenerationProgress progress)
        {
            progress.Message = "Creating a barren wasteland.";
            progress.Set(0.20f);
            int startPositionX = WorldGen.genRand.Next(Main.maxTilesX / 2 - 260, Main.spawnTileX);
            int startPositionY = (int)Main.worldSurface - Main.rand.Next(20, 60);
            int size = 0;
            if (Main.maxTilesX == 4200 && Main.maxTilesY == 1200)
            {
                size = 40;
            }
            if (Main.maxTilesX == 6300 && Main.maxTilesY == 1800)
            {
                size = 62;
            }
            if (Main.maxTilesX == 8400 && Main.maxTilesY == 2400)
            {
                size = 84;
            }
            startPositionX--;
            if (Main.tile[startPositionX, startPositionY].type == TileID.Sand)
            {
                var startX = startPositionX;
                var startY = startPositionY;
                startY++;
                progress.Set(0.50f);

                for (int x = startX - size; x <= startX + size; x++)
                {
                    for (int y = startY - size; y <= startY + size; y++)
                    {
                        if (Vector2.Distance(new Vector2(startX, startY), new Vector2(x, y)) <= size)
                        {
                            WorldGen.TileRunner(x, y, 300, WorldGen.genRand.Next(100, 200), mod.TileType("CoarseRock"), false, 0f, 0f, true, true);
                            WorldGen.PlaceWall(x, y, mod.WallType("CoarseRockWall"));
                            progress.Set(0.70f);
                        }
                    }
                }
           
                while (!Main.tile[startX, startY].active() && startY < Main.worldSurface)
                {
                    startY++;
                }
            }
        }
    }
}
