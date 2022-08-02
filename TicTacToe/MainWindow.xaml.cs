using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TicTacToe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        #region Private members

        /// <summary>
        /// Holds the current results of cells in the active game
        /// </summary>
        private MarkType[] mResults;

        /// <summary>
        /// True if player 1's turn (X), or player 2's turn (O)
        /// </summary>
        private bool mPlayer1Turn;

        /// <summary>
        /// True if game has concluded
        /// </summary>
        private bool mGameEnded;
        #endregion
        #region contructor 

        /// <summary>
        /// Default contructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            newGame();

        }

        #endregion

        /// <summary>
        /// Starts a new game and clears all the values back to the start
        /// </summary>
        private void newGame()
        {
            ///Create a new  blank array of free cells
            mResults = new MarkType[9];

            //Not needed since Free is the first option stated in enum but showing in case if it wasn't, declared just to let it be known
            for (var i = 0; i < mResults.Length; i++)
                mResults[i] = MarkType.Free;

            //Make sure player1 starts the game
            mPlayer1Turn = true;

            //Iterate through all the buttons on the grid
            Container.Children.Cast<Button>().ToList().ForEach(button =>
            {
                button.Content = string.Empty;
                button.Background = Brushes.White;
                button.Foreground = Brushes.Blue;
            });

            mGameEnded = false;
        }

        /// <summary>
        /// Handles a button click
        /// </summary>
        /// <param name="sender">The button that was clicked</param>
        /// <param name="e">The events of the click</param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (mGameEnded)
            {
                newGame();
                return;
            }

            //Case sender to button
            var button = (Button)sender;

            //Find the button's position in the array
            var column = Grid.GetColumn(button);
            var row = Grid.GetRow(button);

            var index = column + (row * 3);

            //Don't do anything if the cell already has a value in it
            if (mResults[index] != MarkType.Free)
                return;

            //Set the cell value based on which players turn it is
            mResults[index] = mPlayer1Turn ? MarkType.Cross : MarkType.Nought;
            //if condition ? true execution : false execution

            //Set button text to the result
            button.Content = mPlayer1Turn ? "X" : "O";

            if (!mPlayer1Turn)
                button.Foreground = Brushes.Red;

            //Toggle the player's turns
            mPlayer1Turn ^= true;

            //Check for winner
            CheckForWinner();
        }

        /// <summary>
        /// Checks if there is a winner
        /// </summary>
        private void CheckForWinner()
        {
            #region Horizontal wins
            //Checks for horizontal wins
            //Row 0
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[1] & mResults[2]) == mResults[0])
            {
                //Game ended
                mGameEnded = true;

                //Highlight winning cells
                Button0_0.Background = Button1_0.Background = Button2_0.Background = Brushes.Green;
            }

            //Row 1
            if (mResults[3] != MarkType.Free && (mResults[3] & mResults[4] & mResults[5]) == mResults[3])
            {
                //Game ended
                mGameEnded = true;

                //Highlight winning cells
                Button0_1.Background = Button1_1.Background = Button2_1.Background = Brushes.Green;
            }

            //Row 2
            if (mResults[6] != MarkType.Free && (mResults[6] & mResults[7] & mResults[8]) == mResults[6])
            {
                //Game ended
                mGameEnded = true;

                //Highlight winning cells
                Button0_2.Background = Button1_2.Background = Button2_2.Background = Brushes.Green;
            }

            #endregion

            #region Vertical wins
            //Checks for vertical wins
            //Column 0
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[3] & mResults[6]) == mResults[0])
            {
                //Game ended
                mGameEnded = true;

                //Highlight winning cells
                Button0_0.Background = Button0_1.Background = Button0_2.Background = Brushes.Green;
            }

            //Column 1
            if (mResults[1] != MarkType.Free && (mResults[1] & mResults[4] & mResults[7]) == mResults[1])
            {
                //Game ended
                mGameEnded = true;

                //Highlight winning cells
                Button1_0.Background = Button1_1.Background = Button1_2.Background = Brushes.Green;
            }

            //Column 2
            if (mResults[2] != MarkType.Free && (mResults[2] & mResults[5] & mResults[8]) == mResults[2])
            {
                //Game ended
                mGameEnded = true;

                //Highlight winning cells
                Button2_0.Background = Button2_1.Background = Button2_2.Background = Brushes.Green;
            }
            #endregion

            #region Diagonal winners
            //Check for Diagnoal wins
            //Top Left -> Bottom Right

            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[4] & mResults[8]) == mResults[0])
            {
                //Game ended
                mGameEnded = true;

                //Highlight winning cells
                Button0_0.Background = Button1_1.Background = Button2_2.Background = Brushes.Green;
            }

            //Bottom Left -> Top Right

            if (mResults[2] != MarkType.Free && (mResults[2] & mResults[4] & mResults[6]) == mResults[2])
            {
                //Game ended
                mGameEnded = true;

                //Highlight winning cells
                Button0_2.Background = Button1_1.Background = Button2_0.Background = Brushes.Green;
            }

            #endregion

            #region no winners
            //Check for no winners and full board
            if (!mResults.Any(result => result == MarkType.Free))
            {

                //Game ended
                mGameEnded = true;

                //Turn all cells orange
                Container.Children.Cast<Button>().ToList().ForEach(button =>
                {
                    button.Background = Brushes.Orange;
                });


            }
            #endregion
        }
    }

}
