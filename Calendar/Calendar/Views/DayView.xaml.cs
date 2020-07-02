using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Calendar.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DayView : ContentPage {
        public DayView() {
            InitializeComponent();
        }
    }
}