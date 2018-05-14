using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MatchingGame
{
    public partial class Form1 : Form
    {
        Random random = new Random();

        List<string> icons = new List<string>()
        {
            "!", "!", "N", "N", ",", ",", "k", "k",
            "b", "b", "v", "v", "w", "w", "z", "z"
        };

        Label firstClicked = null;

        Label secondClicked = null;

        public Form1()
        {
            InitializeComponent();

            AssignIconsToSquares();
        }

        private void AssignIconsToSquares()
        {
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                Label iconLabel = control as Label;
                if (iconLabel != null)
                {
                    int randomNumber = random.Next(icons.Count);
                    iconLabel.Text = icons[randomNumber];
                    iconLabel.ForeColor = iconLabel.BackColor;
                    icons.RemoveAt(randomNumber);
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();

            if (firstClicked.Text == secondClicked.Text)
            {
                CheckForWinner();
            } else
            {
                firstClicked.ForeColor = firstClicked.BackColor;
                secondClicked.ForeColor = secondClicked.BackColor;
            }

            firstClicked = null;
            secondClicked = null;
        }

        /// <summary>
        /// Os clicks de todas as labels são tratados por este evento.
        /// </summary>
        /// <param name="sender">A label que foi clicada.</param>
        /// <param name="e"></param>

        private void label_Click(object sender, EventArgs e)
        {
            Label clicledLabel = sender as Label;

            if (clicledLabel != null)
            {
                if (clicledLabel.ForeColor == Color.Black)
                    return;

                if (firstClicked == null)
                {
                    firstClicked = clicledLabel;
                    firstClicked.ForeColor = Color.Black;
                }
                else if (secondClicked == null)
                {
                    secondClicked = clicledLabel;
                    secondClicked.ForeColor = Color.Black;
                    timer1.Start();
                }
            }
        }

        private void CheckForWinner()
        {
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                Label iconLabel = control as Label;

                if (iconLabel != null)
                {
                    if (iconLabel.ForeColor == iconLabel.BackColor)
                    {
                        return;
                    }
                }
            }

            MessageBox.Show("Você tem uma memória de elefante!", "Parabéns");
            Close();
        }
    }
}
