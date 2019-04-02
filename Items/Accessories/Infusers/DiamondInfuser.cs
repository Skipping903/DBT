﻿using Terraria.ID;

namespace DBT.Items.Accessories.Infusers
{
    public sealed class DiamondInfuser : Infuser
    {
        public DiamondInfuser() : base("Diamond Ki Infuser", "Hitting enemies with ki attacks inflicts confusion.", 260 * Constants.SILVER_VALUE_MULTIPLIER, ItemID.Diamond)
        {
        }
    }
}