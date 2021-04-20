using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Card = System.Int32;

namespace Pexeso
{
    public partial class GameForm : Form
    {
        private readonly StartScreen startScreen;
        private readonly Card[] playingCards = { 1, 2, 3, 4, 5, 6, 7, 8 }; // set content of cards here
        private readonly Card[] cards;
        private Button firstCard, secondCard;
        private readonly Random rnd = new Random();
        private State state = State.noCardsOpened;
        public GameForm(StartScreen startScreen)
        {
            this.startScreen = startScreen;

            // create pairs of cards
            cards = new Card[playingCards.Length * 2];
            for (int i = 0; i < cards.Length; i++)
            {
                cards[i] = playingCards[i / 2];
            }

            randomOrder(ref cards);

            InitializeComponent();
        }

        private void GameForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            startScreen.Show();
        }

        private void cardClick(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int index = btn.TabIndex;
            switch (state)
            {
                case State.oneCardOpened:
                    if (btn != firstCard)
                    {
                        secondCard = btn;
                        btn.Text = cards[index].ToString();
                        state = State.twoCardsOpened;
                    }
                    break;
                case State.twoCardsOpened:
                    if (firstCard.Text == secondCard.Text)
                    {
                        firstCard.Hide();
                        secondCard.Hide();
                    }
                    else
                    {
                        firstCard.Text = "";
                        secondCard.Text = "";
                    }
                    state = State.noCardsOpened;
                    break;
                case State.noCardsOpened:
                    firstCard = btn;
                    btn.Text = cards[index].ToString();
                    state = State.oneCardOpened;
                    break;
            }
        }

        private void randomOrder(ref Card[] arr)
        {
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