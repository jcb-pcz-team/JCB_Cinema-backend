﻿using System.ComponentModel;

namespace JCB_Cinema.Domain.ValueObjects
{
    public enum ScreenType
    {
        [Description("2D")]
        TwoD,   // 2D
        [Description("3D")]
        ThreeD, // 3D
        [Description("IMAX")]
        IMAX    // IMAX
    }
}