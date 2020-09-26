using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace Calendar.ViewModels {
    public class MonthCellViewModel : INotifyPropertyChanged {
        private Color color;
        private string month;

        public Color Color {
            get => color;
            set {
                if (color != value) {
                    color = value;
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
