

Tytuł projektu: Gra w życie

Autorzy: Mikołaj Król, Paweł Lipiński, Jacek Sudak



Temat:

Tematem projektu jest stworzenie prostego symulatora wirtualnego świata w postaci dwuwymiarowej tablicy o zadanych rozmiarach NxN. W świecie istnieją proste formy życia o odmiennym zachowaniu. Każdy z organizmów zajmuje dokładnie jedno pole w tablicy, na każdym polu może znajdować się co najwyżej jeden organizm (w przypadku kolizji jeden z nich usunięty lub przesunięty). Symulator ma turowy charakter. W każdej turze wszystkie organizmy istniejące na świecie wykonują akcję odpowiednią dla swojej klasy. Część z nich będzie się poruszała (zwierzęta), część będzie nieruchoma (rośliny). W przypadku kolizji (jeden z organizmów znajdzie się na tym samym polu, co inny) jeden z organizmów zwycięża, zabijając konkurenta. Kolejność ruchów organizmów w turze zależy od inicjatywy. Pierwsze ruszają się zwierzęta posiadające najwyższą inicjatywę. Jeśli dwa organizmy mają taką samą inicjatywę, o tym, który wykona akcję najpierw decyduje wiek organizmu (starszy wykona akcję pierwszy). Zwycięstwo przy spotkaniu zależy od siły organizmu. Przy równej sile zwycięża organizm, który zaatakował. Po uruchomieniu programu na planszy pojawia się po kilka sztuk zwierząt i roślin. W oknie programu wypisywane są informacje z wykonanej tury - kto zjadł kogo, jaki organizm uciekł przed walką itd. Dodatkowo każde pole na planszy ma określony rodzaj terenu reprezentowany przez klasę Area. w zależności od rodzaju terenu, statystyki oraz zachowania poszczególnych zwierząt mogą się zmieniać.
Dokładny opis poszczególnych organizmów i obszarów podany jest poniżej, przy opisie poszczególnych klas.



Opis klas i metod w nich zawartych :

