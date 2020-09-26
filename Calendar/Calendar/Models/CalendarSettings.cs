using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Calendar.Models {
    public static class CalendarSettings {
        public static MonthSettings[] MonthSettings;

        static CalendarSettings() {
            SetMonthSettings();
        }

        private static void SetMonthSettings() {
            MonthSettings = new MonthSettings[12] {
                new MonthSettings(Color.FromHex("19598B"), Color.FromHex("72B3E5"), Color.FromHex("052F4F"), Color.FromHex("ADCDE5")), // январь 225
                new MonthSettings(Color.FromHex("0F8282"), Color.FromHex("69E3E3"), Color.FromHex("034949"), Color.FromHex("A7E3E3")), // февраль 210
                new MonthSettings(Color.FromHex("D9B81A"), Color.FromHex("F6E072"), Color.FromHex("7A6606"), Color.FromHex("F6EBB5")), // март 90
                new MonthSettings(Color.FromHex("D9C71A"), Color.FromHex("F6EA72"), Color.FromHex("7A7006"), Color.FromHex("F6F1B5")), // апрель 105
                new MonthSettings(Color.FromHex("D9D91A"), Color.FromHex("F6F672"), Color.FromHex("7A7A06"), Color.FromHex("F6F6B5")), // май 120
                new MonthSettings(Color.FromHex("8FCA18"), Color.FromHex("C8F370"), Color.FromHex("4E7205"), Color.FromHex("DEF3B3")), // июнь 150
                new MonthSettings(Color.FromHex("64C117"), Color.FromHex("AAF16F"), Color.FromHex("346D05"), Color.FromHex("CEF1B2")), // июль 165
                new MonthSettings(Color.FromHex("15AD15"), Color.FromHex("6DED6D"), Color.FromHex("046204"), Color.FromHex("AEEDAE")), // август 180
                new MonthSettings(Color.FromHex("D9871A"), Color.FromHex("F6BE72"), Color.FromHex("7A4806"), Color.FromHex("F6DBB5")), // сентябрь 45
                new MonthSettings(Color.FromHex("D9701A"), Color.FromHex("F6AE72"), Color.FromHex("7A3A06"), Color.FromHex("F6D3B5")), // октябрь 30
                new MonthSettings(Color.FromHex("D9501A"), Color.FromHex("F69872"), Color.FromHex("7A2706"), Color.FromHex("F6C8B5")), // ноябрь 15
                new MonthSettings(Color.FromHex("1F4191"), Color.FromHex("7899E7"), Color.FromHex("071D52"), Color.FromHex("B0C1E7")) // декабрь 240
            };
        }
    }

    public class MonthSettings {
        public Color DefaultColor { get; private set; }
        public Color TodaysColor { get; private set; }
        public Color PrevNextColor { get; private set; }
        public Color BackgroundColor { get; private set; }

        public MonthSettings(Color defaultColor, Color todaysColor, Color prevNextColor, Color backgroundColor) {
            DefaultColor = defaultColor;
            TodaysColor = todaysColor;
            PrevNextColor = prevNextColor;
            BackgroundColor = backgroundColor;
        }
    }
}
