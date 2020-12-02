using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Windows.Input;

namespace Calendar.Views.Components {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ToolBarComponent : ContentView {
        public ToolBarComponent() {
            InitializeComponent();
        }

        public static readonly BindableProperty BackCommandProperty = BindableProperty.Create(
            "BackCommand",
            typeof(ICommand),
            typeof(ContextMenuComponent),
            null);

        public static readonly BindableProperty CentralTitleProperty = BindableProperty.Create(
            "CentralTitle",
            typeof(string),
            typeof(ContextMenuComponent),
            null);

        public static readonly BindableProperty IsCentralTitleVisibleProperty = BindableProperty.Create(
            "IsCentralTitleVisible",
            typeof(bool),
            typeof(ContextMenuComponent),
            false);

        public ICommand BackCommand {
            get => (ICommand)GetValue(BackCommandProperty);
            set => SetValue(BackCommandProperty, value);
        }

        public string CentralTitle {
            get => (string)GetValue(CentralTitleProperty);
            set => SetValue(CentralTitleProperty, value);
        }

        public bool IsCentralTitleVisible {
            get => (bool)GetValue(IsCentralTitleVisibleProperty);
            set => SetValue(IsCentralTitleVisibleProperty, value);
        }
    }
}