Klasa MainWindow - główna klasa programu odpowiadająca za funkcjonowanie interfejsu i przetrzymująca obiekty klasy World, reprezentujące światy gry (aktualny i te z poprzednich tur)

        - private static int maxNumberOfPastTurns - ilość tur archiwalnych zachowywanych w pamięci
        - private static int pastTurnsMemorized - aktualna ilość zachowanych tur
        - private static int turnsCounter - licznik tur
        - private static World[] _worldBoards - tablica zawierająca wszystkie światy
        - public static Grid boardGrid - plansza
        - public static string logInfo - treść logów
        - public static int worldSize - rozmiar planszy
        - public static TextBox logBox - okienko logów
        - public static int previousTurns - ilość poprzednich tur
        - public static bool automatic - używany przy autoamtycznym wykonywaniu tur w odstępach czasowych
        - public static DispatcherTimer dispatcherTimer - używany przy autoamtycznym wykonywaniu tur w odstępach czasowych
        - public static List<Label> labels 
        - public static Brush buttonDefault - kolor przycisku, używany przy autoamtycznym wykonywaniu tur w odstępach czasowych
        - private string fileName - nazwa pliku zapisu
        - private static bool stable - używany do sprawdzenia czy poprzednie zadanie zostało wykonane przed rozpoczęciem kolejnego
	
	
	Metody i konstruktory:
	- public MainWindow() - konstruktor, tworzy nowe okno gry
	- public async static void SerializeGame(string fileName) - metoda używana do serializacji gry - zapisu do pliku binarnego. W kolejnych pętlach serializuje poszczególne światy World, listy z OrganismBoard i AreaBoard tych światów wraz z obiektami się na nich znajdującymi. Serializowane są też listy inicjatyw (np Init1Plants), w formie zapisania ilości organizmów znajdujących się na danej liście, a następnie zapisaniu ciągu lokalizacji tych organizmów.
	- public async static void DeserializeGame(string fileName) - metoda używana do deserializacji gry - odczytu z pliku binarnego. Działanie jest analogiczne do serializacji, w poszczególnych pętlach wczytywane są poszczególne elementy światów World i zawieranych przez nie Organismów i Area.
	- private static void NextTurn() - metoda wywołująca nową turę. Najpierw czyszczona jest plansza i okienko logów z poprzedniego stanu, następnie przepisywane są obiekty World w tablicy z archiwalnymi światami i generowana jest kopia aktualnego świata. Następnie wywoływana jest metoda ExecuteTurn().
	- private static void PreviousTurn(int round) - metoda wczytująca obiekt World z poprzedniej tury. Analogicznie do metody NextTurn() przepisywane są na sąsiadujące miejsca obiekty World w tablicy i na jej końcu dopisywany jest null.
	- protected override void OnClosed(EventArgs e) - metoda opisująca co musi zostać wykonane po zamknięciu głównego okna.
	- public void drawBoard() - metoda rysująca planszę. 
	- private void NowaTura_Click(object sender, RoutedEventArgs e) - metoda opisująca zachowanie programu po naciśnięciu przycisku Next Turn. Wywołuje metodę NextRurn() lub jeżeli gra nie jest zainicjowana to wywóluje metodę NowaGra_Click()
	- private void NowaGra_Click(object sender, RoutedEventArgs e) - metoda opisująca zachowanie programu po naciśnięciu przycisku New Game. Wywołuje powstnie Window1 startWindow i metodę startWindow.Show().
	- public void NewBoard() - metoda wywoływana przy tworzeniu nowej gry, odpowiedzialna za zainicjowanie nowego świata World i wywołanie metody drawBoard().
	- private void Button_Click(object sender, RoutedEventArgs e) - metoda opisująca zachowanie programu po naciśnięciu przycisku Previous Turn. Sprawdza czy istnieją zapisane poprzednie tury, jeżeli tak to wczytuje poprzednią turę wywołując metodę PreviousTurn().
	- private async void Button_Click_1(object sender, RoutedEventArgs e) - metoda opisująca zachowanie programu po naciśnięciu przycisku Start/Stop Automatic Turns.
	- private void dispatcherTimer_Tick(object sender, EventArgs e) - metoda odpowiedzialna za cykliczne wywoływanie metody NextTurn() w stałych odtępach czasu.
	- private void Legend_Click(object sender, RoutedEventArgs e) -  metoda opisująca zachowanie programu po naciśnięciu przycisku Legend. Wywołuje okno Window2 zawierające legende.
	- private async void Button_Click_2(object sender, RoutedEventArgs e) - metoda opisująca zachowanie programu po naciśnięciu przycisku Save Game. Wywoływuje metode SerailizeGame().
	- private async void Button_Click_3(object sender, RoutedEventArgs e) - metoda opisująca zachowanie programu po naciśnięciu przycisku Load Game. Wywoływuje metode DeserializeGame() i następnie rysuje nowowczytany świat.



