

Tytu³ projektu: Gra w ¿ycie

Autorzy: Miko³aj Król, Pawe³ Lipiñski, Jacek Sudak



Temat:

Tematem projektu jest stworzenie prostego symulatora wirtualnego œwiata w postaci dwuwymiarowej tablicy o zadanych rozmiarach NxN. W œwiecie istniej¹ proste formy ¿ycia o odmiennym zachowaniu. Ka¿dy z organizmów zajmuje dok³adnie jedno pole w tablicy, na ka¿dym polu mo¿e znajdowaæ siê co najwy¿ej jeden organizm (w przypadku kolizji jeden z nich usuniêty lub przesuniêty). Symulator ma turowy charakter. W ka¿dej turze wszystkie organizmy istniej¹ce na œwiecie wykonuj¹ akcjê odpowiedni¹ dla swojej klasy. Czêœæ z nich bêdzie siê porusza³a (zwierzêta), czêœæ bêdzie nieruchoma (roœliny). W przypadku kolizji (jeden z organizmów znajdzie siê na tym samym polu, co inny) jeden z organizmów zwyciê¿a, zabijaj¹c konkurenta. Kolejnoœæ ruchów organizmów w turze zale¿y od inicjatywy. Pierwsze ruszaj¹ siê zwierzêta posiadaj¹ce najwy¿sz¹ inicjatywê. Jeœli dwa organizmy maj¹ tak¹ sam¹ inicjatywê, o tym, który wykona akcjê najpierw decyduje wiek organizmu (starszy wykona akcjê pierwszy). Zwyciêstwo przy spotkaniu zale¿y od si³y organizmu. Przy równej sile zwyciê¿a organizm, który zaatakowa³. Po uruchomieniu programu na planszy pojawia siê po kilka sztuk zwierz¹t i roœlin. W oknie programu wypisywane s¹ informacje z wykonanej tury - kto zjad³ kogo, jaki organizm uciek³ przed walk¹ itd. Dodatkowo ka¿de pole na planszy ma okreœlony rodzaj terenu reprezentowany przez klasê Area. w zale¿noœci od rodzaju terenu, statystyki oraz zachowania poszczególnych zwierz¹t mog¹ siê zmieniaæ.
Dok³adny opis poszczególnych organizmów i obszarów podany jest poni¿ej, przy opisie poszczególnych klas.



Opis klas i metod w nich zawartych :

