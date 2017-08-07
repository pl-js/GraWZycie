using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Gra
{
    [Serializable]
    public class Grass : Plant
    {

        public Grass(int location) : base(0, location, 'G', 0) { }
        private Grass(Organism old) : base(old.Strength, old.Location, old.Sign, old.DeathCounter) { }
        public Grass(SerializationInfo info, StreamingContext context) : base(info, context) { }


        /**
        *  Spawn - trying to Spawn a new this.class object.
        *  It has a 50% chance.
        *  It looks for an empty Location around currentLocation. If it finds it then it spawns a new this.class object.
        *  Else nothing happens and Spawn fails.
        */
        override public void Spawn(World world)
        {
            int location;
            //Console.Write("Log: \tGrass on space (" + this.Location % world.Size + "," + this.Location / world.Size + ") ");
            MainWindow.logInfo += "Log: \tGrass on space (" + this.Location % world.Size + "," + this.Location / world.Size + ") ";
            if (world.GetGenerator().Next(0, 2) == 1)
            { /// 50% chance for Spawn
                location = world.GenerateNearbyEmptyLocation(this.Location);
                //Console.Write("succeeded to Spawn new Grass");
                MainWindow.logInfo += "succeeded to Spawn new Grass\n";
                if (location != -1)
                {
                    world.AddToBoard(location, new Grass(location));
                    world.AddingList.AddLast(world.OrganismBoard.ElementAt(location));
                    //Console.WriteLine(" on space (" + location % world.Size +  "," + location / world.Size + ").");
                    MainWindow.logInfo += " on space (" + location % world.Size + "," + location / world.Size + "). ";
                }
                else
                {
                    //Console.WriteLine(" but there is no empty space around to put it. Spawn failed.");
                    MainWindow.logInfo += " but there is no empty space around to put it. Spawn failed.\n";
                }
            }
            else
                //Console.WriteLine("failed to spawn a new Grass.");
                MainWindow.logInfo += "failed to spawn a new Grass.\n";
        }

        override public string ToString()
        {
            return "Grass";
        }

        override public Organism GetCloned()
        {
            return new Grass(this);
        }

        /** 
         *  Cheeking for dangerous enviroment
         *  When Grass stands on a Desert (area.Type=1) or on Forest (area.Type=3) it increases its DeathCounter.
         *  DeathCounter reverts to 0 when Grass lands on a safe Area.
         */
        override public void UpdateDeathCounter(Area area)
        {
            if ((area.Type == 1) || (area.Type == 3))
                this.DeathCounter++;
            else
                this.DeathCounter = 0;
        }
    }
}
