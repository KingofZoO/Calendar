using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using Calendar.Interfaces;
using Xamarin.Forms;

namespace Calendar.Models {
    public class RecordsRepository {
        private SQLiteConnection database;

        public INotificationManager NotificationManager { get; private set; }

        public RecordsRepository(string path) {
            database = new SQLiteConnection(path);
            database.CreateTable<NoteRecord>();

            NotificationManager = DependencyService.Get<INotificationManager>();
        }

        public IEnumerable<NoteRecord> DayRecords(DateTime day) {
            return database.Query<NoteRecord>("SELECT * FROM NoteRecords " +
                                              "WHERE NoteDate BETWEEN ? AND ? " +
                                              "ORDER BY NoteDate", day.Date, new DateTime(day.Year, day.Month, day.Day, 23, 59, 59));
        }

        public IEnumerable<NoteRecord> RecordsByDaysInterval(DateTime firstDay, DateTime lastDay) {
            return database.Query<NoteRecord>("SELECT * FROM NoteRecords " +
                                              "WHERE NoteDate BETWEEN ? AND ? " +
                                              "ORDER BY NoteDate", firstDay.Date, new DateTime(lastDay.Year, lastDay.Month, lastDay.Day, 23, 59, 59));
        }

        public void SaveRecord(NoteRecord record) {
            if (record.Id != 0)
                database.Update(record);
            else
                database.Insert(record);

            if (record.NotifyDate.HasValue) {
                NotificationManager.ScheduleNotification(record.NoteTime, record.Note, record.NotifyDate.Value, record.Id);
            }
        }

        public void DeleteRecord(NoteRecord record) {
            NotificationManager.CancelNotification(record.Id);
            database.Delete<NoteRecord>(record.Id);
        }

        public void DropRecords() {
            var notifiedRecords = database.Query<NoteRecord>("SELECT * FROM NoteRecords WHERE NotifyDate IS NOT NULL");

            foreach(var record in notifiedRecords) {
                NotificationManager.CancelNotification(record.Id);
            }

            database.DropTable<NoteRecord>();
            database.CreateTable<NoteRecord>();
        }
    }
}