Klasa MainWindow - g³owna klasa programu odpowiadaj¹ca za funkcjonowanie interfejsu i przetrzymuj¹ca obiekty klasy World, reprezentuj¹ce œwiaty gry (aktualny i te z poprzednich tur)
        - private static int maxNumberOfPastTurns - iloœæ tur archiwalnych zachowywanych w pamiêci
        - private static int pastTurnsMemorized - aktualna iloœæ zachowanych tur
        - private static int turnsCounter - licznik tur
        - private static World[] _worldBoards - tablica zawieraj¹ca wszystkie œwiaty
        - public static Grid boardGrid - plansza
        - public static string logInfo - treœæ logów
        - public static int worldSize - rozmiar planszy
        - public static TextBox logBox - okienko logów
        - public static int previousTurns - iloœæ poprzednich tur
        - public static bool automatic - u¿ywany przy autoamtycznym wykonywaniu tur w odstêpach czasowych
        - public static DispatcherTimer dispatcherTimer - u¿ywany przy autoamtycznym wykonywaniu tur w odstêpach czasowych
        - public static List<Label> labels 
        - public static Brush buttonDefault - kolor przycisku, u¿ywany przy autoamtycznym wykonywaniu tur w odstêpach czasowych
        - private string fileName - nazwa pliku zapisu
        - private static bool stable - u¿ywany do sprawdzenia czy poprzednie zadanie zosta³o wykonane przed rozpoczêciem kolejnego

	Metody i konstruktory:
	- public MainWindow() - konstruktor, tworzy nowe okno gry
	- public async static void SerializeGame(string fileName) - metoda u¿ywana do serializacji gry - zapisu do pliku binarnego. W kolejnych pêtlach serializuje poszczególne œwiaty World, listy z OrganismBoard i AreaBoard tych œwiatów wraz z obiektami siê na nich znajduj¹cymi. Serializowane s¹ te¿ listy inicjatyw (np Init1Plants), w formie zapisania iloœci organizmów znajduj¹cych siê na danej liœcie, a nastêpnie zapisaniu ci¹gu lokalizacji tych organizmów.
	- public async static void DeserializeGame(string fileName) - metoda u¿ywana do deserializacji gry - odczytu z pliku binarnego. Dzia³anie jest analogiczne do serializacji, w poszczególnych pêtlach wczytywane s¹ poszczególne elementy œwiatów World i zawieranych przez nie Organismów i Area.
	- private static void NextTurn() - metoda wywo³uj¹ca now¹ turê. Najpierw czyszczona jest plansza i okienko logów z poprzedniego stanu, nastêpnie przepisywane s¹ obiekty World w tablicy z archiwalnymi œwiatami i generowana jest kopia aktualnego œwiata. Nastêpnie wywo³ywana jest metoda ExecuteTurn().
	- private static void PreviousTurn(int round) - metoda wczytuj¹ca obiekt World z poprzedniej tury. Analogicznie do metody NextTurn() przepisywane s¹ na s¹siaduj¹ce miejsca obiekty World w tablicy i na jej koñcu dopisywany jest null.
	- protected override void OnClosed(EventArgs e) - metoda opisuj¹ca co musi zostaæ wykonane po zamkniêciu g³ównego okna.
	- public void drawBoard() - metoda rysuj¹ca planszê. 
	- private void NowaTura_Click(object sender, RoutedEventArgs e) - metoda opisuj¹ca zachowanie programu po naciœniêciu przycisku Next Turn. Wywo³uje metodê NextRurn() lub je¿eli gra nie jest zainicjowana to wywóluje metodê NowaGra_Click()
	- private void NowaGra_Click(object sender, RoutedEventArgs e) - metoda opisuj¹ca zachowanie programu po naciœniêciu przycisku New Game. Wywo³uje powstnie Window1 startWindow i metodê startWindow.Show().
	- public void NewBoard() - metoda wywo³ywana przy tworzeniu nowej gry, odpowiedzialna za zainicjowanie nowego œwiata World i wywo³anie metody drawBoard().
	- private void Button_Click(object sender, RoutedEventArgs e) - metoda opisuj¹ca zachowanie programu po naciœniêciu przycisku Previous Turn. Sprawdza czy istniej¹ zapisane poprzednie tury, je¿eli tak to wczytuje poprzedni¹ turê wywo³uj¹c metodê PreviousTurn().
	- private async void Button_Click_1(object sender, RoutedEventArgs e) - metoda opisuj¹ca zachowanie programu po naciœniêciu przycisku Start/Stop Automatic Turns.
	- private void dispatcherTimer_Tick(object sender, EventArgs e) - metoda odpowiedzialna za cykliczne wywo³ywanie metody NextTurn() w sta³ych odtêpach czasu.
	- private void Legend_Click(object sender, RoutedEventArgs e) -  metoda opisuj¹ca zachowanie programu po naciœniêciu przycisku Legend. Wywo³uje okno Window2 zawieraj¹ce legende.
	- private async void Button_Click_2(object sender, RoutedEventArgs e) - metoda opisuj¹ca zachowanie programu po naciœniêciu przycisku Save Game. Wywo³ywuje metode SerailizeGame().
	- private async void Button_Click_3(object sender, RoutedEventArgs e) - metoda opisuj¹ca zachowanie programu po naciœniêciu przycisku Load Game. Wywo³ywuje metode DeserializeGame() i nastêpnie rysuje nowowczytany œwiat.



