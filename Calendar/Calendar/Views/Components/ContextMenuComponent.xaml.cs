using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Windows.Input;

namespace Calendar.Views.Components {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContextMenuComponent : ContentView {
        public ContextMenuComponent() {
            InitializeComponent();
        }

        public static readonly BindableProperty BackCommandProperty = BindableProperty.Create(
            "BackCommand",
            typeof(ICommand),
            typeof(ContextMenuComponent),
            null);

        public static readonly BindableProperty ChangeCommandProperty = BindableProperty.Create(
            "ChangeCommand",
            typeof(ICommand),
            typeof(ContextMenuComponent),
            null);

        public static readonly BindableProperty DeleteCommandProperty = BindableProperty.Create(
            "DeleteCommand",
            typeof(ICommand),
            typeof(ContextMenuComponent),
            null);

        public static readonly BindableProperty DeleteAllCommandProperty = BindableProperty.Create(
            "DeleteAllCommand",
            typeof(ICommand),
            typeof(ContextMenuComponent),
            null);

        public static readonly BindableProperty ChangeCommandParameterProperty = BindableProperty.Create(
            "ChangeCommandParameter",
            typeof(object),
            typeof(ContextMenuComponent),
            null);

        public static readonly BindableProperty DeleteCommandParameterProperty = BindableProperty.Create(
            "DeleteCommandParameter",
            typeof(object),
            typeof(ContextMenuComponent),
            null);

        public ICommand BackCommand {
            get => (ICommand)GetValue(BackCommandProperty);
            set => SetValue(BackCommandProperty, value);
        }

        public ICommand ChangeCommand {
            get => (ICommand)GetValue(ChangeCommandProperty);
            set => SetValue(ChangeCommandProperty, value);
        }

        public ICommand DeleteCommand {
            get => (ICommand)GetValue(DeleteCommandProperty);
            set => SetValue(DeleteCommandProperty, value);
        }

        public ICommand DeleteAllCommand {
            get => (ICommand)GetValue(DeleteAllCommandProperty);
            set => SetValue(DeleteAllCommandProperty, value);
        }

        public object ChangeCommandParameter {
            get => GetValue(ChangeCommandParameterProperty);
            set => SetValue(ChangeCommandParameterProperty, value);
        }

        public object DeleteCommandParameter {
            get => GetValue(DeleteCommandParameterProperty);
            set => SetValue(DeleteCommandParameterProperty, value);
        }
    }
}