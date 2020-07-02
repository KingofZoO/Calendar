using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;
using Calendar.Models;

namespace Calendar.ViewModels {
    public class NewRecordViewModel : INotifyPropertyChanged {
        private TimeSpan time = new TimeSpan(12, 0, 0);
        private string note;
        private NoteRecord newRecord = new NoteRecord();

        public DayViewModel DayViewModel { get; private set; }

        public ICommand AddRecordCommand { get; private set; }

        public NewRecordViewModel(DayViewModel vm, NoteRecord record = null) {
            DayViewModel = vm;

            if (record != null) {
                Time = record.NoteDate - vm.Date;
                Note = record.Note;
                newRecord = record;
            }

            AddRecordCommand = new Command(AddRecord);
        }

        public TimeSpan Time {
            get => time;
            set {
                if (time != value) {
                    time = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Note {
            get => note;
            set {
                if (note != value) {
                    note = value;
                    OnPropertyChanged();
                }
            }
        }

        private void AddRecord() {
            if (!string.IsNullOrEmpty(Note)) {
                newRecord.NoteDate = DayViewModel.Date.Date + Time;
                newRecord.Note = Note;

                App.DataBase.SaveRecord(newRecord);
                DayViewModel.BackToDayView();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
