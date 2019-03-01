using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace BlindTyper {
    public static class Utility {

        /// <summary>
        /// Attempts to get a color from the resources of our app.
        /// TODO Add an exception in here.
        /// </summary>
        /// <param name="key">The key (name) of our color as defined in the resources within App.xaml</param>
        /// <returns>The color. A (122, 122, 122) gray color is returned if the key is not found.</returns>
        public static Color GetColorFromKey(string key) {
            Color result = Color.FromRgb(122, 122, 122);

            try {
                var fromResource = App.Current.Resources[key];
                if (fromResource != null) {
                    result = (Color)ColorConverter.ConvertFromString(fromResource.ToString());
                    return result;
                }
            } catch (Exception e) {
                Console.WriteLine(e.Message);
            }

            return result;
        }

    }
}
