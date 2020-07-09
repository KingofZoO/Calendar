using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calendar.Interfaces;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Calendar.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MonthView : ContentPage {
        INotificationManager notificationManager;

        private string[] DaysName = new[] {
            "пн",
            "вт",
            "ср",
            "чт",
            "пт",
            "сб",
            "вс"
        };

        public MonthView() {
            InitializeComponent();
            
            PrepareCalendar();
            notificationManager = DependencyService.Get<INotificationManager>();
        }

        private void PrepareCalendar() {
            CalendarGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
            CalendarGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            CalendarGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            CalendarGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            CalendarGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            CalendarGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            CalendarGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            CalendarGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            CalendarGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            CalendarGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            CalendarGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            CalendarGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            CalendarGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            CalendarGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

            for (int row = 0; row < 7; row++) {
                for (int col = 0; col < 7; col++) { 
                    if(row == 0) {
                        Label weekDay = new Label() { 
                            Text = DaysName[col],
                            HorizontalOptions = LayoutOptions.CenterAndExpand
                        };
                        CalendarGrid.Children.Add(weekDay, col, row);
                    }
                    else {
                        Button dayButton = new Button();
                        int dayIndex = 7 * (row - 1) + col;
                        dayButton.SetBinding(Button.TextProperty, $"CalendarDays[{dayIndex}].Date.Day");
                        dayButton.BorderColor = Color.Black;
                        dayButton.SetBinding(Button.BorderWidthProperty, $"CalendarDays[{dayIndex}].IsNoted");
                        dayButton.SetBinding(Button.BackgroundColorProperty, $"CalendarDays[{dayIndex}].Color");
                        dayButton.SetBinding(Button.CommandProperty, "DayViewCommand");
                        dayButton.CommandParameter = dayIndex;
                        CalendarGrid.Children.Add(dayButton, col, row);
                    }
                }
            }
        }

        private void Button_Clicked(object sender, EventArgs e) {
            notificationManager.ScheduleNotification("Title", "Message", DateTime.Now.AddSeconds(5));
        }
    }
}