using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using Xamarin.Forms;
using Calendar.Models;
using System.Runtime.CompilerServices;

namespace Calendar.ViewModels {
    public class DayCellViewModel : INotifyPropertyChanged {
        private DateTime date;
        private Color color;
        private bool isNoted = false;

        public DayCellViewModel() { }

        public DayCellViewModel(DateTime date) {
            Date = date;
        }

        public DateTime Date {
            get => date;
            set {
                if (date != value) {
                    date = value;
                    OnPropertyChanged();
                }
            }
        }

        public Color Color {
            get => color;
            set {
                if (color != value) {
                    color = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool IsNoted {
            get => isNoted;
            set {
                if (isNoted != value) {
                    isNoted = value;
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