Klasa World - podstawowa klasa programu zawierająca wszystkie informacje o aktualnym stanie gry. Składa się z:

	- int Size - rozmiar świata, tym samym boku planszy 
	- static int DeathCounterLimit - (domyślna wartość 4) - określa ile tur Organism może spędzić na niebezpiecznym dla siebie terenie - dokładny opis mechanizmu w klasie Area
	- static int MaxEvolvesPerTurn - (obliczony jako Size*Size/10) - określa ile maksymalnie obiektów Area może się zmienić na raz - dokładny opis mechanizmu w klasie Area
	- List<Organism> OrganismBoard - Lista tworząca "planszę" na której znajdują się obiekty klasy Organism. Dzięki niej można zlokalizować dany Organism w grze.
	- List<Area> AreaBoard - Lista analogiczna do powyższej, ale zawierająca obiekty klasy Area.
	- LinkedList<Organism> Init6Mice - Lista inicjatywy do której dodawane są Organismy o danej inicjatywie - w tym przypadku zwierzę Mouse o inicjatywie 6. Dzięki tej liście można określić kolejność 	wykonywania ruchów przez organizmy w grze. Nowe Organismy o danej inicjatywie dopisywane są do końca listy.
	- LinkedList<Organism> Init5Wolfs - Lista analogiczna do powyższej, zawierająca Organismy o inicjatywie 5 (Wolf).
	- LinkedList<Organism> Init4SheepAndThornEaters - j.w. inicjatywa 4 (Sheep i ThornEater)
	- LinkedList<Organism> Init3Snakes - j.w. inicjatywa 3 Snake)
	- LinkedList<Organism> Init1Plants- j.w. inicjatywa 1 (wszystkie klasy roślin - dziedziczące po Plant, np. Grass)
	- LinkedList<Organism> Init4SheepAndThornEaters - j.w. inicjatywa 4 (Sheep i ThornEater)
	- LinkedList<Organism> AddingList - lista używana do przetrzymywania świeżo utworzonych Organismów przed dodaniem ich do końca listy inicjatyw.
	- LinkedList<int> MaxDifferenceList - lista przetrzymująca lokalizację obiektów Area o najwyższej różnicy wpływów - dokładny opis mechanizmu w klasie Area.
	- int TurnsFromLastEvolve - określa ilość tur od ostatniej ewolucji obszarów Area - dokładny opis mechanizmu w klasie Area.
	- int TurnsFromGeneratingMaxDiffList- określa ilość tur od utworzenia listy MaxDifferenceList - dokładny opis mechanizmu w klasie Area.

	Metody i konstruktory:
	- public World(int size) - podstawowy konstrukor używany przy tworzeniu nowej gry, wywołuje metodę InitializeWorld()
	- public World(World oldWorld) - konstruktor kopiujący, tworzy dokładną kopię instancji klasy World, łącznie z utworzeniem kopii Organismów i Area. Iteruje po List<Organism> OrganismBoard tworząc kopię Organismów, następnie na podstawie list inicjatyw z kopiowanego obiektu World (np Init4SheepAndThornEaters) odtwarza listy nowotworzonego obiektu. Używany do tworzenia historii ostatnich tur.
	- public void ExecuteTurn() - metoda wykonująca jedną turę w grze. Wywołuje ona poszczególne metody składające się na przebieg tury. 
	- private void ExecuteActionForInitList(LinkedList<Organism> currentList) - metoda iterująca po danej liście inicjatyw currentList. Metoda sprawdza czy dany Organism nie zginął od czasu ostatnio wykonywanej akcji, jeżeli jest martwy (Alive==false) to usuwa go z listy, jeżeli jest żywy to wykonuję metodę Action() oraz UpdateDeathCounter(). W przypadku gdy zostanie stworzony nowy Organism w trakcie metody Action(), jest on dodawany do listy AddingList. Po przejściu całej listy currentList nowe Organismy znajdujące się na AddingList są dopisywane do końca currentList. Lista AddingList jest czyszczona na początku działania metody.
	- private void EvolveAreaboard() - metoda dzięki której modyfikowany jest teren na planszy (obiekty Area). Metoda sprawdza ile tur minęło od czasu ostatniej ewolucji terenu, jeżeli >2 to stara się wykonać ewolucję planszy. W tym celu najpierw sprawdza ile czasu minęło od ostatniego wygenerowania MaxDifferenceList, jeżeli >8 to wywołuję metodę GenerateMaxDifferenceList(). Następnie wykonuję ewolucje Area w ilości MaxEvolvesPerTurn. W tym celu losuję lokalizację obiektu Area z listy MaxDifferenceList i wykonuję na tym obiekcie metodę Evolve() (opis w klasie Area). Jeżeli zabraknie obiektów na liście MaxDifferenceList to jest ona generowana od nowa.  
	- private void GenerateMaxDifferenceList() - metoda generująca listę lokalizacji obiektów Area posiadających największą InfluenceDifference (opis w klasie Area). Najpierw iteruję po wszystkich obiektach Area znajdujących się w grze wywołując metodę InfluenceDifference w celu znalezienia maksymalnej wartości MaxInfluenceDifference następnie iteruje ponownie dodając takie obiekty do MaxDifferenceList.
	- private void UpdateStrengthModifiers() - metoda iterująca po planszy - OrganismBoard, wywołująca dla każdego Oganismu metodę UpdateStrength() (opis w klasie Organism)
	- private void CheckEnvironmentalDeaths() - metoda sprawdzająca dla każdego Organismu na planszy, czy któryś z Organismów nie przekroczył DeathCounterLimit, jeżeli tak się stało, Organism ginie (Alive = false)
	- private void InitializeWorld() - metoda inicjalizująca nowy obiekt klasy World, wykonuję ona poszczególne metody składające się na tworzenie nowego świata
	- private int GetPlantsAmount(int size) - metoda zwracająca ilość Organismów typu Plant jaką należy wygenerować w nowym świecie według wzoru toUpper(Size*Size/25)
	- private int GetAnimalsAmount(int size) - metoda zwracająca ilość Organismów typu Animal i Thorn jaką należy wygenerować w nowym świecie według wzoru toUpper(Size*Size/15)
	- private void InitializeOrganismBoard(int plantsAmount, int animalsAmount) - metoda tworząca nowe obiekty poszczególnych klas w nowym świecie. Dla danej klasy (Wolf, Mouse, Grass...) tworzy ona określoną przez GetPlantsAmount(int size) ilość obiektów i umieszcza je na planszy przy uzyciu AddToBoard, dodając je również do odpowiedniej listy inicjatyw (np. Init5Wolfs)
	- private void InitializeAreaBoard() - metoda generująca i wstawiające nowe obiekty Area na nowej plansze AreaBoard. Wykorzystywana podczas inicjalizacji nowej gry. Na początku plansza wypełniana jest obiektami Area o tymczasowym type = -1. Następnie w czterech losowych miejscach wstawiane są po jednym obiekcie każdego typu (opis typów w klasie Area). W momencie dodawania obiektu danego typu obiekty sąsiadujące typu -1 mają modyfikowaną tablicę Influence metodą AddInfluence() i dodawane są do listy InfluencedLocationsList[]. Istnieją cztery listy InfluencedLocationsList dla każdego typu Area. Po wygenerowaniu losowo wybranych punktów początkowych dla każdego typu następuje pętla wypełniająca całą resztę planszy. Dodawane jest po kolei po jedynm obiekcie danego typu. W pętli najpierw sprawdzane jest czy znajdują się obiekty na liście InfluencedLocationsList[], jeżeli tak to pobierany jest z niej losowy obiekt o maksymalnej wartości Influence poszukiwanego typu. Jeżeli nie to wybierana jest losowa wolna lokacja na AreaBoard (type = -1). Następnie w wybrane miejsce wstawiane jest obiekt aktualnie wstawianego typu. Wybrana lokacja jest usuwana z InfluencedLocationsList[] wszystkich typow. Nastepnie do InfluencedLocationsList[] danego typu dodawni sa wszyscy sasiedzi wybranej lokacji o typie -1. Petla powtarza sie az do wypelnienia planszy. Taki przebieg algorytmu ma wymusic grupowanie sie obszarow konkretnych typow w wieksze grupy, jednoczesnie dopuszczajac mozliwosc powstawania wiekszej ilosci mniejszych grupek, gdy poprzednia grupka podczas generowania nie ma sie gdzie "rozrastac"
	- public void PrintBoard() - metoda używana do rysowania planszy. Pętle w metodzie przechodzą po całej liście AreaBoard i OrganismBoard rysując odpowiednie tło pola w zależności od Area i odpowiedni Organism jeśli taki się na danym polu znajduję.
	- public int GenerateEmptyLocation() - metoda generująca dowolną pustą lokalizację na planszy. Generuje losowe lokazilacje dopóki nie trafi takiej która nie jest pusta.
	- public int GenerateNearbyEmptyLocation(int currentLocation) - metoda generująca losową lokację wokół danej lokalizacji currentLocation. Wywołuje metodę PotentialList(currentLocation), następnie z uzyskanej listy odrzuca te lokalizacje które są zajęte. Jeżeli odrzuci wszystkei lokalizacje z listy, to zwraca -1, jeżeli na liście coś zostanie, to zwraca losowy jej element.
	- public int GenerateNearbyLocation(int currentLocation) - metoda generująca dowolną lokalizację wokół zadanej lokalizacji currentLocation. Wywołuje metodę PotentialList(currentLocation), następnie zwraca losowy jej element.
	- private LinkedList<int> PotentialList(int currentLocation) - metoda generujaca liste lokacji sasiadujacych do lokacji currentLocation. Przy pomocy kilku warunkow brzegowych sprawdzajacych czy podana lokacja nie znajduje sie na skraju planszy zaweza ona ilosc generowanych wartosci. 
	- public void AddToBoard(int location, Organism organism) - metoda wstawiajaca w podane miejsce na planszy location podany organism.
	- public void NullToBoard(int location) - metoda wstawiajaca w podane miejsce na planszy location wartość null.
	- public World(SerializationInfo info, StreamingContext context) - konstruktor wykorzystywany przy deserializacji obiektu - wczytaniu gry z pliku binarnego.
	- public void GetObjectData(SerializationInfo info, StreamingContext context) - metoda wykorzystywana do serializacji obiektu.



