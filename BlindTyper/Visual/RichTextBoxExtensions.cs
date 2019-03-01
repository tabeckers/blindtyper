using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace BlindTyper.Visual {

    public static class RichTextBoxExtensions {

        /// <summary>
        /// Adds some text of a specified color to a rich text box. 
        /// The background is transparant by default.
        /// </summary>
        /// <param name="text">The text to add</param>
        /// <param name="color">The color specified</param>
        public static void AppendText(this RichTextBox box, string text, Color color) {
            AppendText(box, text, color, Color.FromArgb(0, 0, 0, 0));
        }

        /// <summary>
        /// Adds some text of a specified color to a rich text box
        /// </summary>
        /// <param name="text">The text to add</param>
        /// <param name="color">The color specified</param>
        /// <param name="colorBackground">The color for the background, used to indicate spaces</param>
        public static void AppendText(this RichTextBox box, string text, Color color, Color colorBackground) {
            TextRange range = new TextRange(box.Document.ContentEnd, box.Document.ContentEnd) {
                Text = text
            };
            range.ApplyPropertyValue(TextElement.ForegroundProperty, new SolidColorBrush(color));
            range.ApplyPropertyValue(TextElement.BackgroundProperty, new SolidColorBrush(colorBackground));
            box.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
        }

        /// <summary>
        /// This clears the richtextbox and adds the given text to it
        /// </summary>
        /// <param name="text">The text to put in the box</param>
        public static void OverwriteText(this RichTextBox box, string text) {
            box.Clear();
            box.AppendText(text);
        }

        /// <summary>
        /// Clears the richtextbox
        /// </summary>
        /// <param name="box"></param>
        public static void Clear(this RichTextBox box) {
            box.Document.Blocks.Clear();
        }
    }

}
