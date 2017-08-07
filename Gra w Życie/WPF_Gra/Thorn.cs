using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Gra
{
    [Serializable]
    public class Thorn : Plant
    {

        public Thorn(int location) : base(2, location, 'T', 0) { }
        private Thorn(Organism old) : base(old.Strength, old.Location, old.Sign, old.DeathCounter) { }
        public Thorn(SerializationInfo info, StreamingContext context) : base(info, context) { }


        /**
        *  Spawn - trying to Spawn a new this.class object.
        *  It looks for an empty Location around currentLocation. If it finds it then it spawns a new this.class object.
        *  Else nothing happens and Spawn fails.
        */
        override public void Spawn(World world)
        {
            int location;
            MainWindow.logInfo += "Log: \tThorn on space (" + this.Location % world.Size + "," + this.Location / world.Size + ") ";
            if (world.GetGenerator().Next(0, 2) == 1)
            { /// 50% chance for Spawn
                location = world.GenerateNearbyEmptyLocation(this.Location);
                MainWindow.logInfo += "succeeded to Spawn new Thorn";
                if (location != -1)
                {
                    world.AddToBoard(location, new Thorn(location));
                    world.AddingList.AddLast(world.OrganismBoard.ElementAt(location));
                    MainWindow.logInfo += " on space (" + location % world.Size + "," + location / world.Size + ").\n";
                }
                else
                {
                    MainWindow.logInfo += " but there is no empty space around to put it. Spawn failed.\n";
                }
            }
            else
                MainWindow.logInfo += "failed to spawned a new Thorn.\n";
        }


        /** When a ThornEater attacks Thorn and eats it, it gains 2 Strength
         */
        override public bool Collision(World world, Organism attackingOrganism)
        {
            if (this.Strength > attackingOrganism.Strength)
            {
                MainWindow.logInfo += this.ToString() + "(" + this.Strength + ") and dies to its thorns.\n";
                return false;
            }
            else
            {
                MainWindow.logInfo += this.ToString() + "(" + this.Strength + ") and eats it";
                if (attackingOrganism.GetType().Name == "ThornEater")
                {
                    attackingOrganism.AddStrength(2);
                    MainWindow.logInfo += " gaining 2 Strength";
                }
                world.NullToBoard(Location);
                this.Alive = false;
                Console.WriteLine(".");
                return true;
            }
        }


        override public string ToString()
        {
            return "Thorn";
        }

        override public Organism GetCloned()
        {
            return new Thorn(this);
        }
    }
}