Klasa Area - klasa opisująca rodzja pola znajdującego się w danym miejscu na planszy. Głównym parametrem jest wartość type określająca rodzaj pola (0 - Polana, 1 - Pustynia, 2 - Bagno, 3 - Las). Rodzaj obszaru znajdujący się w danym miejscu planszy ma duże znaczenie na zachowanie niektórych obiektów klasy Organism.

	- public int Type - określa typ danego obszaru Area
	- public int[] Influence - tablica zawierająca cztery wartości odpowiadające czterem rodzajom terenu, oznaczające jaki wpływ ma dany typ terenu na dany obiekt Area.
	- public List<int> MaxInfluenceType - Lista zawierająca typy terenów które mają najwyższy wpływ na obiekt.
	- public int InfluenceDifference - wartość różnicy między najwyższym wpływem z tablicy Influence[] i wartością wpływu typu własciwego danemu obiektowi Area.
	- private int MaxInfluenceValue - wartość maksymalnego wpływa z tablicy Influence[].

	Metody i konstruktory:
	- public Area(int type) - konstruktor podstawowy tworzący obiekt podanego typu.
	- public Area(Area area) - konstruktor kopiujący.
	- public Area GetCloned() - metoda zwracająca klon danego obiektu.
	- public void CheckInfuence(World world, LinkedList<int> potentialList) - metoda sprawdzająca wpływ sąsiadujących komórek, aktualizująca tablicę InfluenceList[]. Następnie odnajduje wartość maksymalną MaxInfluenceValue i porównując ją z wpływem typu macierzystego uzyskuje InfluenceDifference. Czyści i uzupełnia liste MaxInfluenceType.
	- public void AddInfluence(int type) - metoda inkrementujaca komorke w tablicy Influence[].
	- public void Evolve(Random generator) - metoda wywołująca zmianę typu danego obiektu i przemiane w typ odpowiadający losowemu elementowi z tablicy MaxInfluenceType.
	- public Area(SerializationInfo info, StreamingContext context) - konstruktor wykorzystywany przy deserializacji - wczytaniu gry z pliku binarnego. Odczytuje z pliku poszczegolne wartosci danego obiektu i na ich podstawie tworzy nowy obiekt Area.
	- public void GetObjectData(SerializationInfo info, StreamingContext context) - metoda wykorzystywana przy serializacji - zapisu gry do pliku binarnego. Analogiczne działanie do kostruktora powyżej.



