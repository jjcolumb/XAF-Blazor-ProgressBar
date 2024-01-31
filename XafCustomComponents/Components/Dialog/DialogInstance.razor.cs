// Copyright (c) 2019 Blazored
// Copyright (c) 2020 Adapted by Jonny Larsson, Meinrad Recheis and Contributors

// Copyright (c) 2022 Adapted by Lemsoy Valiente

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using XafCustomComponents.Services;
using XafCustomComponents.Utilities;

namespace XafCustomComponents
{
    public partial class DialogInstance : CustomComponentBase, IDisposable
    {
        private DialogOptions _options = new();
        private string _elementId = "dialog_" + Guid.NewGuid().ToString().Substring(0, 8);

        [CascadingParameter(Name = "RightToLeft")] public bool RightToLeft { get; set; }
        [CascadingParameter] private DialogProvider Parent { get; set; }
        [CascadingParameter] private DialogOptions GlobalDialogOptions { get; set; } = new DialogOptions();

        [Parameter]
        public DialogOptions Options
        {
            get
            {
                if (_options == null)
                    _options = new DialogOptions();
                return _options;
            }
            set => _options = value;
        }

        [Parameter]
        public string Title { get; set; }

        [Parameter]
        public RenderFragment TitleContent { get; set; }

        [Parameter]
        public RenderFragment Content { get; set; }

        [Parameter]
        public Guid Id { get; set; }

        public bool IsVisible {get; set; }
        public DialogPosition Position { get; set; }
        public string Width { get; set; }
        public string MinWidth { get; set; }
        public string MaxWidth { get; set; }
        public string Height { get; set; }
        public string MinHeight { get; set; }
        public string MaxHeight { get; set; }
        public bool CloseOnOutsideClick { get; set; }
        public bool CloseOnEscapeKey { get; set; }
        public bool ShowHeader { get; set; }
        public bool ShowFooter { get; set; }
        public bool CloseButton { get; set; }
        public bool Backdrop { get; set; }

        protected override void OnInitialized()
        {
            ConfigureInstance();
        }

        public void SetOptions(DialogOptions options)
        {
            Options = options;
            ConfigureInstance();
            StateHasChanged();
        }

        public void SetTitle(string title)
        {
            Title = title;
            StateHasChanged();
        }

        /// <summary>
        /// Close and return null. 
        /// 
        /// This is a shorthand of Close(DialogResult.Ok((object)null));
        /// </summary>
        public void Close()
        {
            IsVisible = false;
            Close(DialogResult.Ok<object>(null));
        }

        /// <summary>
        /// Close with dialog result.
        /// 
        /// Usage: Close(DialogResult.Ok(returnValue))
        /// </summary>
        public void Close(DialogResult dialogResult)
        {
            IsVisible = false;
            Parent.DismissInstance(Id, dialogResult);
        }

        /// <summary>
        /// Close and directly pass a return value. 
        /// 
        /// This is a shorthand for Close(DialogResult.Ok(returnValue))
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="returnValue"></param>
        public void Close<T>(T returnValue)
        {
            IsVisible = false;
            var dialogResult = DialogResult.Ok<T>(returnValue);
            Parent.DismissInstance(Id, dialogResult);
        }

        /// <summary>
        /// Cancel the dialog. DialogResult.Cancelled will be set to true
        /// </summary>
        public void Cancel()
        {
            IsVisible = false;
            Close(DialogResult.Cancel());
        }

        private void ConfigureInstance()
        {
            IsVisible = SetVisibility();
            Position = SetPosition();
            Width = SetWidth();
            MinWidth = SetMinWidth();
            MaxWidth = SetMaxWidth();
            Height = SetHeight();
            MinHeight = SetMinHeight();
            MaxHeight = SetMaxHeight();
            ShowHeader = SetHeaderVisibility();
            ShowFooter = SetFooterVisibility();
            CloseButton = SetCloseButton();
            CloseOnOutsideClick = SetCloseOnOutsideClick();
            CloseOnEscapeKey = SetCloseOnEscapeKey();
            Backdrop= SetBackdropVisibility();
            Class = ClassName;
        }

        private bool SetVisibility()
        {
            bool isVisible = true;
            return isVisible;
        }

        private DialogPosition SetPosition()
        {
            DialogPosition position;

            if (Options.Position.HasValue)
            {
                position = Options.Position.Value;
            }
            else if (GlobalDialogOptions.Position.HasValue)
            {
                position = GlobalDialogOptions.Position.Value;
            }
            else
            {
                position = DialogPosition.Center;
            }
            return position;
        }