Klasa World - podstawowa klasa programu zawieraj¹ca wszystkie informacje o aktualnym stanie gry. Sk³ada siê z:
	- int Size - rozmiar œwiata, tym samym boku planszy 
	- static int DeathCounterLimit - (domyœlna wartoœæ 4) - okreœla ile tur Organism mo¿e spêdziæ na niebezpiecznym dla siebie terenie - dok³adny opis mechanizmu w klasie Area
	- static int MaxEvolvesPerTurn - (obliczony jako Size*Size/10) - okreœla ile maksymalnie obiektów Area mo¿e siê zmieniæ na raz - dok³adny opis mechanizmu w klasie Area
	- List<Organism> OrganismBoard - Lista tworz¹ca "planszê" na której znajduj¹ siê obiekty klasy Organism. Dziêki niej mo¿na zlokalizowaæ dany Organism w grze.
	- List<Area> AreaBoard - Lista analogiczna do powy¿szej, ale zawieraj¹ca obiekty klasy Area.
        - LinkedList<Organism> Init6Mice - Lista inicjatywy do której dodawane s¹ Organismy o danej inicjatywie - w tym przypadku zwierzê Mouse o inicjatywie 6. Dziêki tej liœcie mo¿na okreœliæ kolejnoœæ 	wykonywania ruchów przez organizmy w grze. Nowe Organismy o danej inicjatywie dopisywane s¹ do koñca listy.
        - LinkedList<Organism> Init5Wolfs - Lista analogiczna do powy¿szej, zawieraj¹ca Organismy o inicjatywie 5 (Wolf).
        - LinkedList<Organism> Init4SheepAndThornEaters - j.w. inicjatywa 4 (Sheep i ThornEater)
        - LinkedList<Organism> Init3Snakes - j.w. inicjatywa 3 Snake)
        - LinkedList<Organism> Init1Plants- j.w. inicjatywa 1 (wszystkie klasy roœlin - dziedzicz¹ce po Plant, np. Grass)
        - LinkedList<Organism> Init4SheepAndThornEaters - j.w. inicjatywa 4 (Sheep i ThornEater)
	- LinkedList<Organism> AddingList - lista u¿ywana do przetrzymywania œwie¿o utworzonych Organismów przed dodaniem ich do koñca listy inicjatyw.
	- LinkedList<int> MaxDifferenceList - lista przetrzymuj¹ca lokalizacjê obiektów Area o najwy¿szej ró¿nicy wp³ywów - dok³adny opis mechanizmu w klasie Area.
	- int TurnsFromLastEvolve - okreœla iloœæ tur od ostatniej ewolucji obszarów Area - dok³adny opis mechanizmu w klasie Area.
	- int TurnsFromGeneratingMaxDiffList- okreœla iloœæ tur od utworzenia listy MaxDifferenceList - dok³adny opis mechanizmu w klasie Area.

	Metody i konstruktory:
	- public World(int size) - podstawowy konstrukor u¿ywany przy tworzeniu nowej gry, wywo³uje metodê InitializeWorld()
	- public World(World oldWorld) - konstruktor kopiuj¹cy, tworzy dok³adn¹ kopiê instancji klasy World, ³¹cznie z utworzeniem kopii Organismów i Area. Iteruje po List<Organism> OrganismBoard tworz¹c kopiê Organismów, nastêpnie na podstawie list inicjatyw z kopiowanego obiektu World (np Init4SheepAndThornEaters) odtwarza listy nowotworzonego obiektu. U¿ywany do tworzenia historii ostatnich tur.
	- public void ExecuteTurn() - metoda wykonuj¹ca jedn¹ turê w grze. Wywo³uje ona poszczególne metody sk³adaj¹ce siê na przebieg tury. 
	- private void ExecuteActionForInitList(LinkedList<Organism> currentList) - metoda iteruj¹ca po danej liœcie inicjatyw currentList. Metoda sprawdza czy dany Organism nie zgin¹³ od czasu ostatnio wykonywanej akcji, je¿eli jest martwy (Alive==false) to usuwa go z listy, je¿eli jest ¿ywy to wykonujê metodê Action() oraz UpdateDeathCounter(). W przypadku gdy zostanie stworzony nowy Organism w trakcie metody Action(), jest on dodawany do listy AddingList. Po przejœciu ca³ej listy currentList nowe Organismy znajduj¹ce siê na AddingList s¹ dopisywane do koñca currentList. Lista AddingList jest czyszczona na pocz¹tku dzia³ania metody.
	- private void EvolveAreaboard() - metoda dziêki której modyfikowany jest teren na planszy (obiekty Area). Metoda sprawdza ile tur minê³o od czasu ostatniej ewolucji terenu, je¿eli >2 to stara siê wykonaæ ewolucjê planszy. W tym celu najpierw sprawdza ile czasu minê³o od ostatniego wygenerowania MaxDifferenceList, je¿eli >8 to wywo³ujê metodê GenerateMaxDifferenceList(). Nastêpnie wykonujê ewolucje Area w iloœci MaxEvolvesPerTurn. W tym celu losujê lokalizacjê obiektu Area z listy MaxDifferenceList i wykonujê na tym obiekcie metodê Evolve() (opis w klasie Area). Je¿eli zabraknie obiektów na liœcie MaxDifferenceList to jest ona generowana od nowa.  
	- private void GenerateMaxDifferenceList() - metoda generuj¹ca listê lokalizacji obiektów Area posiadaj¹cych najwiêksz¹ InfluenceDifference (opis w klasie Area). Najpierw iterujê po wszystkich obiektach Area znajduj¹cych siê w grze wywo³uj¹c metodê InfluenceDifference w celu znalezienia maksymalnej wartoœci MaxInfluenceDifference nastêpnie iteruje ponownie dodaj¹c takie obiekty do MaxDifferenceList.
	- private void UpdateStrengthModifiers() - metoda iteruj¹ca po planszy - OrganismBoard, wywo³uj¹ca dla ka¿dego Oganismu metodê UpdateStrength() (opis w klasie Organism)
	- private void CheckEnvironmentalDeaths() - metoda sprawdzaj¹ca dla ka¿dego Organismu na planszy, czy któryœ z Organismów nie przekroczy³ DeathCounterLimit, je¿eli tak siê sta³o, Organism ginie (Alive = false)
	- private void InitializeWorld() - metoda inicjalizuj¹ca nowy obiekt klasy World, wykonujê ona poszczególne metody sk³adaj¹ce siê na tworzenie nowego œwiata
	- private int GetPlantsAmount(int size) - metoda zwracaj¹ca iloœæ Organismów typu Plant jak¹ nale¿y wygenerowaæ w nowym œwiecie wed³ug wzoru toUpper(Size*Size/25)
	- private int GetAnimalsAmount(int size) - metoda zwracaj¹ca iloœæ Organismów typu Animal i Thorn jak¹ nale¿y wygenerowaæ w nowym œwiecie wed³ug wzoru toUpper(Size*Size/15)
	- private void InitializeOrganismBoard(int plantsAmount, int animalsAmount) - metoda tworz¹ca nowe obiekty poszczególnych klas w nowym œwiecie. Dla danej klasy (Wolf, Mouse, Grass...) tworzy ona okreœlon¹ przez GetPlantsAmount(int size) iloœæ obiektów i umieszcza je na planszy przy uzyciu AddToBoard, dodaj¹c je równie¿ do odpowiedniej listy inicjatyw (np. Init5Wolfs)
	- private void InitializeAreaBoard() - metoda generuj¹ca i wstawiaj¹ce nowe obiekty Area na nowej plansze AreaBoard. Wykorzystywana podczas inicjalizacji nowej gry. Na pocz¹tku plansza wype³niana jest obiektami Area o tymczasowym type = -1. Nastêpnie w czterech losowych miejscach wstawiane s¹ po jednym obiekcie ka¿dego typu (opis typów w klasie Area). W momencie dodawania obiektu danego typu obiekty s¹siaduj¹ce typu -1 maj¹ modyfikowan¹ tablicê Influence metod¹ AddInfluence() i dodawane s¹ do listy InfluencedLocationsList[]. Istniej¹ cztery listy InfluencedLocationsList dla ka¿dego typu Area. Po wygenerowaniu losowo wybranych punktów pocz¹tkowych dla ka¿dego typu nastêpuje pêtla wype³niaj¹ca ca³¹ resztê planszy. Dodawane jest po kolei po jedynm obiekcie danego typu. W pêtli najpierw sprawdzane jest czy znajduj¹ siê obiekty na liœcie InfluencedLocationsList[], je¿eli tak to pobierany jest z niej losowy obiekt o maksymalnej wartoœci Influence poszukiwanego typu. Je¿eli nie to wybierana jest losowa wolna lokacja na AreaBoard (type = -1). Nastêpnie w wybrane miejsce wstawiane jest obiekt aktualnie wstawianego typu. Wybrana lokacja jest usuwana z InfluencedLocationsList[] wszystkich typow. Nastepnie do InfluencedLocationsList[] danego typu dodawni sa wszyscy sasiedzi wybranej lokacji o typie -1. Petla powtarza sie az do wypelnienia planszy. Taki przebieg algorytmu ma wymusic grupowanie sie obszarow konkretnych typow w wieksze grupy, jednoczesnie dopuszczajac mozliwosc powstawania wiekszej ilosci mniejszych grupek, gdy poprzednia grupka podczas generowania nie ma sie gdzie "rozrastac"
	- public void PrintBoard() - metoda u¿ywana do rysowania planszy. Pêtle w metodzie przechodz¹ po ca³ej liœcie AreaBoard i OrganismBoard rysuj¹c odpowiednie t³o pola w zale¿noœci od Area i odpowiedni Organism jeœli taki siê na danym polu znajdujê.
	- public int GenerateEmptyLocation() - metoda generuj¹ca dowoln¹ pust¹ lokalizacjê na planszy. Generuje losowe lokazilacje dopóki nie trafi takiej która nie jest pusta.
	- public int GenerateNearbyEmptyLocation(int currentLocation) - metoda generuj¹ca losow¹ lokacjê wokó³ danej lokalizacji currentLocation. Wywo³uje metodê PotentialList(currentLocation), nastêpnie z uzyskanej listy odrzuca te lokalizacje które s¹ zajête. Je¿eli odrzuci wszystkei lokalizacje z listy, to zwraca -1, je¿eli na liœcie coœ zostanie, to zwraca losowy jej element.
	- public int GenerateNearbyLocation(int currentLocation) - metoda generuj¹ca dowoln¹ lokalizacjê wokó³ zadanej lokalizacji currentLocation. Wywo³uje metodê PotentialList(currentLocation), nastêpnie zwraca losowy jej element.
	- private LinkedList<int> PotentialList(int currentLocation) - metoda generujaca liste lokacji sasiadujacych do lokacji currentLocation. Przy pomocy kilku warunkow brzegowych sprawdzajacych czy podana lokacja nie znajduje sie na skraju planszy zaweza ona ilosc generowanych wartosci. 
	- public void AddToBoard(int location, Organism organism) - metoda wstawiajaca w podane miejsce na planszy location podany organism.
	- public void NullToBoard(int location) - metoda wstawiajaca w podane miejsce na planszy location wartoœæ null.
	- public World(SerializationInfo info, StreamingContext context) - konstruktor wykorzystywany przy deserializacji obiektu - wczytaniu gry z pliku binarnego.
	- public void GetObjectData(SerializationInfo info, StreamingContext context) - metoda wykorzystywana do serializacji obiektu.



