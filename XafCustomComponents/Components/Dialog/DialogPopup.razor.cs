using DevExpress.Blazor;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;

namespace XafCustomComponents
{
    public partial class DialogPopup : CustomComponentBase
    {
        [CascadingParameter] private DialogInstance DialogInstance { get; set; }

        [Inject] public IDialogService DialogService { get; set; }

        [Parameter]
        public RenderFragment TitleContent { get; set; }
        [Parameter]
        public RenderFragment DialogContent { get; set; }

        [Parameter]
        public RenderFragment DialogActions { get; set; }
        [Parameter]
        public DialogOptions Options { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            DialogInstance?.Register(this);
        }

        private void ClosedHandler(PopupClosedEventArgs e)
        {
            DialogInstance.Cancel();
        }

        private Dictionary<DialogPosition, List<object>> PositionMap = new Dictionary<DialogPosition, List<object>>()
        {
            { DialogPosition.Center, new List<object> { HorizontalAlignment.Center, VerticalAlignment.Center } },
            { DialogPosition.TopCenter, new List<object> { HorizontalAlignment.Center, VerticalAlignment.Top } },
            { DialogPosition.BottomCenter, new List<object> { HorizontalAlignment.Center, VerticalAlignment.Bottom } },
            { DialogPosition.CenterLeft, new List<object> { HorizontalAlignment.Left, VerticalAlignment.Center } },
            { DialogPosition.CenterRight, new List<object> { HorizontalAlignment.Right, VerticalAlignment.Center } },
            { DialogPosition.TopLeft, new List<object> { HorizontalAlignment.Left, VerticalAlignment.Top } },
            { DialogPosition.TopRight, new List<object> { HorizontalAlignment.Right, VerticalAlignment.Top } },
            { DialogPosition.BottomLeft, new List<object> { HorizontalAlignment.Left, VerticalAlignment.Bottom } },
            { DialogPosition.BottomRight, new List<object> { HorizontalAlignment.Right, VerticalAlignment.Bottom } },
        };
    }
}
