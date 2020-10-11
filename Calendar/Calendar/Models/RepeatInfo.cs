using System;
using System.Collections.Generic;
using System.Text;

namespace Calendar.Models {
    public static class RepeatInfo {
        public const int NoRepeatCode = -1;
        public const int YearRepeatCode = 0;
        public const int MonthRepeatCode = 1;
        public const int WeekRepeatCode = 2;

        public static string[] RepeatStringCodes = new string[] { "Ежегодно", "Ежемесячно", "Еженедельно"};
    }
}
