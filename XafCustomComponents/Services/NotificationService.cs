using DevExpress.ExpressApp.Blazor.Services;
using DevExpress.ExpressApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XafCustomComponents
{
    public class NotificationService : INotificationService
    {
        private readonly IXafApplicationProvider _applicationProvider;

        public NotificationService(IXafApplicationProvider applicationProvider)
        {
            _applicationProvider = applicationProvider;
        }

        private XafApplication XafApplication => _applicationProvider.GetApplication();


        public void ShowErrorMessage(string message, int duration)
        {
            ShowMessage(message, duration, InformationType.Error);
        }

        public void ShowInfoMessage(string message, int duration)
        {
            ShowMessage(message, duration, InformationType.Info);
        }

        public void ShowSuccessMessage(string message, int duration)
        {
            ShowMessage(message, duration, InformationType.Success);
        }

        public void ShowWarningMessage(string message, int duration)
        {
            ShowMessage(message, duration, InformationType.Warning);
        }

        private void ShowMessage(string message, int duration, InformationType messageType)
        {
            MessageOptions options = new MessageOptions();
            options.Duration = duration;
            options.Type = messageType;
            options.Web.Position = InformationPosition.Right;
            string displayMessage = message;
            options.Message = displayMessage;
            XafApplication.ShowViewStrategy.ShowMessage(options);
        }
    }
}
