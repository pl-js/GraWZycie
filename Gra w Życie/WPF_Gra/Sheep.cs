using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Gra
{
    [Serializable]
    public class Sheep : Animal
    {

        public Sheep(int location) : base(4, location, 'O', 0) { }
        private Sheep(Organism old) : base(old.Strength, old.Location, old.Sign, old.DeathCounter) { }
        public Sheep(SerializationInfo info, StreamingContext context) : base(info, context) { }

        override public void Spawn(World world)
        {
            int location = world.GenerateNearbyEmptyLocation(this.Location);
            if (location != -1)
            {                                                                  // Spawn successful
                MainWindow.logInfo += "and spawns a new " + this.ToString() + " on (" + location % world.Size + "," + location / world.Size + ").\n";
                world.AddToBoard(location, new Sheep(location));
                world.AddingList.AddLast(world.OrganismBoard.ElementAt(location));
            }
            else
            {                                                                            // Spawn failed - no empty space
                MainWindow.logInfo += "but cannot Spawn a new " + this.ToString() + " - no empty space around\n";
            }
        }

        override public string ToString()
        {
            return "Sheep";
        }

        override public Organism GetCloned()
        {
            return new Sheep(this);
        }

        override public void UpdateStrength(Area area)
        {
            this.Strength -= this.StrengthModifier;
            if ((area.Type == 1) || (area.Type == 3))
            {
                this.StrengthModifier = -2;
            }
            else
            {
                this.StrengthModifier = 0;
            }
            this.Strength += this.StrengthModifier;
        }
    }
}
