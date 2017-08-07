using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WPF_Gra
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static int maxNumberOfPastTurns = 5; // number of passed turns kept in memory
        private static int pastTurnsMemorized = 0;
        private static int turnsCounter = 0;
        private static World[] _worldBoards = new World[maxNumberOfPastTurns + 1];
        public static Grid boardGrid { get; set; }
        public static string logInfo { get; set; }
        public static int worldSize { get; set; }
        public static TextBox logBox {get; set;}
        public static int previousTurns {get;set;}
        public static bool automatic { get; set; }
        public static DispatcherTimer dispatcherTimer { get; set; }
        public static Brush buttonDefault{get; set;}
        private string fileName = "GameSave.myData";
        private static bool stable;

        private static World[] worldBoards = new World[maxNumberOfPastTurns + 1];

        public MainWindow()
        {
            InitializeComponent();
            boardGrid = vBoard; //assign Window elements to static class fields
            logBox = lBox;
            automatic = false; // automatic turn execution is off
            buttonDefault = automaticButton.Background; //saving the color of the button
            stable = true;
        }

        // saving game to bin file
        public async static void SerializeGame(string fileName)  
        {
            while(stable == false)
            {
                await Task.Delay(25);
            }

            try
            {
                IFormatter formatter = new BinaryFormatter();
                FileStream s = new FileStream(fileName, FileMode.Create);
                int organismCounter;

                formatter.Serialize(s, maxNumberOfPastTurns);
                formatter.Serialize(s, turnsCounter);
                formatter.Serialize(s, pastTurnsMemorized);
                for (int i = 0; (i < pastTurnsMemorized + 1) && (i < maxNumberOfPastTurns); i++)
                {
                    formatter.Serialize(s, worldBoards[i]);
                    organismCounter = 0;
                    for (int j = 0; j < (worldBoards[i].Size * worldBoards[i].Size); j++)
                    {
                        if (worldBoards[i].OrganismBoard[j] != null)
                        {
                            organismCounter++;
                        }
                    }
                    formatter.Serialize(s, organismCounter);
                    for (int j = 0; j < (worldBoards[i].Size * worldBoards[i].Size); j++)
                    {
                        if (worldBoards[i].OrganismBoard[j] != null)
                        {
                            formatter.Serialize(s, worldBoards[i].OrganismBoard[j]);
                        }
                    }

                    organismCounter = 0;
                    foreach (Organism o in worldBoards[i].Init1Plants)
                    {
                        if (o != null)
                        {
                            if (o.Alive != false)
                            {
                                organismCounter++;
                            }
                        }
                    }
                    formatter.Serialize(s, organismCounter);
                    foreach (Organism o in worldBoards[i].Init1Plants)
                    {
                        if (o != null)
                        {
                            if (o.Alive != false)
                            {
                                formatter.Serialize(s, o.Location);
                            }
                        }
                    }

                    organismCounter = 0;
                    foreach (Organism o in worldBoards[i].Init3Snakes)
                    {
                        if (o != null)
                        {
                            if (o.Alive != false)
                            {
                                organismCounter++;
                            }
                        }
                    }
                    formatter.Serialize(s, organismCounter);
                    foreach (Organism o in worldBoards[i].Init3Snakes)
                    {
                        if (o != null)
                        {
                            if (o.Alive != false)
                            {
                                formatter.Serialize(s, o.Location);
                            }
                        }
                    }

                    organismCounter = 0;
                    foreach (Organism o in worldBoards[i].Init4SheepAndThornEaters)
                    {
                        if (o != null)
                        {
                            if (o.Alive != false)
                            {
                                organismCounter++;
                            }
                        }
                    }
                    formatter.Serialize(s, organismCounter);
                    foreach (Organism o in worldBoards[i].Init4SheepAndThornEaters)
                    {
                        if (o != null)
                        {
                            if (o.Alive != false)
                            {
                                formatter.Serialize(s, o.Location);
                            }
                        }
                    }

                    organismCounter = 0;
                    foreach (Organism o in worldBoards[i].Init5Wolfs)
                    {
                        if (o != null)
                        {
                            if (o.Alive != false)
                            {
                                organismCounter++;
                            }
                        }
                    }
                    formatter.Serialize(s, organismCounter);
                    foreach (Organism o in worldBoards[i].Init5Wolfs)
                    {
                        if (o != null)
                        {
                            if (o.Alive != false)
                            {
                                formatter.Serialize(s, o.Location);
                            }
                        }
                    }

                    organismCounter = 0;
                    foreach (Organism o in worldBoards[i].Init6Mice)
                    {
                        if (o != null)
                        {
                            if (o.Alive != false)
                            {
                                organismCounter++;
                            }
                        }
                    }
                    formatter.Serialize(s, organismCounter);
                    foreach (Organism o in worldBoards[i].Init6Mice)
                    {
                        if (o != null)
                        {
                            if (o.Alive != false)
                            {
                                formatter.Serialize(s, o.Location);
                            }
                        }
                    }

                    for (int j = 0; j < (worldBoards[i].Size * worldBoards[i].Size); j++)
                    {
                        formatter.Serialize(s, worldBoards[i].AreaBoard[j]);
                        formatter.Serialize(s, worldBoards[i].AreaBoard[j].MaxInfluenceType.Count());
                        foreach (int loc in worldBoards[i].AreaBoard[j].MaxInfluenceType)
                        {
                            formatter.Serialize(s, loc);
                        }
                    }

                    formatter.Serialize(s, worldBoards[i].MaxDifferenceList.Count());
                    foreach (int intiger in worldBoards[i].MaxDifferenceList)
                    {
                        formatter.Serialize(s, intiger);
                    }

                }
                s.Close();
                logInfo = "Game saved successfully!";
                logBox.Text = logInfo;
            }
            catch
            {
                logInfo = "Game save didn't succeed.";
                logBox.Text = logInfo;
            }
        }

        // Loading game from bin file
        public async static void DeserializeGame(string fileName)
        {
            while (stable == false)
            {
                await Task.Delay(25);
            }
            IFormatter formatter = new BinaryFormatter();
            FileStream s = new FileStream(fileName, FileMode.Open);

            maxNumberOfPastTurns = (int)formatter.Deserialize(s);
            turnsCounter = (int)formatter.Deserialize(s);
            pastTurnsMemorized = (int)formatter.Deserialize(s);
            worldBoards = new World[maxNumberOfPastTurns + 1];
            pastTurnsMemorized = 0;

            for (int i = 0; i < pastTurnsMemorized + 1; i++)
            {
                World w = (World)formatter.Deserialize(s);
                worldBoards[i] = w;
                int organismCounter = (int)formatter.Deserialize(s);
                for (int j = 0; j < organismCounter; j++)
                {
                    Organism o = (Organism)formatter.Deserialize(s);
                    worldBoards[i].OrganismBoard[o.Location] = o;
                }
                organismCounter = (int)formatter.Deserialize(s);
                for (int j = 0; j < organismCounter; j++)
                {
                    int location = (int)formatter.Deserialize(s);
                    worldBoards[i].Init1Plants.AddLast(worldBoards[i].OrganismBoard[location]);
                }
                organismCounter = (int)formatter.Deserialize(s);
                for (int j = 0; j < organismCounter; j++)
                {
                    int location = (int)formatter.Deserialize(s);
                    worldBoards[i].Init3Snakes.AddLast(worldBoards[i].OrganismBoard[location]);
                }
                organismCounter = (int)formatter.Deserialize(s);
                for (int j = 0; j < organismCounter; j++)
                {
                    int location = (int)formatter.Deserialize(s);
                    worldBoards[i].Init4SheepAndThornEaters.AddLast(worldBoards[i].OrganismBoard[location]);
                }
                organismCounter = (int)formatter.Deserialize(s);
                for (int j = 0; j < organismCounter; j++)
                {
                    int location = (int)formatter.Deserialize(s);
                    worldBoards[i].Init5Wolfs.AddLast(worldBoards[i].OrganismBoard[location]);
                }
                organismCounter = (int)formatter.Deserialize(s);
                for (int j = 0; j < organismCounter; j++)
                {
                    int location = (int)formatter.Deserialize(s);
                    worldBoards[i].Init6Mice.AddLast(worldBoards[i].OrganismBoard[location]);
                }
                for (int j = 0; j < worldBoards[i].Size * worldBoards[i].Size; j++)
                {
                    Area a = (Area)formatter.Deserialize(s);
                    worldBoards[i].AreaBoard.Add(a);
                    int MaxInfluenceTypeCount = (int)formatter.Deserialize(s);
                    for (int k = 0; k < MaxInfluenceTypeCount; k++)
                    {
                        worldBoards[i].AreaBoard[j].MaxInfluenceType.Add((int)formatter.Deserialize(s));
                    }
                }

                int MaxDifferenceListCount = (int)formatter.Deserialize(s);
                for (int j = 0; j < MaxDifferenceListCount; j++)
                {
                    worldBoards[i].MaxDifferenceList.AddLast((int)formatter.Deserialize(s));
                }

            }
            worldSize = worldBoards[0].Size;
            s.Close();

            logInfo = "Game loaded succesfully!";
            logBox.Text = logInfo;
        }

        // executing next turns
        private static void NextTurn() {
            stable = false;
            boardGrid.Children.Clear(); //clearing the board from previous turn display
            logInfo = "";               // clearing the log
            for(int i=5; i>1;i--) {
                worldBoards[i] = worldBoards[i - 1];
            }
            worldBoards[1] = new World(worldBoards[0]); //establishing a new world in a buffer as a copy of the gamestate from elapsing turn 
            worldBoards[0].ExecuteTurn();               //
            Console.WriteLine("");
            if(pastTurnsMemorized<5)                    //updating info about the buffer
                
                pastTurnsMemorized++;
            turnsCounter++;                             //updating info about passed turns
            logBox.Text = logInfo;                      //updating text of the logBox
            stable = true;

        }

        //going back to the previous turn written in the buffer
        private static void PreviousTurn(int round){
            for (int i=0; i<round; i++) {
                for(int j=0; j<5;j++) {
                    worldBoards[j]=worldBoards[j + 1];
                }
                worldBoards[5]=null;
            }
            pastTurnsMemorized -=round;                 //updating info about the buffer of the previous turns
            turnsCounter-=round;                        
        }

        //method stating what shall be done when main Window closed
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            Application.Current.Shutdown(); // close the application when red cross is pressed
        }

        //drawing basic setting of the board - filling the grid accordingly to the world size given in Window1
        public void drawBoard()
        {
            vBoard.Children.Clear();
            vBoard.RowDefinitions.Clear();
            vBoard.ColumnDefinitions.Clear();
            for (int i=0; i<worldSize; i++)
            {
                RowDefinition y = new RowDefinition();
                ColumnDefinition x = new ColumnDefinition();
                vBoard.ColumnDefinitions.Add(x);
                vBoard.RowDefinitions.Add(y);
            }
                this.vBoard.ShowGridLines = true;
        }

        //what shall be done when "Next Turn" button is pressed
        private void NowaTura_Click(object sender, RoutedEventArgs e)
        {
            if (automatic == false) // check if automatic execution is on - if not, then proceed with the method, else - button will be inactive
            {
                previousTurns = 0;
                if (worldBoards[0] == null)
                {
                    this.NowaGra_Click(sender, e);
                }
                else
                {
                    NextTurn();
                }
            }
        }


        //what shall be done when "New Turn" button is pressed
        private void NowaGra_Click(object sender, RoutedEventArgs e)
        {
            if (automatic == false) // check if automatic execution is on - if not, then proceed with the method, else - button will be inactive
            {
                Window1 startWindow = new Window1(this);
                startWindow.Show();
                this.Hide();
            }
        }

        //method called out when new game is started
        public void NewBoard()
        {
            boardGrid.Children.Clear();
            boardGrid.RowDefinitions.Clear();
            boardGrid.ColumnDefinitions.Clear();
            previousTurns = 0;
            logBox.Text = logInfo;
            boardGrid.Children.Clear();
            worldBoards[0] = new World(worldSize);
            drawBoard();
            worldBoards[0].PrintBoard();
            logInfo = "New game has been started!";
            logBox.Text = logInfo;
        }


        //what shall be done when "Previous Turn" button is pressed
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (automatic == false)
            {
                if (pastTurnsMemorized == 0)
                {
                    logInfo = "No turns in the buffer!\n";
                    logBox.Text = logInfo;
                }
                else if (pastTurnsMemorized <= maxNumberOfPastTurns)
                {
                    boardGrid.Children.Clear();
                    logInfo = "Previous turn number: -" + (previousTurns + 1);
                    logBox.Text = logInfo;
                    previousTurns++;
                    PreviousTurn(1);
                    worldBoards[0].PrintBoard();
                }
            }
        }


        //what shall be done when "Start/Stop Automatic Turns" button is pressed
        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if(worldBoards[0]==null)
            {
                NowaGra_Click(sender, e);
            }
            while (worldBoards[0] == null) { await Task.Delay(25); }


            if (automatic == true)
            {
                automatic = false;
                automaticButton.Background = buttonDefault;
                dispatcherTimer.Stop();
            }
            else
            {
                automatic = true;
                automaticButton.Background = new SolidColorBrush(Colors.Red);
            }

            if (automatic == true)
            {
                dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
                dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
                dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 500);
                dispatcherTimer.Start();
            }

        }

        //method responsible for calling the NextTurn() repeatedly
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (worldBoards[0] == null)
            {
                logInfo = "New game has been started!";
                previousTurns = 0;
                logBox.Text = logInfo;
                boardGrid.Children.Clear();
                worldBoards[0] = new World(worldSize);
                worldBoards[0].PrintBoard();
            }
            else
            {
                NextTurn();
            }
        }

        //calling the Legend Window
        private void Legend_Click(object sender, RoutedEventArgs e)
        {
            Window2 w2 = new Window2();
            w2.Show();
        }

        //serving the "save game" button
        private async void Button_Click_2(object sender, RoutedEventArgs e)
        {
            while (stable == false) //protection from saving in the middle of the turn
            {
                await Task.Delay(25);
            }
            if (automatic == false)
            {
                SerializeGame(fileName);
            }
        }

        //serving the "load game button"
        private async void Button_Click_3(object sender, RoutedEventArgs e)
        {
            while(stable==false)
            {
                await Task.Delay(25);
            }
            if (automatic == false)
            {
                DeserializeGame(fileName);
                drawBoard();
                worldBoards[0].PrintBoard();
            }
        }
    }

}
