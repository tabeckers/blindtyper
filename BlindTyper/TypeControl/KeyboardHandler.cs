using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Forms;

namespace BlindTyper.TypeControl {

    public class KeyboardHandler {

        private KeysConverter keysConverter;
        private Typer typer;
        private bool inFinishedTypingState;

        /// <summary>
        /// Basic constructor
        /// Sets this object as a keyboard input listener
        /// When a key is typed this handles the event
        /// </summary>
        public KeyboardHandler() {
            MainWindow.GlobalMainWindow.KeyDown += OnKeyPressed;
            keysConverter = new KeysConverter();
        }

        /// <summary>
        /// Connect the keyboard handler to a typer object
        /// </summary>
        /// <param name="typer">The typer object to connect to</param>
        public void ConnectToTyper(Typer typer) {
            if (typer == null) {
                return;
            }

            this.typer = typer;
            inFinishedTypingState = false;
        }

        /// <summary>
        /// Disconnect from the typer by setting it to null
        /// </summary>
        public void DisconnectFromTyper() {
            typer = null;
        }

        /// <summary>
        /// Lets the object know we are in the finished typing state
        /// </summary>
        public void SetInFinishedTypingState() {
            inFinishedTypingState = true;
        }

        /// <summary>
        /// This is triggered when a key is pressed while we are in the main window.
        /// </summary>
        private void OnKeyPressed(object sender, System.Windows.Input.KeyEventArgs e) {
            // Return if no typer is connected
            if (typer == null) {
                return;
            }

            // If we are in finished typing state this servers as a "press any key to continue"
            if (inFinishedTypingState) {
                MainWindow.GlobalMainWindow.ChangeProgramState(ProgramState.awaitingImport);
                return;
            }

            // Determine which character was pressed
            Nullable<char> c = null;
            switch (e.Key) {
                // Don't continue if it's a shift key
                case Key.LeftShift:
                    return;
                case Key.RightShift:
                    return;

                // Continue if it's any other key
                case Key.Space:
                    c = ' ';
                    break;

                case Key.D1:
                    if (ShiftIsDown()) {
                        c = '!';
                    }
                    break;

                case Key.OemQuestion:
                    if (ShiftIsDown()) {
                        c = '?';
                    } else {
                        c = '/';
                    }
                    break;

                case Key.OemComma:
                    c = ',';
                    break;

                case Key.OemPeriod:
                    c = '.';
                    break;

                default:
                    string keyChar = keysConverter.ConvertToString(e.Key);
                    char x = keyChar.ToCharArray()[0];
                    if (char.IsLetterOrDigit(x)) {
                        if (ShiftIsDown()) {
                            c = char.ToUpper(x);
                        } else {
                            c = char.ToLower(x);
                        }
                    }
                    break;
            }

            // Send out our character to the typer
            if (c != null) {
                typer.OnPressedChar(c ?? default(char));
            }
        }

        /// <summary>
        /// Checks if any of the shift keys are pressed
        /// </summary>
        /// <returns>True if a shift is being pressed</returns>
        private static bool ShiftIsDown() {
            return Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift);
        }

    }

}
