using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Windows.Input;

namespace Calendar.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainNavigationView : ContentView {
        public static readonly BindableProperty LeftCommandProperty = BindableProperty.Create(
            "LeftCommand",
            typeof(ICommand),
            typeof(MainNavigationView),
            null);

        public static readonly BindableProperty RightCommandProperty = BindableProperty.Create(
            "RightCommand",
            typeof(ICommand),
            typeof(MainNavigationView),
            null);

        public static readonly BindableProperty HouseCommandProperty = BindableProperty.Create(
            "HouseCommand",
            typeof(ICommand),
            typeof(MainNavigationView),
            null);

        public ICommand LeftCommand {
            get => (ICommand)GetValue(LeftCommandProperty);
            set => SetValue(LeftCommandProperty, value);
        }

        public ICommand RightCommand {
            get => (ICommand)GetValue(RightCommandProperty);
            set => SetValue(RightCommandProperty, value);
        }

        public ICommand HouseCommand {
            get => (ICommand)GetValue(HouseCommandProperty);
            set => SetValue(HouseCommandProperty, value);
        }

        public MainNavigationView() {
            InitializeComponent();
        }
    }
}