using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace BlindTyper.FileHandling {

    /// <summary>
    /// This handles the reading (and writing) of files to import into the program.
    /// </summary>
    public static class FileImporter {

        private static List<string> lines = new List<string>();
        private static bool importedSuccesful = false;

        /// <summary>
        /// This attempts to import a file using an open file dialog.
        /// If this succeeds, the GetLines() method is callable.
        /// If this fails, the GetLines() method returns null.
        /// </summary>
        /// <returns>Returns true if succeeded.</returns>
        public static bool OnClickImportFile() {
            importedSuccesful = false;

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = false;
            openFileDialog.Filter = "Text Document (.txt)|*.txt";

            if (openFileDialog.ShowDialog() == true) {
                try {
                    lines = File.ReadAllLines(openFileDialog.FileName).ToList();
                    importedSuccesful = true;
                    return true;
                } catch (Exception e) {
                    Console.WriteLine("Importing file failed. Error message: " + e.Message);
                }
            }

            return false;
        }

        /// <summary>
        /// Gets the imported lines if any are present
        /// </summary>
        /// <returns>Returns the lines if present, otherwise this returns null</returns>
        public static List<string> GetText() {
            if (importedSuccesful && lines != null && lines.Count > 0) {
                return lines;
            } else {
                return null;
            }
        }

    }

}
