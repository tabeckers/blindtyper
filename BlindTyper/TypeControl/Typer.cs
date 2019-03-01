using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using BlindTyper.Visual;

namespace BlindTyper.TypeControl {

    /// <summary>
    /// This class handles all the typing recognition
    /// </summary>
    public class Typer {

        private RichTextBox textBoxTyping;
        private Label labelScore;
        private KeyConverter keyConverter;

        private List<string> text;
        private int sentenceIndex;
        private string sentence;
        private char[] sentenceCharArray;
        private int charIndex;

        private int charsSuccesful;
        private int charsFailed;
        private int charsTotal;

        /// <summary>
        /// Constructor
        /// </summary>
        public Typer() {
            keyConverter = new KeyConverter();
            text = new List<string>();
            sentence = "";
            textBoxTyping = MainWindow.GlobalMainWindow.textBoxTyping;
            labelScore = MainWindow.GlobalMainWindow.labelScore;
        }

        /// <summary>
        /// This loads some text into the typer object
        /// </summary>
        /// <param name="text">The text to load, formatted as a list of strings.</param>
        public void LoadText(List<string> text) {
            if (text == null) {
                return;
            }

            this.text = text;
        }

        /// <summary>
        /// This prepares the typer object for the first typing line
        /// </summary>
        public void InitializeTypingState() {
            charsSuccesful = 0;
            charsFailed = 0;
            charsTotal = 0;

            if (text != null) {
                foreach (string line in text) {
                    charsTotal += line.Length;
                }
            }

            sentenceIndex = 0;
            StartNextLine();
        }

        /// <summary>
        /// This sets us up to go to the next line
        /// This does not check if there is another line
        /// </summary>
        private void StartNextLine() {
            sentence = text[sentenceIndex];
            sentenceCharArray = sentence.ToCharArray();

            textBoxTyping.Clear();
            textBoxTyping.AppendText(sentence, Utility.GetColorFromKey("colorTextIncorrect"));

            sentenceIndex++;
            charIndex = 0;

            labelScore.Content = charIndex + " / " + sentence.Length;
        }

        /// <summary>
        /// Gets called when the user presses a letter, number or commonly used symbol
        /// The KeyboardHandler class decides what gets passed to this method
        /// </summary>
        /// <param name="c">The char that was pressed</param>
        public void OnPressedChar(char c) {
            // If we are already finished
            if (charIndex >= sentence.Length) {
                return;
            }

            char currentDesired = sentenceCharArray[charIndex];
            if (c == currentDesired) {
                charIndex++;
                SetTextGreenUpToIndex(charIndex);
                labelScore.Content = charIndex + " / " + sentence.Length;
                charsSuccesful++;
            } else {
                charsFailed++;
            }

            if (charIndex == sentence.Length) {
                OnFinishedCurrentSentence();
            }
        }

        /// <summary>
        /// Gets called when we finished our current sentence
        /// If a next sentence is present we move to that next sentence
        /// If no next sentence is present we move to the OnFinishedTypingText() method
        /// </summary>
        public void OnFinishedCurrentSentence() {
            if (sentenceIndex < text.Count) {
                StartNextLine();
            } else {
                OnFinishedTypingText();
            }
        }

        /// <summary>
        /// Gets called when we finished all of our lines
        /// This also sets the score label text
        /// </summary>
        private void OnFinishedTypingText() {
            double accuracy = 1 - ((double)charsFailed / (double)(charsFailed + charsSuccesful));
            labelScore.Content = "Your accuracy is " + Math.Round(100 * accuracy) + "%!";
            MainWindow.GlobalMainWindow.ChangeProgramState(ProgramState.finishedTyping);
        }

        /// <summary>
        /// This makes our text green up to the desired index
        /// </summary>
        /// <param name="index">The index up to which we go</param>
        private void SetTextGreenUpToIndex(int index) {
            // Edge cases
            if (index >= sentence.Length) {
                return;
            }

            // Colour our text
            string green = sentence.Substring(0, index);
            string red = sentence.Substring(index, sentence.Length - (index));

            textBoxTyping.Clear();
            textBoxTyping.AppendText(green, Utility.GetColorFromKey("colorTextCorrect"), Utility.GetColorFromKey("colorTextCorrectBackground"));
            textBoxTyping.AppendText(red, Utility.GetColorFromKey("colorTextIncorrect"), Utility.GetColorFromKey("colorTextIncorrectBackground"));
        }

    }
}
