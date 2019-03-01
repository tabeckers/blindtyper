using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using BlindTyper;
using BlindTyper.FileHandling;
using BlindTyper.TypeControl;
using BlindTyper.Visual;

namespace BlindTyper {

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {

        public static MainWindow GlobalMainWindow;

        private Typer typer;
        private KeyboardHandler keyboardHandler;

        /// <summary>
        /// The main entry point of the application
        /// </summary>
        public MainWindow() {
            InitializeComponent();
            Loaded += OnLoaded;
        }

        /// <summary>
        /// This gets called when the WPF window is loaded.
        /// </summary>
        private void OnLoaded(object sender, RoutedEventArgs e) {
            GlobalMainWindow = this;
            textBoxTyping.Clear();
            textBoxTyping.VerticalContentAlignment = VerticalAlignment.Center;

            keyboardHandler = new KeyboardHandler();

            ChangeProgramState(ProgramState.awaitingImport);
        }

        /// <summary>
        /// Gets called when the import file button is clicked.
        /// This opens a file dialog to import a file.
        /// If the import is succesful the program advances to the typing state.
        /// </summary>
        private void ButtonImportFile_Click(object sender, RoutedEventArgs e) {
            if (FileImporter.OnClickImportFile()) {
                typer = new Typer();
                keyboardHandler.ConnectToTyper(typer);
                typer.LoadText(FileImporter.GetText());
                typer.InitializeTypingState();

                ChangeProgramState(ProgramState.typing);
            }
        }

        /// <summary>
        /// This gets called when we finish our text
        /// TODO I believe this is obsolete now?
        /// </summary>
        public void OnFinishedText() {
            keyboardHandler.DisconnectFromTyper();
        }

        /// <summary>
        /// Switches to the desired program state
        /// </summary>
        /// <param name="nextState">The next program state to switch to</param>
        public void ChangeProgramState(ProgramState nextState) {

            switch (nextState) {
                case ProgramState.awaitingImport:
                    buttonImportFile.Visibility = Visibility.Visible;
                    textBoxTyping.Visibility = Visibility.Collapsed;
                    labelScore.Visibility = Visibility.Collapsed;
                    labelBottom.Visibility = Visibility.Collapsed;
                    break;

                case ProgramState.finishedTyping:
                    buttonImportFile.Visibility = Visibility.Collapsed;
                    textBoxTyping.Visibility = Visibility.Visible;
                    labelScore.Visibility = Visibility.Visible;
                    labelBottom.Visibility = Visibility.Visible;

                    textBoxTyping.Clear();
                    textBoxTyping.AppendText(StringConstants.MessageFinishedTyping, Utility.GetColorFromKey("colorTextCorrect"));
                    keyboardHandler.SetInFinishedTypingState();
                    break;

                case ProgramState.typing:
                    buttonImportFile.Visibility = Visibility.Collapsed;
                    textBoxTyping.Visibility = Visibility.Visible;
                    labelScore.Visibility = Visibility.Visible;
                    labelBottom.Visibility = Visibility.Collapsed;
                    break;

                default:
                    Console.WriteLine("Something went wrong with the program state.");
                    ChangeProgramState(ProgramState.awaitingImport);
                    break;
            }
        }

        /// <summary>
        /// Closes the program
        /// </summary>
        private void ButtonExit_Click(object sender, RoutedEventArgs e) {
            GC.Collect();
            System.Windows.Application.Current.Shutdown();
        }

        /// <summary>
        /// This makes the topbar drag the entire application
        /// This triggers both on the topbar and on the top label
        /// </summary>
        private void Topbar_MouseDown(object sender, MouseButtonEventArgs e) {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
    }



    /// <summary>
    /// Used to indicate the program state
    /// </summary>
    public enum ProgramState {
        none,
        awaitingImport,
        typing,
        finishedTyping
    }
}
