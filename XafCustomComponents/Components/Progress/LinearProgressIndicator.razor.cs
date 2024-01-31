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
    public partial class LinearProgressIndicator : CustomComponentBase, IDisposable
    {
        /// <summary>
        /// The current value of the progress bar
        /// </summary>
        [Parameter] public int Progress { get; set; } = 0;
        /// <summary>
        /// The maximum value of the progress bar
        /// </summary>
        [Parameter] public int MaxProgress { get; set; } = 100;
        /// <summary>
        /// The minimum value of the progress bar
        /// </summary>
        [Parameter] public int MinProgress { get; set; } = 0;
        /// <summary>
        /// The color of the progress bar
        /// </summary>
        [Parameter] public Color Color { get; set; } = Color.Primary;
        /// <summary>
        /// If true, the progress indicator will show an animated striped bar
        /// </summary>
        [Parameter] public bool Indeterminate { get; set; } = false;
        /// <summary>
        /// If attached, this component will rerender and send to its parent the worker UserState on each ProgressChanged event of the worker
        /// </summary>
        [Parameter] public BackgroundWorker Worker { get; set; }
        /// <summary>
        /// An event triggered each time an attached <see cref="BackgroundWorker"/> ProgressChanged event is fired. Gives you access to the worker UserState
        /// </summary>
        [Parameter] public EventCallback<object> ProgressChanged { get; set; }


        protected override void OnInitialized()
        {
            base.OnInitialized();
            if (Worker is not null)
            {
                Worker.ProgressChanged += Worker_ProgressChanged;
            }
        }

        private void Worker_ProgressChanged(object? sender, ProgressChangedEventArgs e)
        {
            if (Worker.CancellationPending)
                return;
            Progress = e.ProgressPercentage;
            if (e.UserState is not null)
            {
                ProgressChanged.InvokeAsync(e.UserState);
            }
            StateHasChanged();
        }

        public void Dispose()
        {
            //Worker?.CancelAsync();
        }

        public string ProgressClassNameExternalDiv =>
            new CssBuilder("progress")
            .AddClass("my-1")
            .AddClass(Class)
            .Build();

        public string ProgressClassNameInternalDiv =>
            new CssBuilder("progress-bar")
            .AddClass("progress-bar-striped progress-bar-animated", Indeterminate)
            .AddClass($"bg-{Color.ToDescriptionString()}")
            .Build();

        public string PercentageClassName =>
            new CssBuilder("percentage")
            .AddClass($"text-{TextColor.White.ToDescriptionString()}", Progress >= 50)
            .Build();

        public string ProgressBarStyle =>
            new StyleBuilder()
            .AddStyle("width", $"{Progress}%")
            .Build();
    }
}
