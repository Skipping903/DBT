﻿using System;
using DBTR.Auras;
using DBTR.HairStyles;
using DBTR.Players;
using Microsoft.Xna.Framework;

namespace DBTR.Transformations
{
    public abstract class TransformationAppearance
    {
        protected TransformationAppearance(AuraAppearance aura, HairAppearance hair)
        {
            Aura = aura;
            Hair = hair;
        }

        public AuraAppearance Aura { get; }

        public HairAppearance Hair { get; }
    }

    public class HairAppearance
    {
        public HairAppearance(HairStyle style)
        {
            Style = style;
        }

        public HairStyle Style { get; }
    }
}
