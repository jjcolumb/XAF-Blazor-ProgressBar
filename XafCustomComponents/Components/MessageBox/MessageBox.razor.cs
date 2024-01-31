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
    public partial class MessageBox : CustomComponentBase
    {
        [Inject] private IDialogService DialogService { get; set; }

        [CascadingParameter] private DialogInstance DialogInstance { get; set; }

        /// <summary>
        /// The message box title. If null or empty, title will be hidden
        /// </summary>
        [Parameter]
        public string Title { get; set; }

        [Parameter]
        public TextColor TitleColor { get; set; }

        [Parameter]
        public TextType TitleType { get; set; }

        /// <summary>
        /// Define the message box title as a renderfragment (overrides Title)
        /// </summary>
        [Parameter]
        public RenderFragment TitleContent { get; set; }

        /// <summary>
        /// The message box message as string.
        /// </summary>
        [Parameter]
        public string Message { get; set; }

        /// <summary>
        /// The message box message as markup string.
        /// </summary>
        [Parameter]
        public MarkupString MarkupMessage { get; set; }

        /// <summary>
        /// Define the message box body as a renderfragment (overrides Message)
        /// </summary>
        [Parameter]
        public RenderFragment MessageContent { get; set; }


        /// <summary>
        /// Text of the cancel button. Leave null to hide the button.
        /// </summary>
        [Parameter]
        public string CancelText { get; set; }

        /// <summary>
        /// Define the cancel button as a render fragment (overrides CancelText).
        /// </summary>
        [Parameter]
        public RenderFragment CancelButton { get; set; }

        /// <summary>
        /// Text of the no button. Leave null to hide the button.
        /// </summary>
        [Parameter]
        public string NoText { get; set; }

        /// <summary>
        /// Define the no button as a render fragment (overrides CancelText).
        /// </summary>
        [Parameter]
        public RenderFragment NoButton { get; set; }

        /// <summary>
        /// Text of the yes/OK button. Leave null to hide the button.
        /// </summary>
        [Parameter]
        public string YesText { get; set; } = "OK";

        /// <summary>
        /// Define the cancel button as a render fragment (overrides CancelText).
        /// </summary>
        [Parameter]
        public RenderFragment YesButton { get; set; }

        private void YesButtonClickHandler()
        {
            DialogInstance.Close(DialogResult.Ok(true));
        }

        private void NoButtonClickHandler()
        {
            DialogInstance.Close(DialogResult.Ok(false));
        }

        private void CancelButtonClickHandler()
        {
            DialogInstance.Cancel();
        }

        private string TitleClassName =>
            new CssBuilder($"text-{TitleColor.ToDescriptionString()}")
            .Build();
    }
}
