using System;
using System.Collections.Generic;
using System.Reflection;
using DBT.Commons;
using Terraria.ModLoader;

namespace DBT.Helpers
{
    public static class SteamHelper
    {
        private static bool _initialized = false;
        private static bool? _donator = null;

        private static readonly List<long> _activeDeveloperSteamIds = new List<long>()
        {
            FullNovaAlchemist, NuovaPrime, Webmilio
        };

        private static readonly List<Donator> _activeDonatorSteamIds = new List<Donator>()
        {
            BrushBoy,
            CanadianMRE,
            FreeRaisinBread,
            Hamster, HelloMyFriend,
            Luna,
            Mak0, Megawarrior101,
            NyoonBooetteEnthusiast,
            PapaTingle, Pheonix,
            Skipping, SnorLaxatives, SoulCarnagee,
            Vector,
            William
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

        public static bool CanUserAccess(long steamId64, bool includeDevs = false)
        {
            if (steamId64 == SteamId64) return true;

            if (includeDevs)
                for (int i = 0; i < _activeDeveloperSteamIds.Count; i++)
                    if (_activeDeveloperSteamIds[i] == SteamId64)
                        return true;

            for (int i = 0; i < _activeDonatorSteamIds.Count; i++)
                if (_activeDonatorSteamIds[i].steamId64 == steamId64)
                    return true;

            return false;
        }

        public static bool CanUserAccess(Donator donator, bool includeDevs = false) => CanUserAccess(donator.steamId64, includeDevs);

        public static bool CanUserAccess(bool includeDevs, params long[] steamId64s)
        {
            for (int i = 0; i < steamId64s.Length; i++)
                if (CanUserAccess(steamId64s[i], includeDevs))
                    return true;

            return false;
        }

        public static bool CanUserAccess(bool includeDevs, params Donator[] donators)
        {
            for (int i = 0; i < donators.Length; i++)
                if (CanUserAccess(donators[i].steamId64, includeDevs))
                    return true;

            return false;
        }

        public static bool HasSteamId64 { get; private set; }
        public static long SteamId64 { get; private set; }

        public static bool IsDonator
        {
            get
            {
                if (_donator == null)
                {
                    if (!HasSteamId64)
                    {
                        _donator = false;
                        return _donator.Value;
                    }

                    _donator = false;

                    for (int i = 0; i < _activeDonatorSteamIds.Count; i++)
                        if (_activeDonatorSteamIds[i].steamId64 == SteamId64)
                        {
                            _donator = true;
                            return _donator.Value;
                        }
                }

                return _donator.Value;
            }
        }

        #region Developers

        public static long Webmilio => 76561198046878487;
        public static long NuovaPrime => 76561198323188001;
        public static long FullNovaAlchemist => 76561198128593049;

        #endregion

        #region Donators

        public static Donator BrushBoy => new Donator(76561197960287930, "Brush Boy", 200413799746895873); // Brush Boy#9064
        public static Donator CanadianMRE => new Donator(76561198147284656, "CanadianMRE", 219196190271471616); // CanadianMRE#6288
        public static Donator FreeRaisinBread => new Donator(368799789942308874, "FreeRasinBread", 368799789942308874); // Freerasinbread#3516
        public static Donator Hamster => new Donator(76561198055274667, "Hamster", 224323430651133952); // Hamster#2477
        public static Donator HelloMyFriend => new Donator(76561198062878746, "Hello My Friend", 251408085313126401); // hello my friend#2456
        public static Donator Luna => new Donator(76561198085728391, "Luna", 216853147837005824);
        public static Donator Mak0 => new Donator(76561198161525177, "Michael", 307618173073489920); // Mak0-Z#6790
        public static Donator Megawarrior101 => new Donator(76561193783431419, "Megawarrior_101", 405844470584836117); // Megawarrior_101#0616
        public static Donator NyoonBooetteEnthusiast => new Donator(76561198301260251, "Nyoon Booette Enthusiast", 228294779815985152); // Nyoon Booette enthusiast#4130;
        public static Donator PapaTingle => new Donator(76561198236967076, "Papa Tingle", 159089770763517953); // PapaTingle#5281
        public static Donator Pheonix => new Donator(76561198059019480, "Pheonix", 175445960946745344); // pheonix#7035
        public static Donator Skipping => new Donator(76561193979609866, "Luke", 450018452103757835); // Skipping#7613
        public static Donator SnorLaxatives => new Donator(76561198108193073, "SnorLaxatives", 189983407168684042);
        public static Donator SoulCarnagee => new Donator(76561198059100989, "SoulCarnagee", 313358705854775296); // SoulCarnagee#6993
        public static Donator Vector => new Donator(76561198248632856, "Vector", 300040297256058881); // Mr.Bombastic#1706
        public static Donator William => new Donator(76561198090096918, "William", 141746262490873857); // william#0117

        #endregion
    }
}