Klasa Area - klasa opisuj¹ca rodzja pola znajduj¹cego siê w danym miejscu na planszy. G³ównym parametrem jest wartoœæ type okreœlaj¹ca rodzaj pola (0 - Polana, 1 - Pustynia, 2 - Bagno, 3 - Las). Rodzaj obszaru znajduj¹cy siê w danym miejscu planszy ma du¿e znaczenie na zachowanie niektórych obiektów klasy Organism.
	- public int Type - okreœla typ danego obszaru Area
	- public int[] Influence - tablica zawieraj¹ca cztery wartoœci odpowiadaj¹ce czterem rodzajom terenu, oznaczaj¹ce jaki wp³yw ma dany typ terenu na dany obiekt Area.
	- public List<int> MaxInfluenceType - Lista zawieraj¹ca typy terenów które maj¹ najwy¿szy wp³yw na obiekt.
	- public int InfluenceDifference - wartoœæ ró¿nicy miêdzy najwy¿szym wp³ywem z tablicy Influence[] i wartoœci¹ wp³ywu typu w³asciwego danemu obiektowi Area.
	- private int MaxInfluenceValue - wartoœæ maksymalnego wp³ywa z tablicy Influence[].

	Metody i konstruktory:
	- public Area(int type) - konstruktor podstawowy tworz¹cy obiekt podanego typu.
	- public Area(Area area) - konstruktor kopiuj¹cy.
	- public Area GetCloned() - metoda zwracaj¹ca klon danego obiektu.
	- public void CheckInfuence(World world, LinkedList<int> potentialList) - metoda sprawdzaj¹ca wp³yw s¹siaduj¹cych komórek, aktualizuj¹ca tablicê InfluenceList[]. Nastêpnie odnajduje wartoœæ maksymaln¹ MaxInfluenceValue i porównuj¹c j¹ z wp³ywem typu macierzystego uzyskuje InfluenceDifference. Czyœci i uzupe³nia liste MaxInfluenceType.
	- public void AddInfluence(int type) - metoda inkrementujaca komorke w tablicy Influence[].
	- public void Evolve(Random generator) - metoda wywo³uj¹ca zmianê typu danego obiektu i przemiane w typ odpowiadaj¹cy losowemu elementowi z tablicy MaxInfluenceType.
	- public Area(SerializationInfo info, StreamingContext context) - konstruktor wykorzystywany przy deserializacji - wczytaniu gry z pliku binarnego. Odczytuje z pliku poszczegolne wartosci danego obiektu i na ich podstawie tworzy nowy obiekt Area.
	- public void GetObjectData(SerializationInfo info, StreamingContext context) - metoda wykorzystywana przy serializacji - zapisu gry do pliku binarnego. Analogiczne dzia³anie do kostruktora powy¿ej.



