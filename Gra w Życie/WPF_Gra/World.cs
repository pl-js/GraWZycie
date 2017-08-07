using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Runtime.Serialization;

namespace WPF_Gra
{
    [Serializable]
    public class World : ISerializable
    {
        private Random _generator = new Random();
        public MainWindow Window { get; set; }

        public Random Generator
        {
            get
            {
                return _generator;
            }
        }
        public int Size { get; private set; }
        public static int DeathCounterLimit { get; set; }
        public static int MaxEvolvesPerTurn { get; set; }
        public List<Organism> OrganismBoard { get; set; }
        public List<Area> AreaBoard { get; set; }
        public LinkedList<Organism> Init6Mice { get; set; }
        public LinkedList<Organism> Init5Wolfs { get; set; }
        public LinkedList<Organism> Init4SheepAndThornEaters { get; set; }
        public LinkedList<Organism> Init3Snakes { get; set; }
        public LinkedList<Organism> Init1Plants { get; set; }
        public LinkedList<Organism> AddingList { get; set; }
        public LinkedList<int> MaxDifferenceList { get; set; }
        private int TurnsFromLastEvolve;
        private int TurnsFromGeneratingMaxDiffList;

 
        public World(int size)
        {
            this.Size = size;
            DeathCounterLimit = 4;
            MaxEvolvesPerTurn = size * size / 10;
            this.OrganismBoard = new List<Organism>();
            this.AreaBoard = new List<Area>();
            this.Init6Mice = new LinkedList<Organism>();
            this.Init5Wolfs = new LinkedList<Organism>();
            this.Init4SheepAndThornEaters = new LinkedList<Organism>();
            this.Init3Snakes = new LinkedList<Organism>();
            this.Init1Plants = new LinkedList<Organism>();
            this.AddingList = new LinkedList<Organism>();
            this.MaxDifferenceList = new LinkedList<int>();
            this.TurnsFromLastEvolve = 0;
            this.TurnsFromGeneratingMaxDiffList = 0;
            InitializeWorld();
        }

        /**
         *  deep copy constructor
         */
        public World(World oldWorld)
        {
            this.Size = oldWorld.Size;
            this.OrganismBoard = new List<Organism>();
            this.AreaBoard = new List<Area>();
            this.Init6Mice = new LinkedList<Organism>();
            this.Init5Wolfs = new LinkedList<Organism>();
            this.Init4SheepAndThornEaters = new LinkedList<Organism>();
            this.Init3Snakes = new LinkedList<Organism>();
            this.Init1Plants = new LinkedList<Organism>();
            this.AddingList = new LinkedList<Organism>();
            this.MaxDifferenceList = new LinkedList<int>();
            this.TurnsFromLastEvolve = oldWorld.TurnsFromLastEvolve;
            this.TurnsFromGeneratingMaxDiffList = oldWorld.TurnsFromGeneratingMaxDiffList;

            for (int j = 0; j < oldWorld.OrganismBoard.Count; j++)
            {       // copying Organisms on OrganismBoard
                if (oldWorld.OrganismBoard.ElementAt(j) != null)
                {
                    this.OrganismBoard.Add(oldWorld.OrganismBoard.ElementAt(j).GetCloned());
                }
                else
                {
                    this.OrganismBoard.Add(null);
                }
            }

            for (int j = 0; j < oldWorld.AreaBoard.Count; j++)
            {   // copying Areas on OrganismBoard
                if (oldWorld.AreaBoard.ElementAt(j) != null)
                {
                    this.AreaBoard.Add(oldWorld.AreaBoard.ElementAt(j).GetCloned());
                }
                else
                {
                    this.AreaBoard.Add(null);
                }
            }

            // recreating initLists

            foreach (Organism organism in oldWorld.Init1Plants)
            {
                if ((organism.Alive) && (organism != null))
                {
                    this.Init1Plants.AddLast(this.OrganismBoard.ElementAt(organism.Location));
                }
            }
            foreach (Organism organism in oldWorld.Init3Snakes)
            {
                if ((organism.Alive) && (organism != null))
                {
                    this.Init3Snakes.AddLast(this.OrganismBoard.ElementAt(organism.Location));
                }
            }
            foreach (Organism organism in oldWorld.Init4SheepAndThornEaters)
            {
                if ((organism.Alive) && (organism != null))
                {
                    this.Init4SheepAndThornEaters.AddLast(this.OrganismBoard.ElementAt(organism.Location));
                }
            }
            foreach (Organism organism in oldWorld.Init5Wolfs)
            {
                if ((organism.Alive) && (organism != null))
                {
                    this.Init5Wolfs.AddLast(this.OrganismBoard.ElementAt(organism.Location));
                }
            }
            foreach (Organism organism in oldWorld.Init6Mice)
            {
                if ((organism.Alive) && (organism != null))
                {
                    this.Init6Mice.AddLast(this.OrganismBoard.ElementAt(organism.Location));
                }
            }
            foreach (int i in oldWorld.MaxDifferenceList)
            {
                this.MaxDifferenceList.AddLast(i);
            }
        }

