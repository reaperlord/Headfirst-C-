using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace A_day_at_the_Races
{
    class Guy
    {
        public string Name;
        public Bet MyBet;
        public int Cash;

        public RadioButton MyRadioButton;
        public Label MyLabel;

        


        public void UpdateLabels()
        {
            MyLabel.Text = MyBet.GetDescription();

            MyRadioButton.Text = Name + " has " + Cash + " bucks"; 
        }

        public bool PlaceBet(int BetAmount, int DogToWin)
        {
            if (BetAmount>Cash)
            {
                MessageBox.Show("You have too little cash to place this bet.");
                return false;
            }
            else if ((BetAmount < Form1.minimumBetVal)&&BetAmount!=0)
            {
                MessageBox.Show("You must place a bet of atleast " + Form1.minimumBetVal + " bucks.");
                return false;
            }
            else
            {
                MyBet = new Bet()
                {
                    Amount = BetAmount,
                    Dog = DogToWin,
                    Bettor = this
                };
                UpdateLabels();
                return true;
            }
        }

        public void ClearBet()
        {
            PlaceBet(0, 1);
        }

        

        public int Collect(int Winner)
        {
            Cash += MyBet.PayOut(Winner);
            return MyBet.PayOut(Winner);
        }
    }



    class Bet
    {
        public int Amount, Dog;
        public Guy Bettor;

        public string GetDescription()
        {
            string description;

            if (Amount > 0)
                description = Bettor.Name + " bets " + Amount + " bucks on dog #" + Dog + '.';
            else
                description = Bettor.Name + " hasn't placed a bet yet.";

            return description;
        }

        public int PayOut(int Winner)
        {
            if (Winner == Dog)
                return 2 * Amount;
            else return -Amount;
        }
    }
}
