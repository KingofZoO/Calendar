using System;
using System.Collections.Generic;
using System.Text;

namespace Calendar.Interfaces {
    public interface INotificationManager {
        void ScheduleNotification(string title, string message, DateTime scheduleDate, int id);
        void CancelNotification(int id);
    }
}
