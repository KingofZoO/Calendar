using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Calendar.Converters;

namespace Calendar.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MonthView : ContentPage {
        private string[] DaysName = new[] {
            "пн",
            "вт",
            "ср",
            "чт",
            "пт",
            "сб",
            "вс"
        };

        Animation menuFrameOn;
        Animation menuFrameOff;

        public MonthView() {
            InitializeComponent();

            menuFrameOn = new Animation(w => {
                MenuFrame.IsVisible = true;
                MenuFrame.WidthRequest = w;
            }, 0, 150);
            menuFrameOff = new Animation(w => {
                SettingsButton.IsVisible = false;
                MenuFrame.WidthRequest = w;
            }, 150, 0);

            PrepareCalendar();
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

            NotedToBorderConverter converter = new NotedToBorderConverter();
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

                        dayButton.SetBinding(Button.StyleProperty, $"CalendarDays[{dayIndex}].Style");
                        dayButton.SetBinding(Button.TextProperty, $"CalendarDays[{dayIndex}].Date.Day");
                        dayButton.SetBinding(Button.BorderWidthProperty, $"CalendarDays[{dayIndex}].IsNoted", BindingMode.OneWay, converter);
                        dayButton.SetBinding(Button.CommandProperty, "DayViewCommand");

                        dayButton.CommandParameter = dayIndex;
                        CalendarGrid.Children.Add(dayButton, col, row);
                    }
                }
            }
        }

        private void ImageButton_Clicked(object sender, EventArgs e) {
            if (MenuFrame.IsVisible)
                menuFrameOff.Commit(MenuFrame, "animation_off", 16, 250, null, (v, c) => MenuFrame.IsVisible = false);
            else
                menuFrameOn.Commit(MenuFrame, "animation_on", 16, 250, null, (v, c) => SettingsButton.IsVisible = true);
        }

        protected override void OnAppearing() {
            base.OnAppearing();

            SettingsButton.IsVisible = false;
            MenuFrame.IsVisible = false;
        }

        protected override void OnDisappearing() {
            base.OnDisappearing();
            MenuFrame.WidthRequest = 0;
        }
    }
}