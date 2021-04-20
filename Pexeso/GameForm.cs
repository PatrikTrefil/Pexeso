using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Card = System.Int32; // Change type of content inside a card here (ToString method must be implemented)

namespace Pexeso
{
    public partial class GameForm : Form
    {
        private readonly StartScreen startScreen; // parent form
        private readonly Card[] playingCards = { 1, 2, 3, 4, 5, 6, 7, 8 }; // set content of cards here
        private int pairsNotRevealedCount;
        private readonly Card[] cards; // internal memory of card's content, access by tabIndex
        private Button firstCard, secondCard; // references to buttons pressed
        private State state = State.noCardsOpened;
        public GameForm(StartScreen startScreen)
        {
            this.startScreen = startScreen;
            pairsNotRevealedCount = playingCards.Length;
            prepareCards(ref cards);
            InitializeComponent();
        }

        private void GameForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // on close we want to return to the main menu
            startScreen.Show();
        }

        /// <summary>
        /// creates pairs of cards and stores them in random order inside the referenced array
        /// </summary>
        /// <param name="cards">where to store the cards</param>
        private void prepareCards(ref Card[] cards)
        {
            // create pairs of cards
            cards = new Card[playingCards.Length * 2];
            for (int i = 0; i < cards.Length; i++)
            {
                cards[i] = playingCards[i / 2];
            }
            // randomize
            randomOrder(ref cards);
        }

        private void cardClick(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            switch (state)
            {
                case State.oneCardOpened:
                    if (btn != firstCard)
                    {
                        secondCard = btn; // note the btn that was pressed
                        btn.Text = cards[btn.TabIndex].ToString(); // reveal card's content
                        state = State.twoCardsOpened;
                    }
                    break;
                case State.twoCardsOpened:
                    if (firstCard.Text == secondCard.Text) // match found
                    {
                        firstCard.Hide();
                        secondCard.Hide();
                        pairsNotRevealedCount--;
                        if (pairsNotRevealedCount == 0) // if all revealed
                        {
                            MessageBox.Show("You win!!!");
                            Close();
                            startScreen.Show();
                        }
                    }
                    else
                        firstCard.Text = secondCard.Text = "";

                    state = State.noCardsOpened;
                    break;
                case State.noCardsOpened:
                    firstCard = btn; // note the button pressed
                    btn.Text = cards[btn.TabIndex].ToString(); // reveal it's content
                    state = State.oneCardOpened;
                    break;
            }
        }

        /// <summary>
        /// Fisher–Yates shuffle
        /// </summary>
        /// <param name="arr">array to order randomly</param>
        private void randomOrder(ref Card[] arr)
        {
            Random rnd = new Random();
            for (int i = 0; i < arr.Length; i++)
            {
                int j = rnd.Next(0, i);
                Card temp = arr[j];
                arr[j] = arr[i];
                arr[i] = temp;
            }
        }
        public enum State
        {
            oneCardOpened,
            twoCardsOpened,
            noCardsOpened
        }
    }
}