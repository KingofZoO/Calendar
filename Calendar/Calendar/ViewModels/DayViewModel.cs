using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using System.Collections.ObjectModel;
using Calendar.Models;
using System.Windows.Input;
using Xamarin.Forms;
using Calendar.Views;

namespace Calendar.ViewModels {
    public class DayViewModel : INotifyPropertyChanged {
        private DateTime date;
        private string month;

        private IEnumerable<NoteRecord> noteRecords;

        public MonthViewModel MonthViewModel { get; private set; }

        public ICommand NewNoteCommand { get; private set; }
        public ICommand MonthViewCommand { get; private set; }
        public ICommand NextDayCommand { get; private set; }
        public ICommand PrevDayCommand { get; private set; }
        public ICommand ChangeRecordCommand { get; private set; }
        public ICommand RemoveRecordCommand { get; private set; }

        public DayViewModel(MonthViewModel vm, DateTime date) {
            MonthViewModel = vm;
            Date = date;

            NewNoteCommand = new Command<NoteRecord>(ShowNewNoteView);
            MonthViewCommand = new Command(ShowMonthView);
            PrevDayCommand = new Command(PrevDay);
            NextDayCommand = new Command(NextDay);
            RemoveRecordCommand = new Command<NoteRecord>(RemoveRecord);
            ChangeRecordCommand = new Command<NoteRecord>(ChangeRecord);
        }

        public IEnumerable<NoteRecord> NoteRecords {
            get => noteRecords;
            set {
                if (noteRecords != value) {
                    noteRecords = value;
                    OnPropertyChanged();
                }
            }
        }

        public DateTime Date {
            get => date;
            set {
                if (date != value) {
                    date = value;
                    OnPropertyChanged();

                    Month = MonthViewModel.StringMonth(Date.Month);
                    NoteRecords = App.DataBase.DayRecords(Date);
                }
            }
        }

        public string Month {
            get => month;
            set {
                if (month != value) {
                    month = value;
                    OnPropertyChanged();
                }
            }
        }

        private void ShowMonthView() {
            MonthViewModel.BackToMonthView(Date);
        }

        private void NextDay() => Date = Date.AddDays(1);

        private void PrevDay() => Date = Date.AddDays(-1);

        private void ShowNewNoteView(NoteRecord record = null) {
            MonthViewModel.Navigation.PushAsync(new NewRecordView() {
                BindingContext = new NewRecordViewModel(this, record)
            });
        }

        private void ChangeRecord(NoteRecord record) => ShowNewNoteView(record);

        private void RemoveRecord(NoteRecord record) {
            App.DataBase.DeleteRecord(record);
            NoteRecords = App.DataBase.DayRecords(Date);
        }

        public void BackToDayView() {
            NoteRecords = App.DataBase.DayRecords(Date);
            MonthViewModel.Back();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
