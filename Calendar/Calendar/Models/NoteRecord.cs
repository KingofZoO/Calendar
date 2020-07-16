using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Calendar.Models {
    [Table("NoteRecords")]
    public class NoteRecord {
        [PrimaryKey, AutoIncrement, Column("id")]
        public int Id { get; set; }

        public DateTime NoteDate { get; set; }
        public DateTime? NotifyDate { get; set; }
        public string Note { get; set; }

        [Indexed]
        public int RepeatCode { get; set; } = RepeatInfo.NoRepeatCode;

        [Ignore]
        public string NoteTime => NoteDate.ToString("HH:mm");
    }
}
