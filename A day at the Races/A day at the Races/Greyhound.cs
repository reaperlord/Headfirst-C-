using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace A_day_at_the_Races
{
    class Greyhound
    {
        public int StartingPosition, RacetrackLength;
        public PictureBox MyPictureBox;// = null;
        public int Location = 0;
        public Random Randomizer;

        public bool Run()
        {
            Location += Randomizer.Next(1, 4);
            MyPictureBox.Left = StartingPosition + Location;

            // has dog crossed finish line?
            if (MyPictureBox.Right >= RacetrackLength)
                return true;
            else
                return false;
        }

        public void TakeStartingPosition()
        {
            Location = 0;
            MyPictureBox.Left = StartingPosition;
        }


    }
}