Klasa Organism - klasa bazowa wszystkich organizmow znajdujacych sie w grze. Dziedzicza po niej wszystkie zwierzeta Animal i Plant. Określa ona podstawowe cechy i metody abstrakcyjne organizmow w grze. Implementuje tez kilka podstawowych metod wirtualnych.

	- public int Strength - określa siłe organizmu, podstawowy czynnik wpływający na rozstrzyganie walk między organizmami różnego typu
	- public int StrengthModifier - tymczasowy modyfikator siły zależny od terenu na którym znajduje się dany organizm. Domyślnie równy 0. Podczas walki dodawany jest do siły organizmu Strength.
	- public int Location - określa lokalizację obiektu na planszy.
	- public char Sign - określa znak jakim przestawiany jest obiekt na planszy.
	- public bool Alive - określa czy organizm jest żywy.
	- public int DeathCounter - licznik którego wartość rośnie gdy organizm przebywa na terenie dla siebie niebezpiecznym. Kiedy organizm zejdzie z pola niebezpiecznego DeathCounter wraca do wartości 0.

	Metody i konstruktory:
	- public abstract void Action(World world) - metoda wywoływana gdy dany obiekt ma wykonać swój ruch w danej turze. 
	- public abstract void Spawn(World world) - metoda wywoływana gdy dany obiekt podejmuje próbę rozmnożenia.
	- public abstract void UpdateStrength(Area area) - metoda aktualizująca StrengthModifier danego obiektu.
	- public abstract Organism GetCloned() - metoda zwracająca obiekt będący dokładną kopią obiektu. Jest nadpisywana w każdej klasie i zawsze polega jedynie na zwróceniu obiektu danej klasy za pomocą konstrkura kopiującego.
	- public virtual bool Collision(World world, Organism attackingOrganism) - metoda wirtualna wywoływana kiedy organizm zostanie zaatakowany. Zwraca false jeżeli organizm atakujący jest słabszy (ma niższą siłę) od atakowanego. Zwraca true jeżeli organizm atakujący jest silniejszy, jednocześnie zmienia wartość this.alive na false.
	- public virtual void UpdateDeathCounter(Area area) - metoda wirtualna aktualizująca DeathCounter. Jeżeli dany organizm znajduje się na polu gdzie Area.type = 1, to DeathCounter rośnie, jeżeli nie, to spada do 0.
	- public Organism(SerializationInfo info, StreamingContext context) - konstruktor wykorzystywany przy deserializacji - wczytaniu gry z pliku binarnego. Odczytuje z pliku poszczegolne wartosci danego obiektu i na ich podstawie tworzy nowy obiekt Organism.
	- public virtual void GetObjectData(SerializationInfo info, StreamingContext context) - metoda wykorzystywana przy serializacji - zapisu gry do pliku binarnego. Analogiczne działanie do kostruktora powyżej.



