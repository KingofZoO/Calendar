using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace Calendar.ViewModels {
    public class MonthCellViewModel : INotifyPropertyChanged {
        private Style style;
        private string month;

        public Style Style {
            get => style;
            set {
                if (style != value) {
                    style = value;
                    OnPropertyChanged();
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

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
