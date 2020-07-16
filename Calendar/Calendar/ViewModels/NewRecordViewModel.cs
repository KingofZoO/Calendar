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
        private TimeSpan noteTime = new TimeSpan(12, 0, 0);
        private TimeSpan notifyTime = new TimeSpan(10, 0, 0);
        private string note;
        private bool isNotifyOn = false;
        private bool isRepeatOn = false;
        private int repeatIndex = 0;

        public string[] RepeatIntervals { get; private set; } = RepeatInfo.RepeatStringCodes;

        private NoteRecord newRecord = new NoteRecord();

        public DayViewModel DayViewModel { get; private set; }

        public ICommand AddRecordCommand { get; private set; }

        public NewRecordViewModel(DayViewModel vm, NoteRecord record = null) {
            DayViewModel = vm;

            if (record != null) {
                NoteTime = record.NoteDate - vm.Date;                
                Note = record.Note;

                if (record.NotifyDate.HasValue) {
                    NotifyTime = record.NotifyDate.Value - vm.Date;
                    IsNotifyOn = true;
                }
                
                if(record.RepeatCode != RepeatInfo.NoRepeatCode) {
                    RepeatIndex = record.RepeatCode;
                    IsRepeatOn = true;
                }

                newRecord = record;
            }

            AddRecordCommand = new Command(AddRecord);
        }

        public TimeSpan NoteTime {
            get => noteTime;
            set {
                if (noteTime != value) {
                    noteTime = value;
                    OnPropertyChanged();
                }
            }
        }

        public TimeSpan NotifyTime {
            get => notifyTime;
            set {
                if (notifyTime != value) {
                    notifyTime = value;
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

        public bool IsNotifyOn {
            get => isNotifyOn;
            set {
                if (isNotifyOn != value) {
                    isNotifyOn = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool IsRepeatOn {
            get => isRepeatOn;
            set {
                if (isRepeatOn != value) {
                    isRepeatOn = value;
                    OnPropertyChanged();
                }
            }
        }

        public int RepeatIndex {
            get => repeatIndex;
            set {
                if (repeatIndex != value) {
                    repeatIndex = value;
                    OnPropertyChanged();
                }
            }
        }

        private void AddRecord() {
            if (!string.IsNullOrEmpty(Note)) {
                newRecord.NoteDate = DayViewModel.Date.Date + NoteTime;
                newRecord.Note = Note;

                if (IsNotifyOn) {
                    newRecord.NotifyDate = DayViewModel.Date.Date + NotifyTime;
                }

                if (IsRepeatOn) {
                    newRecord.RepeatCode = repeatIndex;
                }

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
