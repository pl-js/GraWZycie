using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Gra
{
    [Serializable]
    public class ThornEater : Animal
    {

        public ThornEater(int location) : base(4, location, 'E', 0) { }
        private ThornEater(Organism old) : base(old.Strength, old.Location, old.Sign, old.DeathCounter) { }
        public ThornEater(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /**
         *  ThornEater has a 50% chance to Spawn 2 new ThornEaters when Spawn() method is executed in the Forest (area.Type = 3)
         */
        override public void Spawn(World world)
        {
            int location = world.GenerateNearbyEmptyLocation(this.Location);
            if (location != -1)
            {                                                                  // first Spawn successful
                MainWindow.logInfo += "and spawns a new " + this.ToString() + " on (" + location % world.Size + "," + location / world.Size + ").\n";
                world.AddToBoard(location, new ThornEater(location));
                world.AddingList.AddLast(world.OrganismBoard.ElementAt(location));
                if ((world.Generator.Next(0, 2) == 0) && (world.AreaBoard[this.Location].Type == 3))
                {
                    MainWindow.logInfo += "These are twins! ";
                    location = world.GenerateNearbyEmptyLocation(this.Location);
                    if (location != -1)
                    {                                                               // secound Spawn successful
                        MainWindow.logInfo += "New " + this.ToString() + " spawns on (" + location % world.Size + "," + location / world.Size + ").\n";
                        world.AddToBoard(location, new ThornEater(location));
                        world.AddingList.AddLast(world.OrganismBoard.ElementAt(location));
                    }
                    else                                                     // secound Spawn failed - no space
                    {
                        MainWindow.logInfo += "But there is no empty space for another " + this.ToString() + ".\n";
                    }
                }
            }
            else
            {                                                                            // first Spawn failed - no empty space
                MainWindow.logInfo += "but cannot Spawn a new " + this.ToString() + " - no empty space around\n";
            }

        }

        override public string ToString()
        {
            return "ThornEater";
        }

        override public Organism GetCloned()
        {
            return new ThornEater(this);
        }

    }
}