Klasa Organism - klasa bazowa wszystkich organizmow znajdujacych sie w grze. Dziedzicza po niej wszystkie zwierzeta Animal i Plant. Okreœla ona podstawowe cechy i metody abstrakcyjne organizmow w grze. Implementuje tez kilka podstawowych metod wirtualnych.
	- public int Strength - okreœla si³e organizmu, podstawowy czynnik wp³ywaj¹cy na rozstrzyganie walk miêdzy organizmami ró¿nego typu
	- public int StrengthModifier - tymczasowy modyfikator si³y zale¿ny od terenu na którym znajduje siê dany organizm. Domyœlnie równy 0. Podczas walki dodawany jest do si³y organizmu Strength.
	- public int Location - okreœla lokalizacjê obiektu na planszy.
	- public char Sign - okreœla znak jakim przestawiany jest obiekt na planszy.
	- public bool Alive - okreœla czy organizm jest ¿ywy.
	- public int DeathCounter - licznik którego wartoœæ roœnie gdy organizm przebywa na terenie dla siebie niebezpiecznym. Kiedy organizm zejdzie z pola niebezpiecznego DeathCounter wraca do wartoœci 0.

	Metody i konstruktory:
	- public abstract void Action(World world) - metoda wywo³ywana gdy dany obiekt ma wykonaæ swój ruch w danej turze. 
	- public abstract void Spawn(World world) - metoda wywo³ywana gdy dany obiekt podejmuje próbê rozmno¿enia.
	- public abstract void UpdateStrength(Area area) - metoda aktualizuj¹ca StrengthModifier danego obiektu.
	- public abstract Organism GetCloned() - metoda zwracaj¹ca obiekt bêd¹cy dok³adn¹ kopi¹ obiektu. Jest nadpisywana w ka¿dej klasie i zawsze polega jedynie na zwróceniu obiektu danej klasy za pomoc¹ konstrkura kopiuj¹cego.
	- public virtual bool Collision(World world, Organism attackingOrganism) - metoda wirtualna wywo³ywana kiedy organizm zostanie zaatakowany. Zwraca false je¿eli organizm atakuj¹cy jest s³abszy (ma ni¿sz¹ si³ê) od atakowanego. Zwraca true je¿eli organizm atakuj¹cy jest silniejszy, jednoczeœnie zmienia wartoœæ this.alive na false.
	- public virtual void UpdateDeathCounter(Area area) - metoda wirtualna aktualizuj¹ca DeathCounter. Je¿eli dany organizm znajduje siê na polu gdzie Area.type = 1, to DeathCounter roœnie, je¿eli nie, to spada do 0.
	- public Organism(SerializationInfo info, StreamingContext context) - konstruktor wykorzystywany przy deserializacji - wczytaniu gry z pliku binarnego. Odczytuje z pliku poszczegolne wartosci danego obiektu i na ich podstawie tworzy nowy obiekt Organism.
	- public virtual void GetObjectData(SerializationInfo info, StreamingContext context) - metoda wykorzystywana przy serializacji - zapisu gry do pliku binarnego. Analogiczne dzia³anie do kostruktora powy¿ej.



