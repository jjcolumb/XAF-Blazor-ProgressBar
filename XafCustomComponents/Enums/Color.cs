//Copyright(c) 2021 MudBlazor

// Copyright (c) 2022 Adapted by Lemsoy Valiente

using System.ComponentModel;

namespace XafCustomComponents
{
    public enum Color
    {
        [Description("primary")]
        Primary,
        [Description("secondary")]
        Secondary,
        [Description("info")]
        Info,
        [Description("success")]
        Success,
        [Description("warning")]
        Warning,
        [Description("danger")]
        Danger,
        [Description("dark")]
        Dark,
        [Description("light")]
        Light,
        [Description("white")]
        White,
        [Description("body")]
        Body,
        [Description("transparent")]
        Transparent,
    }
}