Klasa Plant - klasa bazowa dla wszystkich roślin, dziedzicząca po Organism. Wszystkie klasy dziedziczące po Plant mają inicjatywę. Nadpisuje jedynie metodę kolizji.

	- override public void Action(World world) - każda klasa dziedzicząca po Plant w swojej turze wykonuje jedynie próbe rozmnożenia Spawn().
	- override public bool Collision(World world, Organism attackingOrganism) - metoda kolizji dla większości roślin, zawsze zwraca true, a obiekt zaatakowany staje się martwy.



Klasa Grass - klasa rośliny dziedzicząca po Plant. Posiada siłę 0 i znak 'G'. Trawa nie posiada wielu specjajnych właściwości. Nadpisuję metodę Spawn(), dodatkowo nadpisuje jedynie metodę UpdateDeathCounter.

	- override public void Spawn(World world) - metoda spawn wywoływana jest w celu podjęcia próby rozprzestrzenienia się danego gatunku rośliny. Rośliny mają 50% szans na podjęcie próby rozmnożenia. Jeżeli próba zakończy się sukcesem to generowana jest lokalizacja nowej rośliny za pomocą metody GenerateNearbyEmptyLocation() i wstawiana jest w jej miejsce nowa roślina. Jeżeli nie ma wolnych lokalizacji wokół rośliny, to próba kończy się niepowodzeniem.
	- override public void UpdateDeathCounter(Area area) - metoda aktualizująca DeathCounter danego obiektu. W przypadku trawy DeathCounter rośnie także gdy area.Type == 3, czyli gdy trawa znajduje się w lesie.



