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
            MonthsGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            MonthsGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            MonthsGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            MonthsGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

            for (int row = 0; row < 3; row++) {
                for (int col = 0; col < 4; col++) {
                    Button monthButton = new Button();
                    int monthIndex = 4 * row + col;
                    monthButton.FontSize = 12;
                    monthButton.SetBinding(Button.TextProperty, $"Months[{monthIndex}].Month");
                    monthButton.SetBinding(Button.BackgroundColorProperty, $"Months[{monthIndex}].Color");
                    monthButton.SetBinding(Button.CommandProperty, "MonthViewCommand");
                    monthButton.CommandParameter = monthIndex;
                    MonthsGrid.Children.Add(monthButton, col, row);
                }
            }
        }
    }
}