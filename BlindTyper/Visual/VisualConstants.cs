using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace BlindTyper.Visual {

    /// <summary>
    /// This is used to store constants for the visual layout troughout the program
    /// </summary>
    public static class VisualConstants {

        public static Color ColorUntouched = Color.FromRgb(0, 0, 0);
        public static Color ColorSucceed = Color.FromRgb(0, 255, 0);
        public static Color ColorFailed = Color.FromRgb(255, 0, 0);
        public static Color ColorBackgroundSucceed = Color.FromArgb(12, 0, 0, 0);
        public static Color ColorBackgroundFailed = Color.FromArgb(0, 255, 0, 0);
        public static Color ColorTransparent = Color.FromArgb(0, 0, 0, 0);

    }

}
