using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Calendar.Themes;

namespace Calendar.Models {
    public static class CalendarSettings {
        private static string isColorsOnPropName = "IsColorsOn";
        private static bool isColorsOn;

        private static ICollection<ResourceDictionary> mergedDictionaries = Application.Current.Resources.MergedDictionaries;
        private static ResourceDictionary[] monthThemes = new ResourceDictionary[13] {
            new JanuaryTheme(),
            new FebruaryTheme(),
            new MarchTheme(),
            new AprilTheme(),
            new MayTheme(),
            new JuneTheme(),
            new JuleTheme(),
            new AugustTheme(),
            new SeptemberTheme(),
            new OctoberTheme(),
            new NovemberTheme(),
            new DecemberTheme(),
            new DefaultTheme()
        };

        public static bool IsColorsOn {
            get => isColorsOn;
            set {
                if (isColorsOn != value) {
                    isColorsOn = value;
                    App.Current.Properties[isColorsOnPropName] = isColorsOn;
                }
            }
        }

        static CalendarSettings() {
            LoadAppSettings();
            SetAppTheme(DateTime.Now.Month - 1);
        }

        public static void SetAppTheme(int monthNum) {
            mergedDictionaries.Clear();

            if (IsColorsOn) {
                mergedDictionaries.Add(monthThemes[monthNum]);
            }
            else
                mergedDictionaries.Add(monthThemes[12]);
        }

        private static void LoadAppSettings() {
            if (App.Current.Properties.TryGetValue(isColorsOnPropName, out object isColorsonProp))
                isColorsOn = (bool)isColorsonProp;
            else
                isColorsOn = false;
        }
    }
}

