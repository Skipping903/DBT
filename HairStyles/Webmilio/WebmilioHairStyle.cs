﻿using DBTR.Utilities;

namespace DBTR.HairStyles.Webmilio
{
    public sealed class WebmilioHairStyle : HairStyle
    {
        public WebmilioHairStyle() : base()
        {
        }

        public override bool CanAccess() => SteamHelper.SteamID64 == "76561198046878487";
    }
}