using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;


namespace WPF_Gra
{
    [Serializable]
    public abstract class Organism : ISerializable
    {
        public int Strength { get; set; }
        public int StrengthModifier { get; set; }
        public int Location { get; set; }
        public char Sign { get; private set; }
        public bool Alive { get; set; }
        public int DeathCounter { get; set; }

        public abstract void Action(World world);       // executing Action
        public abstract void Spawn(World world);        // spawning new Organism
        public abstract void UpdateStrength(Area area); // updating StrengthModifier
        public abstract Organism GetCloned();           // returning clone of an Organism

        protected Organism(int strength, int location, char sign, int deathCounter)
        {
            this.Strength = strength;
            this.Location = location;
            this.Sign = sign;
            this.Alive = true;
            this.DeathCounter = deathCounter;
        }

        /** When an Animal attacks an Organism it execute Organism's Collision method.
         *  Collision method compares attackingOrganism Strength and this.Organism Strength.
         *  If the attackingOrganism Strength is greater or equal to this.Strength this Organism dies. Method return true.
         *  If the attackingOrganism Strength is smaller than this.Strength nothing happens. Method returns false.
         */
        public virtual bool Collision(World world, Organism attackingOrganism)
        {
            if (this.Strength > attackingOrganism.Strength)
            {
                MainWindow.logInfo += this.ToString() + "(" + this.Strength + ") and dies to it.\n";
                return false;
            }
            else
            {
                MainWindow.logInfo += this.ToString() + "(" + this.Strength + ") and kills it.\n";
                world.NullToBoard(Location);
                this.Alive = false;
                return true;
            }
        }

        /** 
         *  Cheeking for dangerous enviroment
         *  When the Organism stands on a Desert (area.Type=1) it increases its DeathCounter.
         *  DeathCounter reverts to 0 when the Organism lands on a safe Area.
         */
        public virtual void UpdateDeathCounter(Area area)
        {
            if (area.Type == 1)
                this.DeathCounter++;
            else
                this.DeathCounter = 0;
        }

        public void AddStrength(int strength)
        {
            this.Strength += strength;
        }

        public Organism(SerializationInfo info, StreamingContext context)
        {
            Strength = (int)info.GetValue("Strength", typeof(int));
            StrengthModifier = (int)info.GetValue("StrengthModifier", typeof(int));
            Location = (int)info.GetValue("Location", typeof(int));
            Sign = (char)info.GetValue("Sign", typeof(char));
            Alive = (bool)info.GetValue("Alive", typeof(bool));
            DeathCounter = (int)info.GetValue("DeathCounter", typeof(int));
        }

        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Strength", Strength, typeof(int));
            info.AddValue("StrengthModifier", StrengthModifier, typeof(int));
            info.AddValue("Location", Location, typeof(int));
            info.AddValue("Sign", Sign, typeof(char));
            info.AddValue("Alive", Alive, typeof(bool));
            info.AddValue("DeathCounter", DeathCounter, typeof(int));
        }
    }
}