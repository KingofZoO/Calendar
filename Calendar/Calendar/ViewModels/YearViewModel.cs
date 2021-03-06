﻿using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Calendar.ViewModels;
using System.Windows.Input;
using Xamarin.Forms;
using Calendar.Views;

namespace Calendar.ViewModels {
    public class YearViewModel : INotifyPropertyChanged {
        private int year;

        public MonthViewModel MonthViewModel { get; private set; }

        public MonthCellViewModel[] Months { get; private set; } = new MonthCellViewModel[12];

        public ICommand NextYearCommand { get; private set; }
        public ICommand PrevYearCommand { get; private set; }
        public ICommand MonthViewCommand { get; private set; }

        public ICommand BackToCurrentYearCommand { get; private set; }

        public YearViewModel(MonthViewModel vm) {
            MonthViewModel = vm;
            Year = vm.Year;

            NextYearCommand = new Command(NextYear);
            PrevYearCommand = new Command(PrevYear);
            MonthViewCommand = new Command<int>(ShowMonthView);

            BackToCurrentYearCommand = new Command(ToCurrentYear);

            PrepareYearData();
            FillYearData();
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

        private void PrepareYearData() {
            for (int i = 0; i < Months.Length; i++) {
                Months[i] = new MonthCellViewModel() { Month = MonthViewModel.MonthsName[i] };
            }
        }

        private void FillYearData() {
            for (int i = 0; i < Months.Length; i++) {
                if (DateTime.Now.Year == Year && DateTime.Now.Month == i + 1)
                    Months[i].Style = (Style)Application.Current.Resources["TodaysButton"];
                else
                    Months[i].Style = (Style)Application.Current.Resources["DefaultButton"];
            }
        }

        private void NextYear() {
            Year++;
            FillYearData();
        }

        private void PrevYear() {
            Year--;
            FillYearData();
        }

        private void ShowMonthView(int monthIndex) {
            MonthViewModel.BackToMonthView(new DateTime(Year, monthIndex + 1, 1));
        }

        private void ToCurrentYear() {
            Year = DateTime.Now.Year;
            FillYearData();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
