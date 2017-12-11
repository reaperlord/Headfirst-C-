using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace A_day_at_the_Races
{
    public partial class Form1 : Form
    {

        public Random MyRandomizer;

        Greyhound[] GreyhoundArray = new Greyhound[4];

        Guy[] GuyArray = new Guy[3];

        RadioButton[] radButtons;
                
        static public int minimumBetVal = 5;

        public int counter;

        int currentGuy;

        public Form1()
        {
            MyRandomizer = new Random();

            InitializeComponent();
            
            radButtons = new RadioButton[] { radioButton1, radioButton2, radioButton3 };

            counter = 0;
            int minBet = minimumBetVal;
            minimumBetLabel.Text = "Minimum bet: " + 5+   " bucks";

            

            GuyArray[0] = new Guy()
            {
                Name = "Joe", Cash = 50, MyLabel = joeBetLabel,
                MyRadioButton = radioButton1, 
                //MyBet = new Bet()// { Amount= 0, Dog =1, Bettor= GuyArray[0] }
            };

            GuyArray[1] = new Guy()
            {
                Name = "Bob",
                Cash = 75,
                MyLabel = bobBetLabel,
                MyRadioButton = radioButton2,
                //MyBet = new Bet()// { Amount = 0, Dog =1 , Bettor = GuyArray[1] }
            };

            GuyArray[2] = new Guy()
            {
                Name = "Al",
                Cash = 45,
                MyLabel = alBetLabel,
                MyRadioButton = radioButton3,
                //MyBet = new Bet() //{ Amount = 0, Dog = 1, Bettor = GuyArray[2] }
            };

            PictureBox[] picBoxArray = { pictureBox1, pictureBox2, pictureBox3, pictureBox4 };

            for (int i = 0; i < 4; ++i)
            {
                GreyhoundArray[i] = new Greyhound()
                {
                    MyPictureBox = picBoxArray[i],
                    StartingPosition = picBoxArray[i].Left,
                    RacetrackLength = finishLine.Left,
                    Randomizer = MyRandomizer
                };
            }

            
            for (int i=0; i<3; ++i)
            {
                GuyArray[i].ClearBet();
                GuyArray[i].UpdateLabels();
            }
            

            radButtons[0].Checked = true;

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            ++counter;
            if (GreyhoundArray[counter % 4].Run())
            {
                int winner = counter % 4 + 1;
                timer1.Stop();
                MessageBox.Show("Congratulations Dog #" + winner + "!!!", "And the winner is...");

                string[] GuyString = new string[3];

                for (int i=0; i < 3; ++i)
                {                    
                    int amountCollected= GuyArray[i].Collect(winner);

                    if (amountCollected<0)
                        GuyString[i] = GuyArray[i].Name+ " loses " + -amountCollected +" bucks.  :-("; 
                    else if (amountCollected>0)
                        GuyString[i] = GuyArray[i].Name + " wins " + amountCollected + " bucks!  :-)";
                    else
                        GuyString[i] = GuyArray[i].Name + " didn't bet anything...  -_-";

                    GuyArray[i].ClearBet();                    
                }

                MessageBox.Show(GuyString[0] + "\n\n" + GuyString[1] + "\n\n" + GuyString[2], "The Results are in...");

                for (int i=0; i<4; ++i)
                    GreyhoundArray[i].TakeStartingPosition();

                betButton.Enabled = true;
            }
        }

        private void startRaceButton_Click(object sender, EventArgs e)
        {
            betButton.Enabled = false;
            timer1.Start();
        }

        private void betButton_Click(object sender, EventArgs e)
        {
            GuyArray[currentGuy].PlaceBet((int)amountEntry.Value, (int)dogEntry.Value);
        }

        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            

            for (currentGuy=0; currentGuy < 3;++currentGuy)
            {
                if (radButtons[currentGuy].Checked)
                    break;
            }

            nameLabel.Text = GuyArray[currentGuy].Name;
            amountEntry.Value = (decimal)GuyArray[currentGuy].MyBet.Amount;
            dogEntry.Value = (decimal)GuyArray[currentGuy].MyBet.Dog;
        }
    }
}