        /**
         *  execute one turn in game
         */
        public void ExecuteTurn()
        {
            ExecuteActionForInitList(Init6Mice);
            ExecuteActionForInitList(Init5Wolfs);
            ExecuteActionForInitList(Init4SheepAndThornEaters);
            ExecuteActionForInitList(Init3Snakes);
            ExecuteActionForInitList(Init1Plants);
            EvolveAreaboard();
            UpdateStrengthModifiers();
            CheckEnvironmentalDeaths();
            PrintBoard();
        }
        /**
         *  ExecuteActionForInitList is a method that iterates through currentList LinkedList
         *  and calls Action() and UpdateDeathCounter() methods.
         *  First it clears the AddingList.
         *  When it encounters an object which status Alive = false it deletes it.
         *  At the end it adds new Organisms from AddingList to currentList.
         */
        private void ExecuteActionForInitList(LinkedList<Organism> currentList)
        {
            AddingList.Clear();

            var node = currentList.First;
            while (node != null)
            {
                var next = node.Next;
                if (node.Value.Alive == true)
                {
                    node.Value.Action(this);
                    node.Value.UpdateDeathCounter(this.AreaBoard.ElementAt(node.Value.Location));
                }
                else
                {
                    currentList.Remove(node);
                }
                node = next;
            }

            foreach (Organism element in AddingList)
            {
                currentList.AddLast(element);
            }
        }

        /**
         *  changing hole AreaBoard to selected Type - for debugging
         */
        private void EvolveAreaboard()
        {
            TurnsFromGeneratingMaxDiffList++;
            TurnsFromLastEvolve++;
            Console.WriteLine("Evolve:");
            if (TurnsFromLastEvolve > 2)
            {
                Console.WriteLine("Time to evolve");
                TurnsFromLastEvolve = 0;
                if (TurnsFromGeneratingMaxDiffList > 8)
                {
                    Console.WriteLine("Time to generate (>8)");
                    GenerateMaxDifferenceList();
                    TurnsFromGeneratingMaxDiffList = 0;
                }
                for (int i = 0; i < MaxEvolvesPerTurn; i++)
                {
                    if (MaxDifferenceList.Any())
                    {
                        int ChosenElement = MaxDifferenceList.ElementAt(Generator.Next(MaxDifferenceList.Count));
                        Console.WriteLine("Evolve element at: " + ChosenElement);
                        AreaBoard[ChosenElement].Evolve(Generator);
                        MaxDifferenceList.Remove(ChosenElement);
                    }
                    else
                    {
                        Console.WriteLine("Empty - generate list");
                        GenerateMaxDifferenceList();
                        TurnsFromGeneratingMaxDiffList = 0;
                        i--;
                    }
                }
            }
        }

        private void GenerateMaxDifferenceList()
        {
            int MaxInfluenceDifference = 0;
            for (int i = 0; i < Size * Size; i++)
            {
                AreaBoard[i].CheckInfuence(this, PotentialList(i));
                if (AreaBoard[i].InfluenceDifference > MaxInfluenceDifference)
                {
                    MaxInfluenceDifference = AreaBoard[i].InfluenceDifference;
                }
            }
            this.MaxDifferenceList = new LinkedList<int> { };
            for (int i = 0; i < Size * Size; i++)
            {
                if (AreaBoard[i].InfluenceDifference == MaxInfluenceDifference)
                {
                    MaxDifferenceList.AddLast(i);
                }
            }
        }


        /**
         *  updating StrengthModifier for every Organism on board by calling UpdateStrength()
         */
        private void UpdateStrengthModifiers()
        {
            for (int i = 0; i < OrganismBoard.Count; i++)
            {
                if (OrganismBoard[i] != null)
                {
                    OrganismBoard[i].UpdateStrength(AreaBoard[i]);
                }
            }
        }


