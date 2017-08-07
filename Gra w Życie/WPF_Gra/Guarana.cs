using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Gra
{
    [Serializable]
    public class Guarana : Plant
    {

        public Guarana(int location) : base(0, location, 'U', 0) { }
        private Guarana(Organism old) : base(old.Strength, old.Location, old.Sign, old.DeathCounter) { }
        public Guarana(SerializationInfo info, StreamingContext context) : base(info, context) { }


        /**
        *  Spawn - trying to Spawn a new this.class object.
        *  It has a 50% chance.
        *  It looks for an empty Location around currentLocation. If it finds it then it spawns a new this.class object.
        *  Else nothing happens and Spawn fails.
        */
        override public void Spawn(World world)
        {
            int location;
            MainWindow.logInfo += "Log: \tGuarana on space (" + this.Location % world.Size + "," + this.Location / world.Size + ") ";
            if (world.GetGenerator().Next(0, 2) == 1)
            { /// 50% chance for Spawn
                location = world.GenerateNearbyEmptyLocation(this.Location);
                MainWindow.logInfo += "succeeded to Spawn new Guarana";
                if (location != -1)
                {
                    world.AddToBoard(location, new Guarana(location));
                    world.AddingList.AddLast(world.OrganismBoard.ElementAt(location));
                    MainWindow.logInfo += " on space (" + location % world.Size + "," + location / world.Size + ").\n";
                }
                else
                {
                    MainWindow.logInfo += " but there is no empty space around to put it. Spawn failed.\n";
                }
            }
            else
                MainWindow.logInfo += "failed to spawned a new Guarana.\n";
        }

        /** When an Animal attacks Gaurana it eats it and gains Strength
         * Depending on an area.Type the Strength gain is 2 (for Glade) or 3 (for any other area)
         */
        override public bool Collision(World world, Organism attackingOrganism)
        {
            int strengthToGain;
            if (world.AreaBoard[this.Location].Type == 0)
            {
                strengthToGain = 2;
            }
            else
            {
                strengthToGain = 3;
            }
            MainWindow.logInfo += this.ToString() + "(" + this.Strength + ") and eats it gaining " + strengthToGain + " Strength.\n";
            attackingOrganism.AddStrength(strengthToGain);
            world.NullToBoard(Location);
            this.Alive = false;
            return true;
        }


        override public string ToString()
        {
            return "Guarana";
        }

        override public Organism GetCloned()
        {
            return new Guarana(this);
        }

    }
}
