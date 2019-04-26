using Terraria;
using Terraria.ModLoader;
using DBT.Players;

namespace DBT.Wasteland.Backgrounds
{
	public class WastelandBG : ModSurfaceBgStyle
	{
		public override bool ChooseBgStyle()
		{
			return !Main.gameMenu && Main.LocalPlayer.GetModPlayer<DBTPlayer>(mod).zoneWasteland;
		}

		public override void ModifyFarFades(float[] fades, float transitionSpeed)
		{
			for (int i = 0; i < fades.Length; i++)
			{
				if (i == Slot)
				{
					fades[i] += transitionSpeed;
					if (fades[i] > 1f)
					{
						fades[i] = 1f;
					}
				}
				else
				{
					fades[i] -= transitionSpeed;
					if (fades[i] < 0f)
					{
						fades[i] = 0f;
					}
				}
			}
		}

		public override int ChooseFarTexture()
		{
			return mod.GetBackgroundSlot("Wasteland/Backgrounds/WastelandSurfaceFar");
		}

		public override int ChooseMiddleTexture()
		{
            return mod.GetBackgroundSlot("Wasteland/Backgrounds/WastelandSurfaceMid");
        }

		public override int ChooseCloseTexture(ref float scale, ref double parallax, ref float a, ref float b)
		{
            return mod.GetBackgroundSlot("Wasteland/Backgrounds/WastelandSurfaceClose");
        }
	}
}