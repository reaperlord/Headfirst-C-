using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp5
{
   abstract class Location
    {
        protected Location(string name) { Name = name; }

        public Location[] Exits;
        public string Name { get; protected set;}
        public virtual string Description
        {
            get
            {
                string description = "You're standing in the " + Name + ". You see exits to the following places: ";
                
                for (int i= 0; i< Exits.Length; i++)
                {
                    description += " " + Exits[i].Name;

                    if (i != Exits.Length - 1)
                        description += ",";
                }
                description += ".";
                return description;
            }
        }

        protected  class Door
        {
            public Door(Location location1, Location location2, string description)
            { twoSides =new Location[] { location1, location2};
                Description = description;

                foreach( Location side in twoSides)
                {
                    if (side is RoomWithDoor)
                    {
                        RoomWithDoor room = side as RoomWithDoor;
                        room.setDoor(this);
                    }
                    else if (side is OutsideWithDoor)
                    {
                        OutsideWithDoor outside = side as OutsideWithDoor;
                        outside.setDoor(this);
                    }
                }

            }

            public string Description { get; private set; }

            private Location[] twoSides; 

            public Location GiveOtherSideOfDoor(Location oneSide)
            {
                foreach (Location side in twoSides)
                {
                    if (!side.Equals(oneSide))
                        return side;
                }

                return null;
            }
        }
        
        protected virtual void setDoor(Door door)
        { }



    }


    class Room: Location
    {
        private string decoration;

        public Room(string name): this(name, "")
        {            
        }

        public Room(string name, string decoration) : base(name)
        {
            this.decoration = decoration;
        }

        public override string Description
        {
            get
            {
                string description = base.Description;

                if (!string.IsNullOrEmpty(decoration))
                    description += " You see " + decoration + ".";

                return description;
            }
        }   

    }

    class RoomWithDoor : Room, IHasExteriorDoor
    {
        public RoomWithDoor(string name, string decoration, string doorDescription, OutsideWithDoor otherSide) : base(name, decoration)
        { createDoor(doorDescription, otherSide); }


        public RoomWithDoor(string name, string doorDescription, OutsideWithDoor otherSide ) : this(name, "", doorDescription, otherSide)
        { }

        
        public RoomWithDoor(string name, string decoration) : base (name, decoration){ }



        public string DoorDescription { get { return door.Description; } }

        public Location DoorLocation
        {
            get
            {
                return door.GiveOtherSideOfDoor(this);
            }
        }

        
        private Door door;

        protected override void setDoor(Door door)
        {
            this.door = door;
        }

        private void createDoor(string doorDescription, OutsideWithDoor otherSide)
        {
            door = new Door(this, otherSide, doorDescription);
        }

        public override string Description
        {
            get
            {
                string description = base.Description;

                description += "There is " + DoorDescription + " that outside inside to the " + (door.GiveOtherSideOfDoor(this)).Name + ".";

                return description;
            }
        }


    }



    class Outside : Location
    {
        public Outside(string name, bool hot) : base(name)
        {
            this.hot = hot;
        }

        private bool hot;


        public override string Description
        {
            get
            {
                string description = base.Description;

                if (hot)
                    description += " It's very hot here.";

                return description;
            }
        }

    }

    class OutsideWithDoor : Outside, IHasExteriorDoor
    {
        public OutsideWithDoor(string name, bool hot, string doorDescription, RoomWithDoor otherSide):base(name, hot)
        {
            createDoor(doorDescription, otherSide);
        }

        public OutsideWithDoor(string name, bool hot) : base(name, hot) { }

        public string DoorDescription { get { return door.Description; } }

        public Location DoorLocation
        { get { return door.GiveOtherSideOfDoor(this); } }

        private Door door;

        private void createDoor(string doorDescription, RoomWithDoor otherSide)
        {
            door = new Door(this, otherSide, doorDescription);            
        }

        protected override void setDoor(Door door)
        {
            this.door = door;
        }

        public override string Description
        {
            get
            {
                string description = base.Description;
                
                description += "There is " + DoorDescription + " that leads inside to the " + (door.GiveOtherSideOfDoor(this)).Name + ".";

                return description;
            }
        }


    }

}
