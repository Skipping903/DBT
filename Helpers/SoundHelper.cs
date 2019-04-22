using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using ReLogic.Utilities;
using Terraria;
using Terraria.Audio;
using SoundType = Terraria.ModLoader.SoundType;

namespace DBT.Helpers
{
    // Credit to MercuriusXeno for the SoundHelper.
    public class SoundHelper
    {
        public static void PlayVanillaSound(int soundId, Player player, float volume = 1f, float pitchVariance = 0f) => 
            PlayVanillaSound(soundId, player?.Center ?? Vector2.Zero, volume, pitchVariance);

        public static void PlayVanillaSound(int soundId, Vector2 position, float volume = 1f, float pitchVariance = 0f)
        {
            if (Main.dedServ) return;

            if (position == Vector2.Zero)
                Main.PlaySound(soundId, (int) position.X, (int) position.Y, 1, volume, pitchVariance);
        }

        public static SoundEffectInstance PlayVanillaSound(int soundId, Vector2 position, int style)
        {
            if (Main.dedServ) return null;

            return Main.PlaySound(soundId, (int) position.X, (int) position.Y, style);
        }

        public static SlotId PlayVanillaSound(LegacySoundStyle soundId, Player player = null, float volume = 1f, float pitchVariance = 0f) =>
            PlayVanillaSound(soundId, player?.Center ?? Vector2.Zero, volume, pitchVariance);

        public static SlotId PlayVanillaSound(LegacySoundStyle soundId, Vector2 position, float volume = 1f, float pitchVariance = 0f)
        {
            if (Main.dedServ) return SlotId.Invalid;

            if (position == Vector2.Zero)
                return Main.PlayTrackedSound(soundId.WithVolume(volume).WithPitchVariance(pitchVariance), position);

            return Main.PlayTrackedSound(soundId.WithVolume(volume).WithPitchVariance(pitchVariance), position);
        }

        public static KeyValuePair<uint, SoundEffectInstance> PlayCustomSound(string soundId, Player player = null, float volume = 1f, float pitchVariance = 0f) =>
            PlayCustomSound(soundId, player?.Center ?? Vector2.Zero, volume, pitchVariance);

        public static KeyValuePair<uint, SoundEffectInstance> PlayCustomSound(string soundId, Vector2 position, float volume = 1f, float pitchVariance = 0f)
        {
            if (Main.dedServ) return new KeyValuePair<uint, SoundEffectInstance>(InvalidSlot, null);

            uint slotId = InvalidSlot;
            LegacySoundStyle style = GetCustomStyle(soundId, volume, pitchVariance);

            SoundEffectInstance sound = null;

            if (position == Vector2.Zero)
                sound = Main.PlaySound(style);
            else
                sound = Main.PlaySound(style, position);

            slotId = (uint) DBTMod.Instance.GetSoundSlot(SoundType.Custom, soundId);
            return new KeyValuePair<uint, SoundEffectInstance>(slotId, sound);
        }

        public static LegacySoundStyle GetCustomStyle(string soundId, float volume = 1f, float pitchVariance = 0f) =>
            DBTMod.Instance.GetLegacySoundSlot(SoundType.Custom, soundId).WithVolume(volume).WithPitchVariance(pitchVariance);

        public static KeyValuePair<uint, SoundEffectInstance> KillTrackedSound(KeyValuePair<uint, SoundEffectInstance> soundInfo)
        {
            ActiveSound sound = Main.GetActiveSound(new SlotId(soundInfo.Key));

            if (sound != null)
                sound.Stop();

            if (soundInfo.Value != null)
                soundInfo.Value.Stop();

            return new KeyValuePair<uint, SoundEffectInstance>(InvalidSlot, null);
        }

        public static void UpdateTrackedSound(KeyValuePair<uint, SoundEffectInstance> soundInfo, Vector2 position)
        {
            ActiveSound sound = Main.GetActiveSound(new SlotId(soundInfo.Key));

            if (sound != null)
            {
                sound.Position = position;
                sound.Update();
            }
        }

        // TODO Add remaining methods.
        //public static bool CanPlayOtherPlayerAudio(DBTPlayer dbtPlayer, Player otherPlayer) => dbtPlayer.player

        public static uint InvalidSlot { get; } = (uint) SlotId.Invalid.ToFloat();
    }
}