        /**
         *  Checking for enviromental kills.
         *  When DeathCounter of an Organism exceeds DeathCounterLimit (4 by default) the Organism dies.
         */
        private void CheckEnvironmentalDeaths()
        {
            for (int i = 0; i < OrganismBoard.Count; i++)
            {
                if (OrganismBoard[i] != null)
                {
                    if (OrganismBoard[i].DeathCounter > DeathCounterLimit)
                    {
                        OrganismBoard[i].Alive = false;
                        Console.WriteLine(OrganismBoard[i].ToString() + " umiera - ponad "+DeathCounterLimit+" tury w nieprzyjaznym środowisku.");
                        NullToBoard(i);
                    }
                }
            }
        }

        /**
         *  InitializeWorld is a method that fills the OrganismBoard with nulls and starting Animals and Plants.
         *  First it counts how many Animals and Plants should be added.
         *  Then it initializes AreaBoard and OrganismBoard
         */
        private void InitializeWorld()
        {
            int animalsAmount = GetAnimalsAmount(Size);
            int plantsAmount = GetPlantsAmount(Size);
            InitializeOrganismBoard(plantsAmount, animalsAmount);
            InitializeAreaBoard();
            UpdateStrengthModifiers();
            GenerateMaxDifferenceList();
        }

        /**
         *  The formula for Plants (expect Thorns) is toUpper(Size*Size/25);
         */
        private int GetPlantsAmount(int size)
        {
            double howManyPlants = (double)(size * size);
            howManyPlants /= 15;
            if (howManyPlants % 1 > 0.5)
                howManyPlants++;
            return (int)Math.Truncate(howManyPlants);
        }

        /**
         *  The formula for Animals and Thorns is toUpper(Size*Size/15);
         */
        private int GetAnimalsAmount(int size)
        {
            Double howManyAnimals = (double)(size * size);
            howManyAnimals /= 25;
            if (howManyAnimals % 1 > 0.5)
                howManyAnimals++;
            return (int)Math.Truncate(howManyAnimals);
        }

        /**
         *  InitializeOrganismBoard is a method that fills the OrganismBoard with nulls and starting Animals and Plants.
         */
        private void InitializeOrganismBoard(int plantsAmount, int animalsAmount)
        {
            for (int i = 0; i < Size * Size; i++)
                OrganismBoard.Add(null);

            for (int i = 0; i < plantsAmount; i++)
            {
                Organism temp = new Grass(GenerateEmptyLocation());
                AddToBoard(temp.Location, temp);
                Init1Plants.AddLast(temp);
            }
            for (int i = 0; i < plantsAmount; i++)
            {
                Organism temp = new Guarana(GenerateEmptyLocation());
                AddToBoard(temp.Location, temp);
                Init1Plants.AddLast(temp);
            }
            for (int i = 0; i < animalsAmount; i++)
            {
                Organism temp = new Thorn(GenerateEmptyLocation());
                AddToBoard(temp.Location, temp);
                Init1Plants.AddLast(temp);
            }
            for (int i = 0; i < animalsAmount; i++)
            {
                Organism temp = new Wolf(GenerateEmptyLocation());
                AddToBoard(temp.Location, temp);
                Init5Wolfs.AddLast(temp);
            }
            for (int i = 0; i < animalsAmount; i++)
            {
                Organism temp = new Sheep(GenerateEmptyLocation());
                AddToBoard(temp.Location, temp);
                Init4SheepAndThornEaters.AddLast(temp);
            }
            for (int i = 0; i < animalsAmount; i++)
            {
                Organism temp = new ThornEater(GenerateEmptyLocation());
                AddToBoard(temp.Location, temp);
                Init4SheepAndThornEaters.AddLast(temp);
            }
            for (int i = 0; i < animalsAmount; i++)
            {
                Organism temp = new Snake(GenerateEmptyLocation());
                AddToBoard(temp.Location, temp);
                Init3Snakes.AddLast(temp);
            }
            for (int i = 0; i < animalsAmount; i++)
            {
                Organism temp = new Mouse(GenerateEmptyLocation());
                AddToBoard(temp.Location, temp);
                Init6Mice.AddLast(temp);
            }
        }

