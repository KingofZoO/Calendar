using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Calendar.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class YearView : ContentPage {
        public YearView() {
            InitializeComponent();
            
            PrepareMonths();
        }

        private void PrepareMonths() {
            MonthsGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            MonthsGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            MonthsGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            MonthsGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            MonthsGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            MonthsGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            MonthsGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

            for (int row = 0; row < 4; row++) {
                for (int col = 0; col < 3; col++) {
                    Button monthButton = new Button();
                    int monthIndex = 3 * row + col;

                    monthButton.SetBinding(Button.StyleProperty, $"Months[{monthIndex}].Style");
                    monthButton.SetBinding(Button.TextProperty, $"Months[{monthIndex}].Month");
                    monthButton.SetBinding(Button.CommandProperty, "MonthViewCommand");

                    monthButton.CommandParameter = monthIndex;
                    MonthsGrid.Children.Add(monthButton, col, row);
                }
            }
        }
    }
}