Klasa Plant - klasa bazowa dla wszystkich roœlin, dziedzicz¹ca po Organism. Wszystkie klasy dziedzicz¹ce po Plant maj¹ inicjatywê. Nadpisuje jedynie metodê kolizji.
        - override public void Action(World world) - ka¿da klasa dziedzicz¹ca po Plant w swojej turze wykonuje jedynie próbe rozmno¿enia Spawn().
	- override public bool Collision(World world, Organism attackingOrganism) - metoda kolizji dla wiêkszoœci roœlin, zawsze zwraca true, a obiekt zaatakowany staje siê martwy.



Klasa Grass - klasa roœliny dziedzicz¹ca po Plant. Posiada si³ê 0 i znak 'G'. Trawa nie posiada wielu specjajnych w³aœciwoœci. Nadpisujê metodê Spawn(), dodatkowo nadpisuje jedynie metodê UpdateDeathCounter.
	- override public void Spawn(World world) - metoda spawn wywo³ywana jest w celu podjêcia próby rozprzestrzenienia siê danego gatunku roœliny. Roœliny maj¹ 50% szans na podjêcie próby rozmno¿enia. Je¿eli próba zakoñczy siê sukcesem to generowana jest lokalizacja nowej roœliny za pomoc¹ metody GenerateNearbyEmptyLocation() i wstawiana jest w jej miejsce nowa roœlina. Je¿eli nie ma wolnych lokalizacji wokó³ roœliny, to próba koñczy siê niepowodzeniem.
	- override public void UpdateDeathCounter(Area area) - metoda aktualizuj¹ca DeathCounter danego obiektu. W przypadku trawy DeathCounter roœnie tak¿e gdy area.Type == 3, czyli gdy trawa znajduje siê w lesie.



