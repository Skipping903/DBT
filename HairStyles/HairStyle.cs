using System;
using System.Collections.Generic;
using DBT.Commons;
using DBT.Transformations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;

namespace DBT.HairStyles
{
    public class HairStyle : IHasUnlocalizedName
    {
        public const string HAIR_STYLE_SUFFIX = "HairStyle";

        protected readonly Dictionary<string, Texture2D> texturesPerTransformationUnlocalized = new Dictionary<string, Texture2D>();

        public HairStyle(int xOffset = 0, int yOffset = 0, bool autoBobbing = true, string hairStyleSuffix = HAIR_STYLE_SUFFIX)
        {
            Offset = new Vector2(xOffset, -yOffset);
            AutoBobbing = autoBobbing;

            Initialize(this.GetType(), hairStyleSuffix);
        }

        public HairStyle(Type type, int xOffset = 0, int yOffset = 0, bool autoBobbing = false, string hairStyleSuffix = HAIR_STYLE_SUFFIX)
        {
            Offset = new Vector2(xOffset, -yOffset);
            AutoBobbing = autoBobbing;

            Initialize(type, hairStyleSuffix);
        }


        private void Initialize(Type type, string hairStyleSuffix = HAIR_STYLE_SUFFIX)
        {
            string[] segments = type.Namespace.Split('.');
            string root = string.Join("/", segments, 1, segments.Length - 1) + '/';

            UnlocalizedName = type.Name.Replace(hairStyleSuffix, "");
            RootPath = root;

            Mod mod = ModLoader.GetMod(segments[0]);

            string basePath = RootPath + "Base";

            if (mod.TextureExists(basePath))
                Base = mod.GetTexture(basePath);

            foreach (string key in TransformationDefinitionManager.Instance.Keys)
                if (mod.TextureExists(RootPath + key))
                    texturesPerTransformationUnlocalized.Add(key, mod.GetTexture(RootPath + key));
        }


        public virtual bool CanAccess() => true;


        public Texture2D this[TransformationDefinition transformation] => this[transformation.UnlocalizedName];

        public Texture2D this[string key]
        {
            get
            {
                if (!texturesPerTransformationUnlocalized.ContainsKey(key))
                    return Base;

                return texturesPerTransformationUnlocalized[key];
            }
        }


        public string UnlocalizedName { get; private set; }

        public string RootPath { get; protected set; }


        public Vector2 Offset { get; }

        public bool AutoBobbing { get; }


        public Texture2D Base { get; private set; }
    }
}
