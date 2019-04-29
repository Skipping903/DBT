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
            tasks.Insert(shiniesIndex + 4, new PassLegacy("Wasteland", WastelandGen));
        }

        private void WastelandGen(GenerationProgress progress)
        {
            progress.Message = "Creating a barren wasteland.";
            progress.Set(0.20f);
            int startPositionX = WorldGen.genRand.Next(Main.spawnTileX + 300, Main.spawnTileX + 1200);
            int startPositionY = (int)WorldGen.worldSurface;
            int size = 0;
            if (Main.maxTilesX == 4200 && Main.maxTilesY == 1200)
            {
                size = 12;
            }
            if (Main.maxTilesX == 6300 && Main.maxTilesY == 1800)
            {
                size = 24;
            }
            if (Main.maxTilesX == 8400 && Main.maxTilesY == 2400)
            {
                size = 36;
            }

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
                        if (Main.tile[x, y].type != TileID.Mud && Main.tile[x, y].type != TileID.SnowBlock && Main.tile[x, y].type != TileID.Sand)
                        {
                            WorldGen.TileRunner(x, y, 300, WorldGen.genRand.Next(100, 200), mod.TileType("CoarseRock"), false, 0f, 0f, true, true);
                            Main.tile[x, y].wall = (ushort)mod.WallType("CoarseRockWall");
                            progress.Set(0.70f);
                        }
                        else
                        {
                            startX++;
                        }
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

