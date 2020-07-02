using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Calendar.Views;
using Calendar.Models;
using Calendar.ViewModels;
using System.IO;

namespace Calendar {
    public partial class App : Application {
        private const string DATABASE_NAME = "calendar.db";
        private static RecordsRepository database;

        public static RecordsRepository DataBase {
            get {
                if (database == null) {
                    database = new RecordsRepository(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), DATABASE_NAME));
                }
                return database;
            }
        }

        public App() {
            InitializeComponent();

            MainPage = new NavigationPage(new MonthView());
            MainPage.BindingContext = new MonthViewModel(DateTime.Today) {
                Navigation = MainPage.Navigation
            };
        }

        protected override void OnStart() {
        }

        protected override void OnSleep() {
        }

        protected override void OnResume() {
        }
    }
}