        private string SetWidth()
        {
            string width = string.Empty;

            if (!string.IsNullOrWhiteSpace(Options.Width))
            {
                width = Options.Width;
            }
            if (!string.IsNullOrWhiteSpace(GlobalDialogOptions.Width))
            {
                width = GlobalDialogOptions.Width;
            }
            return width;
        }

        private string SetMinWidth()
        {
            string width = string.Empty;

            if (!string.IsNullOrWhiteSpace(Options.MinWidth))
            {
                width = Options.MinWidth;
            }
            if (!string.IsNullOrWhiteSpace(GlobalDialogOptions.MinWidth))
            {
                width = GlobalDialogOptions.MinWidth;
            }
            return width;
        }

        private string SetMaxWidth()
        {
            string width = string.Empty;

            if (!string.IsNullOrWhiteSpace(Options.MaxWidth))
            {
                width = Options.MaxWidth;
            }
            if (!string.IsNullOrWhiteSpace(GlobalDialogOptions.MaxWidth))
            {
                width = GlobalDialogOptions.MaxWidth;
            }
            return width;
        }

        private string SetHeight()
        {
            string height = string.Empty;

            if (!string.IsNullOrWhiteSpace(Options.Height))
            {
                height = Options.Height;
            }
            if (!string.IsNullOrWhiteSpace(GlobalDialogOptions.Height))
            {
                height = GlobalDialogOptions.Height;
            }
            return height;
        }

        private string SetMinHeight()
        {
            string height = string.Empty;

            if (!string.IsNullOrWhiteSpace(Options.MinHeight))
            {
                height = Options.MinHeight;
            }
            if (!string.IsNullOrWhiteSpace(GlobalDialogOptions.MinHeight))
            {
                height = GlobalDialogOptions.MinHeight;
            }
            return height;
        }

        private string SetMaxHeight()
        {
            string height = string.Empty;

            if (!string.IsNullOrWhiteSpace(Options.MaxHeight))
            {
                height = Options.MaxHeight;
            }
            if (!string.IsNullOrWhiteSpace(GlobalDialogOptions.MaxHeight))
            {
                height = GlobalDialogOptions.MaxHeight;
            }
            return height;
        }

        protected string ClassName =>
            new CssBuilder()
                .AddClass(_dialog?.Class)
            .Build();

        private bool SetHeaderVisibility()
        {
            if (Options.ShowHeader.HasValue)
                return Options.ShowHeader.Value;

            if (GlobalDialogOptions.ShowHeader.HasValue)
                return GlobalDialogOptions.ShowHeader.Value;

            return true;
        }

        private bool SetFooterVisibility()
        {
            if (Options.ShowFooter.HasValue)
                return Options.ShowFooter.Value;

            if (GlobalDialogOptions.ShowFooter.HasValue)
                return GlobalDialogOptions.ShowFooter.Value;

            return true;
        }

        private bool SetCloseButton()
        {
            if (Options.CloseButton.HasValue)
                return Options.CloseButton.Value;

            if (GlobalDialogOptions.CloseButton.HasValue)
                return GlobalDialogOptions.CloseButton.Value;

            return true;
        }

        private bool SetCloseOnOutsideClick()
        {
            if (Options.CloseOnOutsideClick.HasValue)
                return Options.CloseOnOutsideClick.Value;

            if (GlobalDialogOptions.CloseOnOutsideClick.HasValue)
                return GlobalDialogOptions.CloseOnOutsideClick.Value;

            return true;
        }

        private bool SetCloseOnEscapeKey()
        {
            if (Options.CloseOnEscapeKey.HasValue)
                return Options.CloseOnEscapeKey.Value;

            if (GlobalDialogOptions.CloseOnEscapeKey.HasValue)
                return GlobalDialogOptions.CloseOnEscapeKey.Value;

            return true;
        }

        private bool SetBackdropVisibility()
        {
            if (Options.Backdrop.HasValue)
                return Options.Backdrop.Value;

            if (GlobalDialogOptions.Backdrop.HasValue)
                return GlobalDialogOptions.Backdrop.Value;

            return true;
        }

        private DialogPopup _dialog;
        private bool _disposedValue;

        public void Register(DialogPopup dialog)
        {
            if (dialog == null)
                return;
            _dialog = dialog;
            Class = dialog.Class;
            Style = dialog.Style;
            TitleContent = dialog.TitleContent;
            StateHasChanged();
        }

        public void ForceRender()
        {
            StateHasChanged();
        }

        /// <summary>
        /// Cancels all dialogs in dialog provider collection.
        /// </summary>
        public void CancelAll()
        {
            Parent?.DismissAll();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                _disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
