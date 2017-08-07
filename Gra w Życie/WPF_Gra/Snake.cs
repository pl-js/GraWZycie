using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Gra
{
    [Serializable]
    class Snake : Animal
    {

        public Snake(int location) : base(2, location, 'S', 0) { }
        private Snake(Organism old) : base(old.Strength, old.Location, old.Sign, old.DeathCounter) { }
        public Snake(SerializationInfo info, StreamingContext context) : base(info, context) { }


        /** 
         *  If a Snake is attacked and loses the fight it dies but it also kills its attacker.
         */
        override public bool Collision(World world, Organism attackingOrganism)
        {
            if (this.Strength > attackingOrganism.Strength)
            {
                MainWindow.logInfo += this.ToString() + "(" + this.Strength + ") and dies to it.\n";
                return false;
            }
            else
            {
                MainWindow.logInfo += this.ToString() + "(" + this.Strength + ") and kills it but dies to its poison.\n";
                world.NullToBoard(Location);
                this.Alive = false;
                return false;
            }
        }


        override public void Spawn(World world)
        {
            int location = world.GenerateNearbyEmptyLocation(this.Location);
            if (location != -1)
            {                                                                  // Spawn successful
                MainWindow.logInfo += "and spawns a new " + this.ToString() + " on (" + location % world.Size + "," + location / world.Size + ").\n";
                world.AddToBoard(location, new Snake(location));
                world.AddingList.AddLast(world.OrganismBoard.ElementAt(location));
            }
            else
            {                                                                            // Spawn failed - no empty space
                MainWindow.logInfo += "but cannot Spawn a new " + this.ToString() + " - no empty space around\n";
            }
        }

        override public string ToString()
        {
            return "Snake";
        }

        override public Organism GetCloned()
        {
            return new Snake(this);
        }

        override public void UpdateStrength(Area area)
        { }
    }
}
