using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Windows.Input;

namespace Calendar.Views.Components {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainNavigationComponent : ContentView {
        public MainNavigationComponent() {
            InitializeComponent();
        }

        public static readonly BindableProperty LeftCommandProperty = BindableProperty.Create(
            "LeftCommand",
            typeof(ICommand),
            typeof(MainNavigationComponent),
            null);

        public static readonly BindableProperty RightCommandProperty = BindableProperty.Create(
            "RightCommand",
            typeof(ICommand),
            typeof(MainNavigationComponent),
            null);

        public static readonly BindableProperty HouseCommandProperty = BindableProperty.Create(
            "HouseCommand",
            typeof(ICommand),
            typeof(MainNavigationComponent),
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
    }
}