Klasa Guarana - klasa roœliny dziedzicz¹ca po Plant. Posiada si³ê 0 i znak 'U'. Specjaln¹ w³aœciwoœci¹ Guarany jest to ¿e zwiêksza si³ê obiektu, który j¹ zaatakuje o 3 lub o 2 je¿eli Guarana znajduje siê na Area.type==0. W zwi¹zku z tym Guarana nadpisuje metodê Collision i metodê Spawn(). 
	- override public void Spawn(World world) - metoda spawn wywo³ywana jest w celu podjêcia próby rozprzestrzenienia siê danego gatunku roœliny. Roœliny maj¹ 50% szans na podjêcie próby rozmno¿enia. Je¿eli próba zakoñczy siê sukcesem to generowana jest lokalizacja nowej roœliny za pomoc¹ metody GenerateNearbyEmptyLocation() i wstawiana jest w jej miejsce nowa roœlina. Je¿eli nie ma wolnych lokalizacji wokó³ roœliny, to próba koñczy siê niepowodzeniem.
	- override public bool Collision(World world, Organism attackingOrganism) - metoda kolizji, zwraca true a obiekt zaatakowany staje siê martwy. Dodatkowo zwiêksza si³ê organizmu atakuj¹cego o 3 lub o 2 je¿eli dana Guarana znajduje siê na Area.type==0 (na polanie).



Klasa Thorn - klasa roœliny dziedzicz¹ca po Plant. Posiada si³ê 2 i znak 'T'. Specjaln¹ w³aœciwoœci¹ Thorn jest to, ¿e posiada si³ê 2, oraz w metodzie Collision je¿eli zostanie zjedzony przez obiekt klasy ThornEater, to zwiêksza jego si³e o 2. W zwi¹zku z tym Thorn nadpisuje metodê Collision i metodê Spawn(). 
	- override public void Spawn(World world) - metoda spawn wywo³ywana jest w celu podjêcia próby rozprzestrzenienia siê danego gatunku roœliny. Roœliny maj¹ 50% szans na podjêcie próby rozmno¿enia. Je¿eli próba zakoñczy siê sukcesem to generowana jest lokalizacja nowej roœliny za pomoc¹ metody GenerateNearbyEmptyLocation() i wstawiana jest w jej miejsce nowa roœlina. Je¿eli nie ma wolnych lokalizacji wokó³ roœliny, to próba koñczy siê niepowodzeniem.
	- override public bool Collision(World world, Organism attackingOrganism) - metoda kolizji. Zwraca false je¿eli organizm atakuj¹cy jest s³abszy (ma ni¿sz¹ si³ê) od atakowanego. Zwraca true je¿eli organizm atakuj¹cy jest silniejszy, jednoczeœnie zmienia wartoœæ this.alive na false. Dodatkowo je¿eli organizmem który zatakowa³ jest ThornEater zwiêksza jego si³ê o 2.



Klasa Animal - klasa bazowa dla wszystkich zwierz¹t, dziedzicz¹ca po Organism. Wszystkie klasy dziedzicz¹ce po Animal maj¹ tak¹ sam¹ metodê Action(). Nadpisuje równie¿ metodê UpdateStrength().
        - override public void Action(World world) - metoda wywo³ywana gdy dany obiekt ma wykonaæ swój ruch w danej turze. W przypadku wszystkich zwierz¹t wygl¹da tak samo. Dane zwierzê stara siê w swojej kolejce przejœæ na s¹siaduj¹ce pole generowane przez GenerateNearbyLocation(). Je¿eli wygenerowane pole jest puste to zwierzê na nie przechodzi. Je¿eli na wygenerowanym polu znajduje siê obiekt tej samej klasy to wywo³ywana jest metoda Spawn(). Je¿eli znajduje siê obiekt innej klasy, to rozpoczyna siê walka miêdzy tymi obiektami poprzez wywo³anie metody Collision obiektu zatakowanego. Je¿eli metoda Collision zwróci true, to organizm atakuj¹cy wygra³ i przesuwa siê na wygenerowane wczeœniej pole. Je¿eli zwróci false, to przegra³, w efekcie ginie i jest usuwany z planszy.
	- override public void UpdateStrength(Area area) - metoda aktualizuj¹ca StrengthModifier danego obiektu. W przypadku wiêkszoœci zwierz¹t StrengthModifier zmniejsza modyfikator si³y na polu Area.type == 1 (na pustyni).



