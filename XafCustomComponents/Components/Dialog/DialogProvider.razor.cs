// Copyright (c) 2020 Jonny Larsson
// License: MIT
// See https://github.com/MudBlazor/MudBlazor
// Modified version of Blazored Modal
// Copyright (c) 2019 Blazored
// License: MIT
// See https://github.com/Blazored

// Copyright (c) 2022 Adapted by Lemsoy Valiente

using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;

namespace XafCustomComponents
{
    public partial class DialogProvider : IDisposable
    {
        [Inject] private IDialogService DialogService { get; set; }
        [Inject] private NavigationManager NavigationManager { get; set; }

        [Parameter] public bool? ShowHeader { get; set; }
        [Parameter] public bool? ShowFooter { get; set; }
        [Parameter] public bool? CloseButton { get; set; }
        [Parameter] public bool? CloseOnOutsideClick { get; set; }
        [Parameter] public bool? CloseOnEscapeKey { get; set; }
        [Parameter] public string Width { get; set; }
        [Parameter] public string MinWidth { get; set; }
        [Parameter] public string MaxWidth { get; set; }
        [Parameter] public string Height { get; set; }
        [Parameter] public string MinHeight { get; set; }
        [Parameter] public string MaxHeight { get; set; }
        [Parameter] public DialogPosition? Position { get; set; }
        [Parameter] public bool? Backdrop { get; set; }

        private readonly Collection<IDialogReference> _dialogs = new();
        private readonly DialogOptions _globalDialogOptions = new();

        protected override void OnInitialized()
        {
            DialogService.OnDialogInstanceAdded += AddInstance;
            DialogService.OnDialogCloseRequested += DismissInstance;
            NavigationManager.LocationChanged += LocationChanged;

            _globalDialogOptions.CloseOnOutsideClick = CloseOnOutsideClick;
            _globalDialogOptions.CloseOnEscapeKey = CloseOnEscapeKey;
            _globalDialogOptions.CloseButton = CloseButton;
            _globalDialogOptions.ShowHeader = ShowHeader;
            _globalDialogOptions.ShowFooter = ShowFooter;
            _globalDialogOptions.Position = Position;
            _globalDialogOptions.Width = Width;
            _globalDialogOptions.MinWidth = MinWidth;
            _globalDialogOptions.MaxWidth = MaxWidth;
            _globalDialogOptions.Height = Width;
            _globalDialogOptions.MinHeight = MinHeight;
            _globalDialogOptions.MaxHeight = MaxHeight;
            _globalDialogOptions.Backdrop = Backdrop;
        }

        protected override Task OnAfterRenderAsync(bool firstRender)
        {
            if (!firstRender)
            {
                foreach (var dialogReference in _dialogs.Where(x => !x.Result.IsCompleted))
                {
                    dialogReference.RenderCompleteTaskCompletionSource.TrySetResult(true);
                }
            }

            return base.OnAfterRenderAsync(firstRender);
        }

        internal void DismissInstance(Guid id, DialogResult result)
        {
            var reference = GetDialogReference(id);
            if (reference != null)
                DismissInstance(reference, result);
        }

        private void AddInstance(IDialogReference dialog)
        {
            _dialogs.Add(dialog);
            StateHasChanged();
        }

        public void DismissAll()
        {
            _dialogs.ToList().ForEach(r => DismissInstance(r, DialogResult.Cancel()));
            StateHasChanged();
        }

        private void DismissInstance(IDialogReference dialog, DialogResult result)
        {
            if (!dialog.Dismiss(result)) return;

            _dialogs.Remove(dialog);
            StateHasChanged();
        }

        private IDialogReference GetDialogReference(Guid id)
        {
            return _dialogs.SingleOrDefault(x => x.Id == id);
        }

        private void LocationChanged(object sender, LocationChangedEventArgs args)
        {
            DismissAll();
        }

        public void Dispose()
        {
            if (NavigationManager != null)
                NavigationManager.LocationChanged -= LocationChanged;

            if (DialogService != null)
            {
                DialogService.OnDialogInstanceAdded -= AddInstance;
                DialogService.OnDialogCloseRequested -= DismissInstance;
            }
        }
    }
}
