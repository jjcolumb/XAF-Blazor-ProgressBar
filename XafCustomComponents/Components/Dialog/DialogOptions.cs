// Copyright (c) 2020 Jonny Larsson
// License: MIT
// See https://github.com/MudBlazor/MudBlazor
// Modified version of Blazored Modal
// Copyright (c) 2019 Blazored
// License: MIT
// See https://github.com/Blazored

// Copyright (c) 2022 Adapted by Lemsoy Valiente


using System.ComponentModel;

namespace XafCustomComponents
{
    public class DialogOptions
    {
        public DialogPosition? Position { get; set; }

        public string? Width { get; set; }
        public string? MinWidth { get; set; }
        public string? MaxWidth { get; set; }
        public string? Height { get; set; }
        public string? MinHeight { get; set; }
        public string? MaxHeight { get; set; }
        public bool? CloseOnOutsideClick { get; set; }
        public bool? CloseOnEscapeKey { get; set; }
        public bool? ShowHeader { get; set; }
        public bool? ShowFooter { get; set; }
        public bool? CloseButton { get; set; }
        public bool? Backdrop { get; set; }
    }

    public enum DialogPosition
    {
        [Description("center")]
        Center,
        [Description("centerleft")]
        CenterLeft,
        [Description("centerright")]
        CenterRight,
        [Description("topcenter")]
        TopCenter,
        [Description("topleft")]
        TopLeft,
        [Description("topright")]
        TopRight,
        [Description("bottomcenter")]
        BottomCenter,
        [Description("bottomleft")]
        BottomLeft,
        [Description("bottomright")]
        BottomRight
    }
}