Klasa Guarana - klasa rośliny dziedzicząca po Plant. Posiada siłę 0 i znak 'U'. Specjalną właściwością Guarany jest to że zwiększa siłę obiektu, który ją zaatakuje o 3 lub o 2 jeżeli Guarana znajduje się na Area.type==0. W związku z tym Guarana nadpisuje metodę Collision i metodę Spawn(). 

	- override public void Spawn(World world) - metoda spawn wywoływana jest w celu podjęcia próby rozprzestrzenienia się danego gatunku rośliny. Rośliny mają 50% szans na podjęcie próby rozmnożenia. Jeżeli próba zakończy się sukcesem to generowana jest lokalizacja nowej rośliny za pomocą metody GenerateNearbyEmptyLocation() i wstawiana jest w jej miejsce nowa roślina. Jeżeli nie ma wolnych lokalizacji wokół rośliny, to próba kończy się niepowodzeniem.
	- override public bool Collision(World world, Organism attackingOrganism) - metoda kolizji, zwraca true a obiekt zaatakowany staje się martwy. Dodatkowo zwiększa siłę organizmu atakującego o 3 lub o 2 jeżeli dana Guarana znajduje się na Area.type==0 (na polanie).



Klasa Thorn - klasa rośliny dziedzicząca po Plant. Posiada siłę 2 i znak 'T'. Specjalną właściwością Thorn jest to, że posiada siłę 2, oraz w metodzie Collision jeżeli zostanie zjedzony przez obiekt klasy ThornEater, to zwiększa jego siłe o 2. W związku z tym Thorn nadpisuje metodę Collision i metodę Spawn(). 

	- override public void Spawn(World world) - metoda spawn wywoływana jest w celu podjęcia próby rozprzestrzenienia się danego gatunku rośliny. Rośliny mają 50% szans na podjęcie próby rozmnożenia. Jeżeli próba zakończy się sukcesem to generowana jest lokalizacja nowej rośliny za pomocą metody GenerateNearbyEmptyLocation() i wstawiana jest w jej miejsce nowa roślina. Jeżeli nie ma wolnych lokalizacji wokół rośliny, to próba kończy się niepowodzeniem.
	- override public bool Collision(World world, Organism attackingOrganism) - metoda kolizji. Zwraca false jeżeli organizm atakujący jest słabszy (ma niższą siłę) od atakowanego. Zwraca true jeżeli organizm atakujący jest silniejszy, jednocześnie zmienia wartość this.alive na false. Dodatkowo jeżeli organizmem który zatakował jest ThornEater zwiększa jego siłę o 2.



Klasa Animal - klasa bazowa dla wszystkich zwierząt, dziedzicząca po Organism. Wszystkie klasy dziedziczące po Animal mają taką samą metodę Action(). Nadpisuje również metodę UpdateStrength().

        - override public void Action(World world) - metoda wywoływana gdy dany obiekt ma wykonać swój ruch w danej turze. W przypadku wszystkich zwierząt wygląda tak samo. Dane zwierzę stara się w swojej kolejce przejść na sąsiadujące pole generowane przez GenerateNearbyLocation(). Jeżeli wygenerowane pole jest puste to zwierzę na nie przechodzi. Jeżeli na wygenerowanym polu znajduje się obiekt tej samej klasy to wywoływana jest metoda Spawn(). Jeżeli znajduje się obiekt innej klasy, to rozpoczyna się walka między tymi obiektami poprzez wywołanie metody Collision obiektu zatakowanego. Jeżeli metoda Collision zwróci true, to organizm atakujący wygrał i przesuwa się na wygenerowane wcześniej pole. Jeżeli zwróci false, to przegrał, w efekcie ginie i jest usuwany z planszy.
	- override public void UpdateStrength(Area area) - metoda aktualizująca StrengthModifier danego obiektu. W przypadku większości zwierząt StrengthModifier zmniejsza modyfikator siły na polu Area.type == 1 (na pustyni).



