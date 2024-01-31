using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using ProgressBar.Blazor.Server.Components.ProcessAction;
using ProgressBar.Module.BusinessObjects;
using ProgressBar.Module.Enums;
using ProgressBar.Module.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using XafCustomComponents;

namespace ProgressBar.Blazor.Server.Controllers
{
    public partial class ProgressBarActionController : ViewController
    {
        IServiceProvider ServiceProvider;
        IDialogService DialogService;
        SimpleAction ProcessAction;
        BackgroundWorker ProcessActionWorker;
        WorkerStatus ProcessActionWorkerStatus;
        public ProgressBarActionController()
        {
            InitializeComponent();
            TargetObjectType = typeof(ProgressBarClass);
            ProcessAction = new SimpleAction(this, "TotalPackWeight_ProcessAction", PredefinedCategory.RecordEdit)
            {
                Caption = "Process",
            };
            ProcessAction.Execute += ProcessAction_Execute;
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            ServiceProvider = Application?.ServiceProvider;
            DialogService = ServiceProvider?.GetRequiredService<IDialogService>();
        }

        protected override void OnDeactivated()
        {
            ReleaseWorkers();
            base.OnDeactivated();
        }

        private async void ProcessAction_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            InitializeProcessActionWorker();
            ProcessActionWorker.RunWorkerAsync();

            DialogParameters parameters = new DialogParameters() { { "Worker", ProcessActionWorker } };
            IDialogReference dialog = DialogService.Show<ProcessActionProgress>("Process Action", parameters);

            var result = await dialog.Result;
            if (result.Cancelled)
                ProcessActionWorkerStatus = WorkerStatus.Cancelled;
        }

        private void InitializeProcessActionWorker()
        {
            ProcessActionWorker = new BackgroundWorker() { WorkerReportsProgress = true, WorkerSupportsCancellation = true };
            ProcessActionWorker.DoWork += ProcessActionWorker_DoWork;
            ProcessActionWorker.RunWorkerCompleted += ProcessActionWorker_RunWorkerCompleted;
            ProcessActionWorkerStatus = WorkerStatus.Running;
        }

        private void ProcessActionWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            IObjectSpace objectSpace = Application.CreateObjectSpace(typeof(ProgressBarClass));

            Operation1(e);
            Operation2(e);
            Operation3(e);
            Operation4(e);
        }

        private void ProcessActionWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error is not null)
            {
                //TODO: Show exception message
            }
            else if (e.Cancelled)
            {
                //View.ObjectSpace.Rollback(false);
            }
            else
            {
                if (View.ObjectSpace.IsModified)
                {
                    //View.ObjectSpace.CommitChanges();
                }
            }
            ProcessActionWorkerStatus = WorkerStatus.Stopped;
            ProcessActionWorker?.Dispose();
        }

        private void Operation1(DoWorkEventArgs e)
        {
            if (ProcessActionWorkerStatus != WorkerStatus.Running)
            {
                e.Cancel = true;
                return;
            }
            int percentage = 0;
            ProcessActionWorker.ReportProgress(percentage);
            ProcessActionState state = new ProcessActionState();
            state.CurrentOperation = ProcessOperation.Operation1;
            System.Threading.Thread.Sleep(500);
            for (int i = 1; i <= 10; i++)
            {
                if (ProcessActionWorkerStatus != WorkerStatus.Running)
                {
                    e.Cancel = true;
                    return;
                }
                percentage = ProgressIndicatorHelper.GetIntegerPercentage(i, 10);
                state.CurrentOperationPercentage = percentage;
                state.OperationElementsDetail = $"Calculating: item {i} of {10}";
                state.OperationCurrentElementDetail = $"Name of item {i}";
                ProcessActionWorker.ReportProgress(percentage, state);
                System.Threading.Thread.Sleep(500);
            }
        }
        private void Operation2(DoWorkEventArgs e)
        {
            if (ProcessActionWorkerStatus != WorkerStatus.Running)
            {
                e.Cancel = true;
                return;
            }
            int percentage = 0;
            ProcessActionWorker.ReportProgress(percentage);
            ProcessActionState state = new ProcessActionState();
            state.CurrentOperation = ProcessOperation.Operation2;
            System.Threading.Thread.Sleep(500);
            for (int i = 1; i <= 10; i++)
            {
                if (ProcessActionWorkerStatus != WorkerStatus.Running)
                {
                    e.Cancel = true;
                    return;
                }
                percentage = ProgressIndicatorHelper.GetIntegerPercentage(i, 10);
                state.CurrentOperationPercentage = percentage;
                state.OperationElementsDetail = $"Calculating: item {i} of {10}";
                state.OperationCurrentElementDetail = $"Name of item {i}";
                ProcessActionWorker.ReportProgress(percentage, state);
                System.Threading.Thread.Sleep(500);
            }
        }
        private void Operation3(DoWorkEventArgs e)
        {
            if (ProcessActionWorkerStatus != WorkerStatus.Running)
            {
                e.Cancel = true;
                return;
            }
            int percentage = 0;
            ProcessActionWorker.ReportProgress(percentage);
            ProcessActionState state = new ProcessActionState();
            state.CurrentOperation = ProcessOperation.Operation3;
            System.Threading.Thread.Sleep(500);
            for (int i = 1; i <= 10; i++)
            {
                if (ProcessActionWorkerStatus != WorkerStatus.Running)
                {
                    e.Cancel = true;
                    return;
                }
                percentage = ProgressIndicatorHelper.GetIntegerPercentage(i, 10);
                state.CurrentOperationPercentage = percentage;
                state.OperationElementsDetail = $"Calculating: item {i} of {10}";
                state.OperationCurrentElementDetail = $"Name of item {i}";
                ProcessActionWorker.ReportProgress(percentage, state);
                System.Threading.Thread.Sleep(500);
            }
        }

        private void Operation4(DoWorkEventArgs e)
        {
            if (ProcessActionWorkerStatus != WorkerStatus.Running)
            {
                e.Cancel = true;
                return;
            }
            int percentage = 0;
            ProcessActionWorker.ReportProgress(percentage);
            ProcessActionState state = new ProcessActionState();
            state.CurrentOperation = ProcessOperation.Operation4;
            System.Threading.Thread.Sleep(2000);
            for (int i = 1; i <= 15; i++)
            {
                if (ProcessActionWorkerStatus != WorkerStatus.Running)
                {
                    e.Cancel = true;
                    return;
                }
                percentage = ProgressIndicatorHelper.GetIntegerPercentage(i, 15);
                state.CurrentOperationPercentage = percentage;
                state.OperationElementsDetail = $"Calculating: item {i} of {15}";
                state.OperationCurrentElementDetail = $"Name of item {i}";
                ProcessActionWorker.ReportProgress(percentage, state);
                System.Threading.Thread.Sleep(500);
            }
        }

        private void ReleaseWorkers()
        {
            ProcessActionWorkerStatus = WorkerStatus.Stopped;
            ProcessActionWorker?.Dispose();
        }
    }
}
