using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using Calendar.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;
using Calendar.Views;
using System.Linq;

namespace Calendar.ViewModels {
    public class MonthViewModel : INotifyPropertyChanged {
        private string month;
        private int year;
        private DateTime currentMonthDate;

        public string[] MonthsName = new[] {
            "Январь",
            "Февраль",
            "Март",
            "Апрель",
            "Май",
            "Июнь",
            "Июль",
            "Август",
            "Сентябрь",
            "Октябрь",
            "Ноябрь",
            "Декабрь"
        };

        private DateTime CurrentMonthDate {
            get => currentMonthDate;
            set {
                currentMonthDate = value;
                Month = StringMonth(currentMonthDate.Month);
                Year = currentMonthDate.Year;

                CalendarSettings.SetAppTheme(currentMonthDate.Month - 1);
                FillCalendarData();
            }
        }

        public DayCellViewModel[] CalendarDays { get; private set; } = new DayCellViewModel[42];

        public INavigation Navigation { get; set; }

        public ICommand NextMonthCommand { get; private set; }
        public ICommand PrevMonthCommand { get; private set; }
        public ICommand YearViewCommand { get; private set; }
        public ICommand DayViewCommand { get; private set; }
        public ICommand SettingsViewCommand { get; private set; }

        public ICommand BackCommand { get; private set; }
        public ICommand BackToCurrentMonthCommand { get; private set; }

        public MonthViewModel(DateTime date) {
            PrepareCalendarData();
            CurrentMonthDate = date;

            NextMonthCommand = new Command(NextMonth);
            PrevMonthCommand = new Command(PrevMonth);
            YearViewCommand = new Command(ShowYearView);
            DayViewCommand = new Command<int>(ShowDayView);
            SettingsViewCommand = new Command(ShowSettingsVew);

            BackCommand = new Command(Back);
            BackToCurrentMonthCommand = new Command(ToCurrentMonth);
        }

        public string StringMonth(int monthIndex) => MonthsName[monthIndex - 1];

        public string Month {
            get => month;
            set {
                if (month != value) {
                    month = value;
                    OnPropertyChanged();
                }
            }
        }

        public int Year {
            get => year;
            set {
                if (year != value) {
                    year = value;
                    OnPropertyChanged();
                }
            }
        }

        private void PrepareCalendarData() {
            for (int i = 0; i < CalendarDays.Length; i++) {
                CalendarDays[i] = new DayCellViewModel();
            }
        }

        private void FillCalendarData() {
            DateTime firstMonthDay = new DateTime(CurrentMonthDate.Year, CurrentMonthDate.Month, 1);
            int prevMonthDays = DayOfWeek(firstMonthDay);
            DateTime counterDay = firstMonthDay.AddDays(-prevMonthDays);

            IEnumerable<NoteRecord> monthRecords = App.DataBase.RecordsByDaysInterval(counterDay, counterDay.AddDays(42));
            IEnumerable<NoteRecord> repeatRecords = App.DataBase.RepeatRecords();

            for (int i = 0; i < CalendarDays.Length; i++) {
                DayCellViewModel currDay = CalendarDays[i];
                currDay.Date = counterDay;

                currDay.IsNoted = monthRecords.Any(el => el.NoteDate.Day == counterDay.Day) ||
                                  repeatRecords.Any(el => (el.RepeatCode == RepeatInfo.MonthRepeatCode && el.NoteDate.Day == counterDay.Day) ||
                                  el.RepeatCode == RepeatInfo.YearRepeatCode && el.NoteDate.Month == counterDay.Month && el.NoteDate.Day == counterDay.Day);

                if (currDay.Date == DateTime.Now.Date)
                    currDay.Style = (Style)Application.Current.Resources["TodaysButton"];
                else if (currDay.Date.Month != CurrentMonthDate.Month)
                    currDay.Style = (Style)Application.Current.Resources["PrevNextButton"];
                else
                    currDay.Style = (Style)Application.Current.Resources["DefaultButton"];

                counterDay = counterDay.AddDays(1);
            }
        }

        private int DayOfWeek(DateTime day) {
            int dayOfWeek = (int)day.DayOfWeek;
            return dayOfWeek == 0 ? 6 : dayOfWeek - 1;
        }

        private void NextMonth() {
            CurrentMonthDate = CurrentMonthDate.AddMonths(1);
        }

        private void PrevMonth() {
            CurrentMonthDate = CurrentMonthDate.AddMonths(-1);
        }

        private void ShowYearView() {
            Navigation.PushAsync(new YearView() {
                BindingContext = new YearViewModel(this)
            });
        }

        private void ShowDayView(int dayIndex) {
            Navigation.PushAsync(new DayView() {
                BindingContext = new DayViewModel(this, CalendarDays[dayIndex].Date)
            });
        }

        private void ShowSettingsVew() {
            Navigation.PushAsync(new SettingsView() {
                BindingContext = new SettingsViewModel(this)
            });
        }

        private void ToCurrentMonth() {
            CurrentMonthDate = DateTime.Today;
        }

        public void BackToMonthView(DateTime date) {
            CurrentMonthDate = date;
            Back();
        }

        public void Back() => Navigation.PopAsync();

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
