//Copyright (c) 2021 MudBlazor

// Copyright (c) 2022 Adapted by Lemsoy Valiente

using Microsoft.AspNetCore.Components;
using System.ComponentModel;

namespace XafCustomComponents
{
    public class CustomComponentBase : ComponentBase
    {
        [Parameter]
        public string Class { get; set; }

        [Parameter]
        public string Style { get; set; }

        [Parameter(CaptureUnmatchedValues = true)]
        public Dictionary<string, object> UserAttributes { get; set; } = new Dictionary<string, object>();
    }
}
