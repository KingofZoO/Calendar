using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Calendar.Models;
using Xamarin.Forms;

namespace Calendar.ViewModels {
    public class SettingsViewModel : INotifyPropertyChanged {
        private bool isColorsOn;

        public MonthViewModel MonthViewModel { get; private set; }

        public ICommand MonthViewCommand { get; private set; }

        public SettingsViewModel(MonthViewModel vm) {
            LoadAppSettings();
            MonthViewModel = vm;

            MonthViewCommand = new Command(ShowMonthView);
        }

        private void LoadAppSettings() {
            isColorsOn = CalendarSettings.IsColorsOn;
        }

        public bool IsColorsOn {
            get => isColorsOn;
            set {
                if (isColorsOn != value) {
                    isColorsOn = value;
                    OnPropertyChanged();

                    CalendarSettings.IsColorsOn = isColorsOn;
                    CalendarSettings.SetAppTheme(DateTime.Now.Month - 1);
                }
            }
        }

        private void ShowMonthView() {
            MonthViewModel.BackToMonthView(DateTime.Now);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
