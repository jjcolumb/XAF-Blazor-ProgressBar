using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XafCustomComponents
{
    public partial class ProgressMessageBox : CustomComponentBase
    {
        [CascadingParameter] DialogInstance Dialog { get; set; }
        [Parameter] public BackgroundWorker Worker { get; set; }
        [Parameter] public ProgressIndicatorState ProgressIndicatorState { get; set; }

        public ProgressIndicatorState State { get; set; }
        public string ButtonText { get; set; } = "Cancel";

        protected override void OnInitialized()
        {
            base.OnInitialized();
            Dialog.CloseOnEscapeKey = false;
            Dialog.CloseOnOutsideClick = false;
        }

        private void ProgressChangedHandler(object state)
        {
            State = state as ProgressIndicatorState;
            ButtonText = State?.OperationNum == State?.TotalOperations ? "OK" : "Cancel";
        }

        private void CancelClickedHandler(MouseEventArgs e)
        {
            Dialog?.Cancel();
        }
    }
}
