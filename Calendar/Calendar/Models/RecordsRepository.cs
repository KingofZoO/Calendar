using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using Calendar.Interfaces;
using Xamarin.Forms;
using System.Linq;

namespace Calendar.Models {
    public class RecordsRepository {
        private SQLiteConnection database;

        public INotificationManager NotificationManager { get; private set; }

        public RecordsRepository(string path) {
            database = new SQLiteConnection(path);
            database.CreateTable<NoteRecord>();

            NotificationManager = DependencyService.Get<INotificationManager>();

            UpdateRepeatedNotifications();
        }

        public IEnumerable<NoteRecord> DayRecords(DateTime day) {
            List<NoteRecord> resultRecords = database.Query<NoteRecord>("SELECT * FROM NoteRecords WHERE RepeatCode = ? AND NoteDate BETWEEN ? and ?", 
                RepeatInfo.NoRepeatCode, day.Date, new DateTime(day.Year, day.Month, day.Day, 23, 59, 59));

            IEnumerable<NoteRecord> repeatRecords = RepeatRecords();

            foreach(var rec in repeatRecords) {
                bool monthCheck = rec.RepeatCode == RepeatInfo.MonthRepeatCode && rec.NoteDate.Day == day.Day;
                bool yearCheck = rec.RepeatCode == RepeatInfo.YearRepeatCode && rec.NoteDate.Month == day.Month && rec.NoteDate.Day == day.Day;

                if (monthCheck || yearCheck)
                    resultRecords.Add(rec);
            }

            return resultRecords.OrderBy(rec => rec.NoteDate);
        }

        public IEnumerable<NoteRecord> RecordsByDaysInterval(DateTime firstDay, DateTime lastDay) {
            return database.Query<NoteRecord>("SELECT * FROM NoteRecords " +
                                              "WHERE RepeatCode = ? AND " +
                                              "NoteDate BETWEEN ? AND ? " +
                                              "ORDER BY NoteDate", RepeatInfo.NoRepeatCode, firstDay.Date, new DateTime(lastDay.Year, lastDay.Month, lastDay.Day, 23, 59, 59));
        }

        public IEnumerable<NoteRecord> RepeatRecords() {
            return database.Query<NoteRecord>("SELECT * FROM NoteRecords WHERE RepeatCode != ?", RepeatInfo.NoRepeatCode);
        }

        public IEnumerable<NoteRecord> RepeatNotifiedRecords() {
            return database.Query<NoteRecord>("SELECT * FROM NoteRecords WHERE RepeatCode != ? AND NotifyDate IS NOT NULL", RepeatInfo.NoRepeatCode);
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

        private void UpdateRepeatedNotifications() {
            foreach(var rec in RepeatNotifiedRecords()) {
                if(DateTime.Now > rec.NotifyDate) {
                    switch (rec.RepeatCode) {
                        case RepeatInfo.MonthRepeatCode:
                            rec.NotifyDate = rec.NotifyDate.Value.AddMonths(1);
                            break;
                        case RepeatInfo.YearRepeatCode:
                            rec.NotifyDate = rec.NotifyDate.Value.AddYears(1);
                            break;
                    }
                    SaveRecord(rec);
                }
            }
        }
    }
}