        /**
         *  initializing the AreaBoard
         */
        private void InitializeAreaBoard()
        {
            for (int i = 0; i < Size * Size; i++)       // Filling up the AreaBoard with Areas of type (-1)
            {
                AreaBoard.Add(new Area(-1));
            }
            List<int>[] InfluencedLocationsList = new List<int>[4];
            for (int i = 0; i < 4; i++)                 // Adding 4 starting points
            {
                InfluencedLocationsList[i] = new List<int>();     // separate InfluencedList for every type of Area, containing list of free Areas (type=-1) that are influenced by current Type
                int location = Generator.Next(0, Size * Size);
                while (AreaBoard[location].Type != -1)
                {
                    location = Generator.Next(0, Size * Size);
                }
                AreaBoard[location] = new Area(i);
                LinkedList<int> potentialList = PotentialList(location);
                foreach (int loc in potentialList)
                {
                    if (AreaBoard[loc].Type == -1)
                    {
                        AreaBoard[loc].AddInfluence(i);
                        InfluencedLocationsList[i].Add(loc);
                    }
                }
            }

            int type = 0;                                  // type of Area to add
            while (AreaBoard.Exists(area => area.Type == -1))        // Filling up the Areaboard until there is no free spaces (Areas.type -1)
            {
                if (type > 3)
                {
                    type = 0;
                }
                int chosenLocation = -1;
                if (InfluencedLocationsList[type].Any())
                {
                    int maxInfluence = 0;
                    List<int> maxInfluenceList = new List<int>();
                    foreach (int loc in InfluencedLocationsList[type])                        // chosing one of the Areas that has max Influence of current Type
                    {
                        if (AreaBoard[loc].Influence[type] > maxInfluence)
                        {
                            maxInfluence = AreaBoard[loc].Influence[type];
                            maxInfluenceList.Clear();
                            maxInfluenceList.Add(loc);
                        }
                        else if (AreaBoard[loc].Influence[type] == maxInfluence)
                        {
                            maxInfluenceList.Add(loc);
                        }
                    }
                    chosenLocation = maxInfluenceList[Generator.Next(0, maxInfluenceList.Count())];
                }
                else                                                                   // there is no Area on InfluencedList, choosing random free Area on board
                {
                    chosenLocation = Generator.Next(0, Size * Size);
                    while (AreaBoard[chosenLocation].Type != -1)
                    {
                        chosenLocation = Generator.Next(0, Size * Size);
                    }
                }
                AreaBoard[chosenLocation] = new Area(type);
                for (int j = 0; j < 4; j++)
                {
                    InfluencedLocationsList[j].Remove(chosenLocation);              // removing locations from lists that are already taken
                }
                LinkedList<int> potentialList = PotentialList(chosenLocation);
                foreach (int loc in potentialList)                                  // adding new InfluencedAreas to the list
                {
                    if (AreaBoard[loc].Type == -1)
                    {
                        AreaBoard[loc].AddInfluence(type);
                        if (!InfluencedLocationsList[type].Contains(loc))
                        {
                            InfluencedLocationsList[type].Add(loc);
                        }
                    }
                }
                type++;
            }
        }