Klasa Mouse - klasa zwierzęcia dziedzicząca po Animal. Posiada inicjatywę 6, siłę 1 i znak 'M'. Specjalną właściwością myszy jest to, że gdy znajduje się na Area.type=0 (na polanie), to ma podwójne szanse na potomstwo. Dodatkowo mysz w metodzie Collision potrafi uciekać jeśli przegra walkę, chyba że atakuje ją Snake, wtedy nie jest w stanie uciec. 

	- override public bool Collision(World world, Organism attackingOrganism) - metoda Collision różniąca się do standardowej tym, że gdy zaatakowana mysz przegra walkę, to stara się uciec na losowe puste wolne miejsce. Jeżeli nie ma akurat żadnego wolnego pola lub gdy organizmem atakującym jest Snake, to myszy nie udaje się uciec i ginie.
	- override public void Spawn(World world) - metoda wywoływana gdy zwierzę danego typu natrafi podczas swojej tury w metodzie Action() na organizm tego samego typu. W przypadku myszy, gdy mysz znajduje się na Area.type=0, to próba jest podejmowana dwukrotnie. 



Klasa Sheep - klasa zwierzęcia dziedzicząca po Animal. Posiada inicjatywę 4, siłę 4 i znak 'O'. Specjalną właściwością owcy jest to, że metoda UpdateStrength zmniejsza modyfikator siły zarówna na terenie Area.type == 1 jak i Area.type == 3 (na pustyni oraz w lesie)

	- override public void Spawn(World world) - metoda wywoływana gdy zwierzę danego typu natrafi podczas swojej tury w metodzie Action() na organizm tego samego typu. W przypadku Owcy nie ma w niej nic specjalnego.
	- override public void UpdateStrength(Area area) - metoda aktualizująca StrengthModifier danego obiektu. W przypadku owcy StrengthModifier zmniejsza modyfikator siły na polu Area.type == 1 oraz Area.type == 3 (na pustyni oraz w lesie).



Klasa Snake - klasa zwierzęcia dziedzicząca po Animal. Posiada inicjatywę 3, siłę 2 i znak 'S'. Specjalną właściwością węży jest to, że kiedy zostanie on zabity podczas metody Collision, to zatruwa on prganizm atakujący, w efekcie również organizm atakujący ginie. 

	- override public void Spawn(World world) - metoda wywoływana gdy zwierzę danego typu natrafi podczas swojej tury w metodzie Action() na organizm tego samego typu. W przypadku węży nie ma w niej nic specjalnego.
	- override public bool Collision(World world, Organism attackingOrganism) - metoda Collision różniąca się do standardowej tym, że gdy zaatakowany wąż przegra walkę, to zatruwa organizm go atakujący. W efekcie atakujący również ginie, metoda zawsze zwraca false.



Klasa ThornEater - klasa zwierzęcia dziedzicząca po Animal. Posiada inicjatywę 4, siłę 4 i znak 'E'. Specjalną właściwością cierniojada jest to, że gdy znajduje się na Area.type=3 (w lesie), to ma szanse na podwójne potomstwo. Dodatkowo gdy ThornEater zje Thorn, to zyskuje +2 siły (opis w klasie Thorn).

	- override public void Spawn(World world) - metoda wywoływana gdy zwierzę danego typu natrafi podczas swojej tury w metodzie Action() na organizm tego samego typu. W przypadku ciernożera, gdy cierniożer znajduje się na Area.type=3, to próba jest podejmowana dwukrotnie.



Klasa Wolf - klasa zwierzęcia dziedzicząca po Animal. Posiada inicjatywę 5, siłę 6 i znak 'W'. Wilk nie posiada specjalnych właściwości.

	- override public void Spawn(World world) - metoda wywoływana gdy zwierzę danego typu natrafi podczas swojej tury w metodzie Action() na organizm tego samego typu. W przypadku wilków nie ma w niej nic specjalnego.





Przybliżony podział zadań:

Jacek Sudak: Klasa World, Możliwość odtworzenia stanu gry w zakresie ostatnich 5 tur.

Paweł Lipiński: Klasa Main i WPF, klasa Organism, Animal, Plant i ich pochodne.

Mikołaj Król: Klasa Area, możliwość zapisu do pliku i wczytania z pliku stanu gry. 

