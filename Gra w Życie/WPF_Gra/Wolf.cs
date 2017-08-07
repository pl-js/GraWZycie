using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Gra
{
    [Serializable]
    public class Wolf : Animal
    {

        public Wolf(int location) : base(6, location, 'W', 0) { }
        private Wolf(Organism old) : base(old.Strength, old.Location, old.Sign, old.DeathCounter) { }
        public Wolf(SerializationInfo info, StreamingContext context) : base(info, context) { }


        override public void Spawn(World world)
        {
            int location = world.GenerateNearbyEmptyLocation(this.Location);
            if (location != -1)
            {                                                                  // Spawn successful
                MainWindow.logInfo += "and spawns a new " + this.ToString() + " on (" + location % world.Size + "," + location / world.Size + ").\n";
                world.AddToBoard(location, new Wolf(location));
                world.AddingList.AddLast(world.OrganismBoard.ElementAt(location));
            }
            else
            {                                                                            // Spawn failed - no empty space
                MainWindow.logInfo += "but cannot Spawn a new " + this.ToString() + " - no empty space around\n";
            }
        }


        override public String ToString()
        {
            return "Wolf";
        }

        override public Organism GetCloned()
        {
            return new Wolf(this);
        }
    }
}
