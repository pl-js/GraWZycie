using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Gra
{
    [Serializable]
    public abstract class Animal : Organism
    {
        public Animal(int strength, int location, char sign, int deathCounter) : base(strength, location, sign, deathCounter) { }
        public Animal(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /**
        *  Spawn - trying to Spawn a new this.class object.
        *  When to same class objects meet the Spawn method is executed.
        *  It looks for an empty Location around currentLocation. If it finds it then it spawns a new this.class object.
        *  Else nothing happens and Spawn fails.
        */
        override public abstract void Spawn(World world);

        override public Organism GetCloned() { return null; }

        /**
        *  updating StrengthModifier
        */
        override public void UpdateStrength(Area area)
        {
            this.Strength -= this.StrengthModifier;
            if (area.Type == 1)
            {                      // area.Type = 1 (Desert)
                this.StrengthModifier = -2;
            }
            else
            {
                this.StrengthModifier = 0;
            }
            this.Strength += this.StrengthModifier;
        }

        /**
        *  Action - trying to move to nearby random actionLocation,
        *  depending on what's standing on a actionLocation it has different outcomes
        */
        override public void Action(World world)
        {
            int actionLocation = world.GenerateNearbyLocation(Location);
            MainWindow.logInfo += "Log: \t" + this.ToString() + "(" + this.Strength + ") on space (" + this.Location % world.Size + "," + this.Location / world.Size + ") ";
            MainWindow.logInfo += "tries to move to (" + actionLocation % world.Size + "," + actionLocation / world.Size + ").\n";
            if ((world.AreaBoard.ElementAt(this.Location).Type == 2) && (world.Generator.Next(0, 2) == 0)) // if area.Type = 2 (swamp) -> 50% chance that it fails to move
            {
                MainWindow.logInfo += "\t\tbut it's stuck in a swamp.\n";
            }
            else
            {
                if (world.OrganismBoard.ElementAt(actionLocation) == null)
                {                                    // move to empty Location
                    MainWindow.logInfo += "\tIt succeeded.\n";
                    world.AddToBoard(actionLocation, this);
                    world.NullToBoard(this.Location);
                    this.Location = actionLocation;
                }
                else if (world.OrganismBoard.ElementAt(actionLocation).GetType().Name == this.GetType().Name)
                {                                   // Spawn new Animal
                    MainWindow.logInfo += "\t\tIt encounters same class Animal ";
                    Spawn(world);
                }
                else
                {                                                // fight
                    MainWindow.logInfo += "\t\tIt encounters ";
                    if (world.OrganismBoard.ElementAt(actionLocation).Collision(world, this))
                    {                                           // win, moving over
                        world.AddToBoard(actionLocation, this);
                        world.NullToBoard(this.Location);
                        this.Location = actionLocation;
                    }
                    else
                    {                                             // lose, dying
                        world.NullToBoard(Location);
                        this.Alive = false;
                    }
                }
            }
        }
    }
}
