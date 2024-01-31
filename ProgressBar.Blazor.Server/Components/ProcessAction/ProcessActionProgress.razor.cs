using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components;
using System.ComponentModel;
using XafCustomComponents;
using XafCustomComponents.Extensions;

namespace ProgressBar.Blazor.Server.Components.ProcessAction
{
    public partial class ProcessActionProgress : ComponentBase
    {
        [Inject] IDialogService DialogService { get; set; }
        [CascadingParameter] DialogInstance Dialog { get; set; }
        [Parameter] public BackgroundWorker Worker { get; set; }

        public ProcessActionState State { get; set; }
        public string CurrentOperationName { get; set; }
        public string OperationElementsDetail { get; set; }
        public string OperationCurrentElementDetail { get; set; }
        public int GlobalPercentage { get; set; }
        public bool Indeterminate { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            Dialog.CloseOnEscapeKey = false;
            Dialog.CloseOnOutsideClick = false;
            Dialog.CloseButton = false;
        }

        private void ProgressChangedHandler(object state)
        {
            State = state as ProcessActionState;
            CurrentOperationName = State.CurrentOperation.ToDescriptionString();
            SetGlobalPercentage(State.CurrentOperationPercentage);
            OperationElementsDetail = State.OperationElementsDetail;
            OperationCurrentElementDetail = State.OperationCurrentElementDetail;
            Indeterminate = State.Indeterminate;
        }

        private void SetGlobalPercentage(int currentOperationPercentage)
        {
            GlobalPercentage = (int)(State.CurrentOperationIncrementalCost * 100 + Math.Floor(State.CurrentOperationCost * currentOperationPercentage));
        }

        private async void CancelClickedHandler(MouseEventArgs e)
        {
            bool? cancelOperation = await DialogService.ShowMessageBox("Cancel Operation", "Are you sure you want to cancel this operation?", yesText: "Yes", noText: "No");
            if (cancelOperation is not null && (bool)cancelOperation)
                Dialog?.Cancel();
        }

        private void OkClickedHandler(MouseEventArgs e)
        {
            Dialog.Close();
        }
    }
}