        /** Method printing current OrganismBoard state (colours) */
        public void PrintBoard()
        {
            for (int i = Size - 1; i >= 0; i--)
                {
                for (int j = 0; j < Size; j++)
                {
                    Label a = new Label();
                    switch (AreaBoard[(i * Size + j)].Type)
                    {
                        case 0:
                            a.Background = new SolidColorBrush(Colors.LawnGreen);
                            break;
                        case 1:
                            a.Background = new SolidColorBrush(Colors.Gold);
                            break;
                        case 2:
                            a.Background = new SolidColorBrush(Colors.Olive);
                            break;
                        case 3:
                            a.Background = new SolidColorBrush(Colors.SeaGreen);
                            break;
                    }

                    if (OrganismBoard[(i * Size + j)] != null)
                    {
                        a.Content = OrganismBoard[(i * Size + j)].Sign + "(" + OrganismBoard[(i * Size + j)].Strength+ ")"; 
                     }

                    Grid.SetRow(a, i);
                    Grid.SetColumn(a, j);
                    MainWindow.boardGrid.Children.Add(a);
                    Console.Write('.');
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }


        /** GenerateEmptyLocation return an empty Location on a OrganismBoard for a new Organism to be spawned,
        *   it's used at the initialization of the World method   **/
        public int GenerateEmptyLocation()
        {
            int location = Generator.Next(this.Size * this.Size);
            while (OrganismBoard[location] != null)
            {
                location = Generator.Next(0,this.Size * this.Size);
            }
            return location;
        }


        /** GenerateNearbyEmptyLocation returns random, empty slot around the currentLocation of an Organism.
         *  It's used during Action() - moving process and spawning a new Organism object.
         *  It uses PotentialList() method to get a list of possible nearby locations,
         *  then it removes locations that are occupied.
         *  If there is no empty Location around currentLocation the method returns -1.
         */
        public int GenerateNearbyEmptyLocation(int currentLocation)
        {
            LinkedList<int> locList = PotentialList(currentLocation);
            var node = locList.First;
            while (node != null)
            {
                var next = node.Next;
                if (OrganismBoard[node.Value] != null)
                    locList.Remove(node);
                node = next;
            }
            if (!locList.Any())
            {
                return -1;
            }
            else
            {
                return locList.ElementAt(Generator.Next(locList.Count));
            }
        }


        /** GenerateNearbyLocation returns random Location from spaces around the currentLocation of an Organism.
         * **/
        public int GenerateNearbyLocation(int currentLocation)
        {
            LinkedList<int> locList = PotentialList(currentLocation);
            return locList.ElementAt(Generator.Next(locList.Count));
        }

        /** PotentialList returns LinkedList of Integers with possible locations around the currentLocation.
         * **/
        private LinkedList<int> PotentialList(int currentLocation)
        {
            int startingI = 0, startingJ = 0;
            int rangeI = 3, rangeJ = 3;
            LinkedList<int> potentialLocation = new LinkedList<int>();

            if (currentLocation % Size == 0)
            {
                startingI++;
            }
            if (currentLocation % Size == Size - 1)
            {
                rangeI--;
            }
            if (currentLocation / Size == 0)
            {
                startingJ++;
            }
            if (currentLocation / Size == Size - 1)
            {
                rangeJ--;
            }
            int checkingLocation = currentLocation - 1 - Size;
            for (int i = startingI; i < rangeI; i++)
            {
                for (int j = startingJ; j < rangeJ; j++)
                {
                    if (checkingLocation + i + Size * j != currentLocation)
                    {
                        potentialLocation.AddLast(checkingLocation + i + Size * j);
                    }
                }
            }
            return potentialLocation;
        }

        /** Simple method adding organism in given Location
         *  It removes previous Organism (or Null) from the OrganismBoard ArrayList and adds organism in it's place
         */
        public void AddToBoard(int location, Organism organism)
        {
             OrganismBoard[location]=null;
             OrganismBoard[location]=organism;
        }

        /** Simple method adding null in given Location
         *  It removes previous Organism (or Null) from the OrganismBoard ArrayList and adds null in it's place
         */
        public void NullToBoard(int location)
        {
            OrganismBoard[location]= null;
        }

        public Random GetGenerator()
        {
            return Generator;
        }

        public World(SerializationInfo info, StreamingContext context)
        {
            this.Size = (int)info.GetValue("WorldSize", typeof(int));
            DeathCounterLimit = (int)info.GetValue("DeathCounterLimit", typeof(int));
            MaxEvolvesPerTurn = (int)info.GetValue("MaxEvolvesPerTurn", typeof(int));
            this.TurnsFromLastEvolve = (int)info.GetValue("TurnsFromLastEvolve", typeof(int));
            this.TurnsFromGeneratingMaxDiffList = (int)info.GetValue("TurnsFromGeneratingMaxDiffList", typeof(int));
            this.OrganismBoard = new List<Organism>();
            for (int i = 0; i < this.Size * this.Size; i++)
            {
                this.OrganismBoard.Add(null);
            }
            this.AreaBoard = new List<Area>();
            this.Init6Mice = new LinkedList<Organism>();
            this.Init5Wolfs = new LinkedList<Organism>();
            this.Init4SheepAndThornEaters = new LinkedList<Organism>();
            this.Init3Snakes = new LinkedList<Organism>();
            this.Init1Plants = new LinkedList<Organism>();
            this.AddingList = new LinkedList<Organism>();
            this.MaxDifferenceList = new LinkedList<int>();
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("WorldSize", Size, typeof(int));
            info.AddValue("DeathCounterLimit", DeathCounterLimit, typeof(int));
            info.AddValue("MaxEvolvesPerTurn", MaxEvolvesPerTurn, typeof(int));
            info.AddValue("TurnsFromLastEvolve", TurnsFromLastEvolve, typeof(int));
            info.AddValue("TurnsFromGeneratingMaxDiffList", TurnsFromGeneratingMaxDiffList, typeof(int));
        }
    }
}
