using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XafCustomComponents
{
    /// <summary>
    /// Contains methods to display Text Notifications
    /// </summary>
    public interface INotificationService
    {
        /// <summary>
        /// Shows an Error Text Notification
        /// </summary>
        /// <param name="message">A string which is a notification message.</param>
        /// <param name="duration">An integer that specifies the duration of the notification (in milliseconds)</param>
        void ShowErrorMessage(string message, int duration = 3000);
        /// <summary>
        /// Shows an Info Text Notification
        /// </summary>
        /// <param name="message">A string which is a notification message.</param>
        /// <param name="duration">An integer that specifies the duration of the notification (in milliseconds)</param>
        void ShowInfoMessage(string message, int duration = 3000);
        /// <summary>
        /// Shows a Success Text Notification
        /// </summary>
        /// <param name="message">A string which is a notification message.</param>
        /// <param name="duration">An integer that specifies the duration of the notification (in milliseconds)</param>
        void ShowSuccessMessage(string message, int duration = 3000);
        /// <summary>
        /// Shows a Warning Text Notification
        /// </summary>
        /// <param name="message">A string which is a notification message.</param>
        /// <param name="duration">An integer that specifies the duration of the notification (in milliseconds)</param>
        void ShowWarningMessage(string message, int duration = 3000);
    }
}
