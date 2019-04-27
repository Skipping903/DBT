using System;
using System.Collections.Generic;
using System.Reflection;
using Terraria.ModLoader;

namespace DBT.Utilities
{
    public static class SteamHelper
    {
        private static bool _initialized = false;
        private readonly static List<long> developerSteamIds = new List<long>()
        {
            76561198046878487, // webmilio
            76561198205177236, // Cat
            76561198323188001, // NuovaPrime
            76561198128593049 // FullNovaAlchemist
        };

        public static void Initialize()
        {
            if (_initialized) return;

            try
            {
                string unparsedSteamID64 = typeof(ModLoader).GetProperty("SteamID64", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null).ToString();

                if (!string.IsNullOrWhiteSpace(unparsedSteamID64))
                {
                    HasSteamId64 = true;
                    SteamId64 = long.Parse(unparsedSteamID64);
                }
            }
            catch (Exception)
            {
                //Console.WriteLine("Unable to fetch SteamID, assuming no steam is present.");
            }
        }

        public static bool CanUserAccess(long steamId, bool includeDevs = false)
        {
            if (steamId == SteamId64) return true;

            if (includeDevs)
                for (int i = 0; i < developerSteamIds.Count; i++)
                    if (developerSteamIds[i] == SteamId64) return true;

            return false;
        }

        public static bool CanUserAccess(bool includeDevs, params long[] steamIds)
        {
            for (int i = 0; i < steamIds.Length; i++)
                if (CanUserAccess(steamIds[i], includeDevs))
                    return true;

            return false;
        }

        public static bool HasSteamId64 { get; private set; }
        public static long SteamId64 { get; private set; }
    }
}
