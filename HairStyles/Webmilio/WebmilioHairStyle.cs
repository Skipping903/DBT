﻿using DBT.Helpers;

namespace DBT.HairStyles.Webmilio
{
    public sealed class WebmilioHairStyle : HairStyle
    {
        public WebmilioHairStyle() : base()
        {
        }

        public override bool CanAccess() => SteamHelper.Webmilio;
    }
}
