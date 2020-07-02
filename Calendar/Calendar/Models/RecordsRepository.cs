using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Calendar.Models {
    public class RecordsRepository {
        private SQLiteConnection database;

        public RecordsRepository(string path) {
            database = new SQLiteConnection(path);
            database.CreateTable<NoteRecord>();
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
        }

        public void DeleteRecord(NoteRecord record) {
            database.Delete<NoteRecord>(record.Id);
        }
    }
}
