using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Gra
{
    [Serializable]
    public abstract class Plant : Organism{
        
        public Plant(int strength, int location, char sign, int deathCounter) : base(strength, location, sign, deathCounter) { }
        public Plant(SerializationInfo info, StreamingContext context) : base(info, context) { }

        override public void UpdateStrength(Area area) { }
        override public void Action(World world) { Spawn(world); }

        /**
        *  Spawn - trying to Spawn a new this.class object.
        *  It has a 50% chance (expect Thorn)
        *  It looks for an empty Location around currentLocation. If it finds it then it spawns a new this.class object.
        *  Else nothing happens and Spawn fails.
        */
        override public abstract void Spawn(World world);


        /** 
         *  When a Plant (expect Thorn) is atacked by an Animal it dies.
         */
        override public bool Collision(World world, Organism attackingOrganism)
        {
            //Console.WriteLine(this.ToString()+"("+this.Strength+") and eats it.");
            MainWindow.logInfo += this.ToString() + "(" + this.Strength + ") and eats it.";
            world.NullToBoard(Location);
            this.Alive = false;
            return true;
        }

    }
}
