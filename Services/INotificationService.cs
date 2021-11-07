

using MaterialDesignThemes.Wpf;

namespace DaysGoneModManager.Services
{
    public interface INotificationService
    {
        //INotificationService SetNotificationContent(string _title, string _message);

        //void NotifySuccess();
        //void NotifyFailure();
        //void NotifyWarning();
        //void NotifyInfo();
    }

    public class NotificationService : INotificationService
    {
        //NotificationManager NotifyManager { get; set; }
        //NotificationContent NContent { get; set; }

        //DialogHost DialogHost { get; set; }

        //private class NotificationContent
        //{
        //    public string Title { get; set; }
        //    public string Body { get; set; }
        //}

        //public NotificationService() => NotifyManager = new NotificationManager();

        //public void Initialize() =>
        //    NotifyManager.Show("Notifications Active",
        //        "Notifications ready...",
        //        NotificationType.Success);

        //public NotificationService SetNotificationContent(string _title, string _message)
        //{
        //    NContent = new NotificationContent()
        //    {
        //        Title = _title,
        //        Body = _message
        //    };
        //    return this;
        //}

        //private void ClearContent() => NContent = null;

        //public void NotifySuccess() => ShowNotification(NotificationType.Success);

        //public void NotifyFailure() => ShowNotification(NotificationType.Error);

        //public void NotifyWarning() => ShowNotification(NotificationType.Warning);

        //public void NotifyInfo() => ShowNotification(NotificationType.Information);

        //private void ShowNotification(NotificationType notificationType)
        //{
        //    if (NContent != null)
        //    {
        //        NotifyManager.Show(NContent.Title, NContent.Message,
        //            notificationType,
        //            trim: NContent.TrimType,
        //            RowsCountWhenTrim: NContent.RowsCount,
        //            onClose: () => ClearContent());
        //    }
        //}
    }
}