Klasa Mouse - klasa zwierzêcia dziedzicz¹ca po Animal. Posiada inicjatywê 6, si³ê 1 i znak 'M'. Specjaln¹ w³aœciwoœci¹ myszy jest to, ¿e gdy znajduje siê na Area.type=0 (na polanie), to ma podwójne szanse na potomstwo. Dodatkowo mysz w metodzie Collision potrafi uciekaæ jeœli przegra walkê, chyba ¿e atakuje j¹ Snake, wtedy nie jest w stanie uciec. 
	- override public bool Collision(World world, Organism attackingOrganism) - metoda Collision ró¿ni¹ca siê do standardowej tym, ¿e gdy zaatakowana mysz przegra walkê, to stara siê uciec na losowe puste wolne miejsce. Je¿eli nie ma akurat ¿adnego wolnego pola lub gdy organizmem atakuj¹cym jest Snake, to myszy nie udaje siê uciec i ginie.
	- override public void Spawn(World world) - metoda wywo³ywana gdy zwierzê danego typu natrafi podczas swojej tury w metodzie Action() na organizm tego samego typu. W przypadku myszy, gdy mysz znajduje siê na Area.type=0, to próba jest podejmowana dwukrotnie. 



Klasa Sheep - klasa zwierzêcia dziedzicz¹ca po Animal. Posiada inicjatywê 4, si³ê 4 i znak 'O'. Specjaln¹ w³aœciwoœci¹ owcy jest to, ¿e metoda UpdateStrength zmniejsza modyfikator si³y zarówna na terenie Area.type == 1 jak i Area.type == 3 (na pustyni oraz w lesie)
	- override public void Spawn(World world) - metoda wywo³ywana gdy zwierzê danego typu natrafi podczas swojej tury w metodzie Action() na organizm tego samego typu. W przypadku Owcy nie ma w niej nic specjalnego.
	- override public void UpdateStrength(Area area) - metoda aktualizuj¹ca StrengthModifier danego obiektu. W przypadku owcy StrengthModifier zmniejsza modyfikator si³y na polu Area.type == 1 oraz Area.type == 3 (na pustyni oraz w lesie).



Klasa Snake - klasa zwierzêcia dziedzicz¹ca po Animal. Posiada inicjatywê 3, si³ê 2 i znak 'S'. Specjaln¹ w³aœciwoœci¹ wê¿y jest to, ¿e kiedy zostanie on zabity podczas metody Collision, to zatruwa on prganizm atakuj¹cy, w efekcie równie¿ organizm atakuj¹cy ginie. 
	- override public void Spawn(World world) - metoda wywo³ywana gdy zwierzê danego typu natrafi podczas swojej tury w metodzie Action() na organizm tego samego typu. W przypadku wê¿y nie ma w niej nic specjalnego.
	- override public bool Collision(World world, Organism attackingOrganism) - metoda Collision ró¿ni¹ca siê do standardowej tym, ¿e gdy zaatakowany w¹¿ przegra walkê, to zatruwa organizm go atakuj¹cy. W efekcie atakuj¹cy równie¿ ginie, metoda zawsze zwraca false.



Klasa ThornEater - klasa zwierzêcia dziedzicz¹ca po Animal. Posiada inicjatywê 4, si³ê 4 i znak 'E'. Specjaln¹ w³aœciwoœci¹ cierniojada jest to, ¿e gdy znajduje siê na Area.type=3 (w lesie), to ma szanse na podwójne potomstwo. Dodatkowo gdy ThornEater zje Thorn, to zyskuje +2 si³y (opis w klasie Thorn).
	- override public void Spawn(World world) - metoda wywo³ywana gdy zwierzê danego typu natrafi podczas swojej tury w metodzie Action() na organizm tego samego typu. W przypadku cierno¿era, gdy ciernio¿er znajduje siê na Area.type=3, to próba jest podejmowana dwukrotnie.



Klasa Wolf - klasa zwierzêcia dziedzicz¹ca po Animal. Posiada inicjatywê 5, si³ê 6 i znak 'W'. Wilk nie posiada specjalnych w³aœciwoœci.
	- override public void Spawn(World world) - metoda wywo³ywana gdy zwierzê danego typu natrafi podczas swojej tury w metodzie Action() na organizm tego samego typu. W przypadku wilków nie ma w niej nic specjalnego.





Przybli¿ony podzia³ zadañ:

Jacek Sudak: klasa World, Mo¿liwoœæ odtworzenia stanu gry w zakresie ostatnich 5 tur.

Pawe³ Lipiñski: Klasa Main i WPF, klasa Organism, Animal, Plant i ich pochodne.

Miko³aj Król: Klasa Area, mo¿liwoœæ zapisu do pliku i wczytania z pliku stanu gry. 

