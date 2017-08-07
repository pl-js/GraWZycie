using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Gra
{
    [Serializable]
    public class Mouse : Animal
    {
        public Mouse(int location) : base(1, location, 'M', 0) { }
        private Mouse(Organism old) : base(old.Strength, old.Location, old.Sign, old.DeathCounter) { }
        public Mouse(SerializationInfo info, StreamingContext context) : base(info, context) { }


        /** 
         *  Mouse tries to escape then it loses the fight. It can't escape form Snake.
         *  It escape to nearbyEmptyLocation, if there is no place to escape it dies.
         */
        override public bool Collision(World world, Organism attackingOrganism)
        {
            if (this.Strength > attackingOrganism.Strength)
            {                                                            // attacking organism lost
                MainWindow.logInfo += this.ToString() + "(" + this.Strength + ") and dies to it.\n";
                return false;
            }
            else
            {                                                              // attacking organism win
                int location = world.GenerateNearbyEmptyLocation(this.Location);    // Mouse tries to escpae
                if ((location != -1) && (attackingOrganism.GetType().Name != "Snake"))
                {                                                           // and succeded
                    world.AddToBoard(location, this);
                    world.NullToBoard(this.Location);
                    this.Location = location;
                    MainWindow.logInfo += this.ToString() + "(" + this.Strength + ") and chase it away to (" + location % world.Size + "," + location / world.Size + ").\n";
                }
                else                                                        // can't escape, no place to escape or Snake attacking
                {
                    MainWindow.logInfo += this.ToString() + "(" + this.Strength + ") and kills it because it failed to escape.\n";
                    world.NullToBoard(this.Location);
                    this.Alive = true;
                }
                return true;
            }
        }


        /**
         *  Mouse has a 50% chance to Spawn 2 new Mouse when Spawn() method is executed
         */

        override public void Spawn(World world)
        {
            int location = world.GenerateNearbyEmptyLocation(this.Location);
            if (location != -1)
            {                                                                  // first Spawn successful
                MainWindow.logInfo += "and spawns a new " + this.ToString() + " on (" + location % world.Size + "," + location / world.Size + ").\n";
                world.AddToBoard(location, new Mouse(location));
                world.AddingList.AddLast(world.OrganismBoard.ElementAt(location));
                if ((world.Generator.Next(0, 2) == 0) && (world.AreaBoard[this.Location].Type == 0))
                {
                    MainWindow.logInfo += "These are twins! ";
                    location = world.GenerateNearbyEmptyLocation(this.Location);
                    if (location != -1)
                    {                                                               // secound Spawn successful
                        MainWindow.logInfo += "New " + this.ToString() + " spawns on (" + location % world.Size + "," + location / world.Size + ").\n";
                        world.AddToBoard(location, new Mouse(location));
                        world.AddingList.AddLast(world.OrganismBoard.ElementAt(location));
                    }
                    else                                                      // secound Spawn failed - no space
                    {
                        MainWindow.logInfo += "But there is no empty space for another " + this.ToString() + ".\n";
                    }
                }
            }
            else
            {                                                                   // first Spawn failed - no empty space
                MainWindow.logInfo += "but cannot Spawn a new " + this.ToString() + " - no empty space around\n";
            }

        }

        override public string ToString()
        {
            return "Mouse";
        }

        override public Organism GetCloned()
        {
            return new Mouse(this);
        }
    }
}
