using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XafCustomComponents.Extensions;
using XafCustomComponents.Utilities;

namespace XafCustomComponents
{
    public partial class CustomText : CustomComponentBase
    {
        [Parameter]
        public TextType TextType { get; set; } = TextType.paragraph;

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public TextColor Color { get; set; } = TextColor.Body;

        private string ClassName =>
            new CssBuilder($"text-{Color.ToDescriptionString()}")
            .AddClass(Class)
            .Build();
    }
}
