using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Gra
{ 
    [Serializable]
    public class Area : ISerializable
    {
        public int Type { get; private set; }  /// 0 - Glade, 1 - Desert, 2 - Swamp, 3 - Forest
        public int[] Influence { get; private set; }
        public List<int> MaxInfluenceType { get; private set; }
        public int InfluenceDifference { get; private set; }
        private int MaxInfluenceValue { get; set; }

        public Area(int type)
        {
            this.Type = type;
            this.Influence = new int[4] { 0, 0, 0, 0 };
            this.MaxInfluenceType = new List<int> { };
            this.InfluenceDifference = 0;
            this.MaxInfluenceValue = 0;
        }

        public Area(Area area)
        {
            this.Type = area.Type;
            this.Influence = area.Influence;
            this.MaxInfluenceType = area.MaxInfluenceType;
            this.InfluenceDifference = area.InfluenceDifference;
            this.MaxInfluenceValue = area.MaxInfluenceValue;
        }

        /**
        *  returning clone of an Organism
        */
        public Area GetCloned()
        {
            return new Area(this);
        }

        public void CheckInfuence(World world, LinkedList<int> potentialList)
        {
            Influence = new int[4] { 0, 0, 0, 0 };
            MaxInfluenceType.Clear();
            foreach (int location in potentialList)
            {
                Influence[world.AreaBoard[location].Type]++;
            }

            MaxInfluenceValue = Influence.Max();
            for (int i = 0; i < 4; i++)
            {
                if (Influence[i] >= MaxInfluenceValue - 1)
                    MaxInfluenceType.Add(i);
            }
            InfluenceDifference = MaxInfluenceValue - Influence[Type];
        }

        public void AddInfluence(int type)
        {
            this.Influence[type]++;
        }

        public void Evolve(Random generator)
        {
            this.Type = MaxInfluenceType.ElementAt(generator.Next(MaxInfluenceType.Count));
        }

        override public string ToString()
        {
            switch (Type)
            {
                case 0:
                    return "Polana";
                case 1:
                    return "Pustynia";
                case 2:
                    return "Bagno";
                case 3:
                    return "Las";
                default:
                    return "inne";
            }
        }

        public Area(SerializationInfo info, StreamingContext context)
        {
            Type = (int)info.GetValue("Type", typeof(int));
            this.Influence = new int[4] { 0, 0, 0, 0 };
            Influence[0] = (int)info.GetValue("Influence0", typeof(int));
            Influence[1] = (int)info.GetValue("Influence1", typeof(int));
            Influence[2] = (int)info.GetValue("Influence2", typeof(int));
            Influence[3] = (int)info.GetValue("Influence3", typeof(int));
            InfluenceDifference = (int)info.GetValue("InfluenceDifference", typeof(int));
            MaxInfluenceValue = (int)info.GetValue("MaxInfluenceValue", typeof(int));
            this.MaxInfluenceType = new List<int> { };
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Type", Type, typeof(int));
            info.AddValue("Influence0", Influence[0], typeof(int));
            info.AddValue("Influence1", Influence[1], typeof(int));
            info.AddValue("Influence2", Influence[2], typeof(int));
            info.AddValue("Influence3", Influence[3], typeof(int));
            info.AddValue("InfluenceDifference", InfluenceDifference, typeof(int));
            info.AddValue("MaxInfluenceValue", MaxInfluenceValue, typeof(int));
        }
    }
}
