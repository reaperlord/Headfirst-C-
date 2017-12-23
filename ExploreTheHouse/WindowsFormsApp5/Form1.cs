using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp5
{
    public partial class Form1 : Form
    {
        Location currentLocation;

        private OutsideWithDoor frontYard, backYard;
        private Outside garden;

        private RoomWithDoor livingRoom, kitchen;
        private Room diningRoom;


        public Form1()
        {
            InitializeComponent();
            CreateObjects();

            //start at a location
            MoveToANewLocation(backYard);
        }

        private void goHere_Click(object sender, EventArgs e)
        {
            MoveToANewLocation(currentLocation.Exits[exits.SelectedIndex]);
        }

        private void CreateObjects()
        {
            frontYard = new OutsideWithDoor("Front Yard", false);
            backYard = new OutsideWithDoor("Back Yard", true);
            garden = new Outside("Garden", false);

            livingRoom = new RoomWithDoor("Living Room", "an antique carpet", "a screen door", frontYard);
            kitchen = new RoomWithDoor("Kitchen", "stainless steel appliances", "an oak door with a brass knob", backYard);
            diningRoom = new Room("Dining Room", "a crystal chandelier");

            frontYard.Exits = new Location[] { backYard, garden};            
            backYard.Exits = new Location[] { frontYard, garden};
            garden.Exits = new Location[] { frontYard, backYard };            
            livingRoom.Exits = new Location[] { diningRoom };            
            kitchen.Exits = new Location[] { diningRoom };            
            diningRoom.Exits = new Location[] { livingRoom, kitchen };
        }

        private void goThroughDoor_Click(object sender, EventArgs e)
        {
            IHasExteriorDoor currentHasDoor= currentLocation as IHasExteriorDoor;
            MoveToANewLocation(currentHasDoor.DoorLocation);
        }

        private void MoveToANewLocation(Location selectedLocation)
        {
            currentLocation = selectedLocation;
            //currentLocation = currentLocation.Exits[exits.SelectedIndex];

            // Clear items in combo box
            exits.Items.Clear();

            foreach (Location location in currentLocation.Exits)
            {
                exits.Items.Add(location.Name);
            }

            exits.SelectedIndex = 0;

            UpdateGUI();
        }


        private void UpdateGUI()
        {
            description.Text= currentLocation.Description;

            if (currentLocation is IHasExteriorDoor)
                goThroughDoor.Visible = true;
            else
                goThroughDoor.Visible = false;
        }

    }
}
