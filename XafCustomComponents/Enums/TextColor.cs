//Copyright (c) 2021 MudBlazor

// Copyright (c) 2022 Adapted by Lemsoy Valiente

using System.ComponentModel;

namespace XafCustomComponents
{
    public enum TextColor
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
        [Description("muted")]
        Muted,
        [Description("body")]
        Body,
        [Description("white")]
        White,
        [Description("black-50")]
        Black50,
        [Description("white-50")]
        White50,
    }
}
