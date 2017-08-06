

Tytu� projektu: Gra w �ycie

Autorzy: Miko�aj Kr�l, Pawe� Lipi�ski, Jacek Sudak



Temat:

Tematem projektu jest stworzenie prostego symulatora wirtualnego �wiata w postaci dwuwymiarowej tablicy o zadanych rozmiarach NxN. W �wiecie istniej� proste formy �ycia o odmiennym zachowaniu. Ka�dy z organizm�w zajmuje dok�adnie jedno pole w tablicy, na ka�dym polu mo�e znajdowa� si� co najwy�ej jeden organizm (w przypadku kolizji jeden z nich usuni�ty lub przesuni�ty). Symulator ma turowy charakter. W ka�dej turze wszystkie organizmy istniej�ce na �wiecie wykonuj� akcj� odpowiedni� dla swojej klasy. Cz�� z nich b�dzie si� porusza�a (zwierz�ta), cz�� b�dzie nieruchoma (ro�liny). W przypadku kolizji (jeden z organizm�w znajdzie si� na tym samym polu, co inny) jeden z organizm�w zwyci�a, zabijaj�c konkurenta. Kolejno�� ruch�w organizm�w w turze zale�y od inicjatywy. Pierwsze ruszaj� si� zwierz�ta posiadaj�ce najwy�sz� inicjatyw�. Je�li dwa organizmy maj� tak� sam� inicjatyw�, o tym, kt�ry wykona akcj� najpierw decyduje wiek organizmu (starszy wykona akcj� pierwszy). Zwyci�stwo przy spotkaniu zale�y od si�y organizmu. Przy r�wnej sile zwyci�a organizm, kt�ry zaatakowa�. Po uruchomieniu programu na planszy pojawia si� po kilka sztuk zwierz�t i ro�lin. W oknie programu wypisywane s� informacje z wykonanej tury - kto zjad� kogo, jaki organizm uciek� przed walk� itd. Dodatkowo ka�de pole na planszy ma okre�lony rodzaj terenu reprezentowany przez klas� Area. w zale�no�ci od rodzaju terenu, statystyki oraz zachowania poszczeg�lnych zwierz�t mog� si� zmienia�.
Dok�adny opis poszczeg�lnych organizm�w i obszar�w podany jest poni�ej, przy opisie poszczeg�lnych klas.



Opis klas i metod w nich zawartych :

Klasa MainWindow - g�owna klasa programu odpowiadaj�ca za funkcjonowanie interfejsu i przetrzymuj�ca obiekty klasy World, reprezentuj�ce �wiaty gry (aktualny i te z poprzednich tur)
        - private static int maxNumberOfPastTurns - ilo�� tur archiwalnych zachowywanych w pami�ci
        - private static int pastTurnsMemorized - aktualna ilo�� zachowanych tur
        - private static int turnsCounter - licznik tur
        - private static World[] _worldBoards - tablica zawieraj�ca wszystkie �wiaty
        - public static Grid boardGrid - plansza
        - public static string logInfo - tre�� log�w
        - public static int worldSize - rozmiar planszy
        - public static TextBox logBox - okienko log�w
        - public static int previousTurns - ilo�� poprzednich tur
        - public static bool automatic - u�ywany przy autoamtycznym wykonywaniu tur w odst�pach czasowych
        - public static DispatcherTimer dispatcherTimer - u�ywany przy autoamtycznym wykonywaniu tur w odst�pach czasowych
        - public static List<Label> labels 
        - public static Brush buttonDefault - kolor przycisku, u�ywany przy autoamtycznym wykonywaniu tur w odst�pach czasowych
        - private string fileName - nazwa pliku zapisu
        - private static bool stable - u�ywany do sprawdzenia czy poprzednie zadanie zosta�o wykonane przed rozpocz�ciem kolejnego

	Metody i konstruktory:
	- public MainWindow() - konstruktor, tworzy nowe okno gry
	- public async static void SerializeGame(string fileName) - metoda u�ywana do serializacji gry - zapisu do pliku binarnego. W kolejnych p�tlach serializuje poszczeg�lne �wiaty World, listy z OrganismBoard i AreaBoard tych �wiat�w wraz z obiektami si� na nich znajduj�cymi. Serializowane s� te� listy inicjatyw (np Init1Plants), w formie zapisania ilo�ci organizm�w znajduj�cych si� na danej li�cie, a nast�pnie zapisaniu ci�gu lokalizacji tych organizm�w.
	- public async static void DeserializeGame(string fileName) - metoda u�ywana do deserializacji gry - odczytu z pliku binarnego. Dzia�anie jest analogiczne do serializacji, w poszczeg�lnych p�tlach wczytywane s� poszczeg�lne elementy �wiat�w World i zawieranych przez nie Organism�w i Area.
	- private static void NextTurn() - metoda wywo�uj�ca now� tur�. Najpierw czyszczona jest plansza i okienko log�w z poprzedniego stanu, nast�pnie przepisywane s� obiekty World w tablicy z archiwalnymi �wiatami i generowana jest kopia aktualnego �wiata. Nast�pnie wywo�ywana jest metoda ExecuteTurn().
	- private static void PreviousTurn(int round) - metoda wczytuj�ca obiekt World z poprzedniej tury. Analogicznie do metody NextTurn() przepisywane s� na s�siaduj�ce miejsca obiekty World w tablicy i na jej ko�cu dopisywany jest null.
	- protected override void OnClosed(EventArgs e) - metoda opisuj�ca co musi zosta� wykonane po zamkni�ciu g��wnego okna.
	- public void drawBoard() - metoda rysuj�ca plansz�. 
	- private void NowaTura_Click(object sender, RoutedEventArgs e) - metoda opisuj�ca zachowanie programu po naci�ni�ciu przycisku Next Turn. Wywo�uje metod� NextRurn() lub je�eli gra nie jest zainicjowana to wyw�luje metod� NowaGra_Click()
	- private void NowaGra_Click(object sender, RoutedEventArgs e) - metoda opisuj�ca zachowanie programu po naci�ni�ciu przycisku New Game. Wywo�uje powstnie Window1 startWindow i metod� startWindow.Show().
	- public void NewBoard() - metoda wywo�ywana przy tworzeniu nowej gry, odpowiedzialna za zainicjowanie nowego �wiata World i wywo�anie metody drawBoard().
	- private void Button_Click(object sender, RoutedEventArgs e) - metoda opisuj�ca zachowanie programu po naci�ni�ciu przycisku Previous Turn. Sprawdza czy istniej� zapisane poprzednie tury, je�eli tak to wczytuje poprzedni� tur� wywo�uj�c metod� PreviousTurn().
	- private async void Button_Click_1(object sender, RoutedEventArgs e) - metoda opisuj�ca zachowanie programu po naci�ni�ciu przycisku Start/Stop Automatic Turns.
	- private void dispatcherTimer_Tick(object sender, EventArgs e) - metoda odpowiedzialna za cykliczne wywo�ywanie metody NextTurn() w sta�ych odt�pach czasu.
	- private void Legend_Click(object sender, RoutedEventArgs e) -  metoda opisuj�ca zachowanie programu po naci�ni�ciu przycisku Legend. Wywo�uje okno Window2 zawieraj�ce legende.
	- private async void Button_Click_2(object sender, RoutedEventArgs e) - metoda opisuj�ca zachowanie programu po naci�ni�ciu przycisku Save Game. Wywo�ywuje metode SerailizeGame().
	- private async void Button_Click_3(object sender, RoutedEventArgs e) - metoda opisuj�ca zachowanie programu po naci�ni�ciu przycisku Load Game. Wywo�ywuje metode DeserializeGame() i nast�pnie rysuje nowowczytany �wiat.



Klasa World - podstawowa klasa programu zawieraj�ca wszystkie informacje o aktualnym stanie gry. Sk�ada si� z:
	- int Size - rozmiar �wiata, tym samym boku planszy 
	- static int DeathCounterLimit - (domy�lna warto�� 4) - okre�la ile tur Organism mo�e sp�dzi� na niebezpiecznym dla siebie terenie - dok�adny opis mechanizmu w klasie Area
	- static int MaxEvolvesPerTurn - (obliczony jako Size*Size/10) - okre�la ile maksymalnie obiekt�w Area mo�e si� zmieni� na raz - dok�adny opis mechanizmu w klasie Area
	- List<Organism> OrganismBoard - Lista tworz�ca "plansz�" na kt�rej znajduj� si� obiekty klasy Organism. Dzi�ki niej mo�na zlokalizowa� dany Organism w grze.
	- List<Area> AreaBoard - Lista analogiczna do powy�szej, ale zawieraj�ca obiekty klasy Area.
        - LinkedList<Organism> Init6Mice - Lista inicjatywy do kt�rej dodawane s� Organismy o danej inicjatywie - w tym przypadku zwierz� Mouse o inicjatywie 6. Dzi�ki tej li�cie mo�na okre�li� kolejno�� 	wykonywania ruch�w przez organizmy w grze. Nowe Organismy o danej inicjatywie dopisywane s� do ko�ca listy.
        - LinkedList<Organism> Init5Wolfs - Lista analogiczna do powy�szej, zawieraj�ca Organismy o inicjatywie 5 (Wolf).
        - LinkedList<Organism> Init4SheepAndThornEaters - j.w. inicjatywa 4 (Sheep i ThornEater)
        - LinkedList<Organism> Init3Snakes - j.w. inicjatywa 3 Snake)
        - LinkedList<Organism> Init1Plants- j.w. inicjatywa 1 (wszystkie klasy ro�lin - dziedzicz�ce po Plant, np. Grass)
        - LinkedList<Organism> Init4SheepAndThornEaters - j.w. inicjatywa 4 (Sheep i ThornEater)
	- LinkedList<Organism> AddingList - lista u�ywana do przetrzymywania �wie�o utworzonych Organism�w przed dodaniem ich do ko�ca listy inicjatyw.
	- LinkedList<int> MaxDifferenceList - lista przetrzymuj�ca lokalizacj� obiekt�w Area o najwy�szej r�nicy wp�yw�w - dok�adny opis mechanizmu w klasie Area.
	- int TurnsFromLastEvolve - okre�la ilo�� tur od ostatniej ewolucji obszar�w Area - dok�adny opis mechanizmu w klasie Area.
	- int TurnsFromGeneratingMaxDiffList- okre�la ilo�� tur od utworzenia listy MaxDifferenceList - dok�adny opis mechanizmu w klasie Area.

	Metody i konstruktory:
	- public World(int size) - podstawowy konstrukor u�ywany przy tworzeniu nowej gry, wywo�uje metod� InitializeWorld()
	- public World(World oldWorld) - konstruktor kopiuj�cy, tworzy dok�adn� kopi� instancji klasy World, ��cznie z utworzeniem kopii Organism�w i Area. Iteruje po List<Organism> OrganismBoard tworz�c kopi� Organism�w, nast�pnie na podstawie list inicjatyw z kopiowanego obiektu World (np Init4SheepAndThornEaters) odtwarza listy nowotworzonego obiektu. U�ywany do tworzenia historii ostatnich tur.
	- public void ExecuteTurn() - metoda wykonuj�ca jedn� tur� w grze. Wywo�uje ona poszczeg�lne metody sk�adaj�ce si� na przebieg tury. 
	- private void ExecuteActionForInitList(LinkedList<Organism> currentList) - metoda iteruj�ca po danej li�cie inicjatyw currentList. Metoda sprawdza czy dany Organism nie zgin�� od czasu ostatnio wykonywanej akcji, je�eli jest martwy (Alive==false) to usuwa go z listy, je�eli jest �ywy to wykonuj� metod� Action() oraz UpdateDeathCounter(). W przypadku gdy zostanie stworzony nowy Organism w trakcie metody Action(), jest on dodawany do listy AddingList. Po przej�ciu ca�ej listy currentList nowe Organismy znajduj�ce si� na AddingList s� dopisywane do ko�ca currentList. Lista AddingList jest czyszczona na pocz�tku dzia�ania metody.
	- private void EvolveAreaboard() - metoda dzi�ki kt�rej modyfikowany jest teren na planszy (obiekty Area). Metoda sprawdza ile tur min�o od czasu ostatniej ewolucji terenu, je�eli >2 to stara si� wykona� ewolucj� planszy. W tym celu najpierw sprawdza ile czasu min�o od ostatniego wygenerowania MaxDifferenceList, je�eli >8 to wywo�uj� metod� GenerateMaxDifferenceList(). Nast�pnie wykonuj� ewolucje Area w ilo�ci MaxEvolvesPerTurn. W tym celu losuj� lokalizacj� obiektu Area z listy MaxDifferenceList i wykonuj� na tym obiekcie metod� Evolve() (opis w klasie Area). Je�eli zabraknie obiekt�w na li�cie MaxDifferenceList to jest ona generowana od nowa.  
	- private void GenerateMaxDifferenceList() - metoda generuj�ca list� lokalizacji obiekt�w Area posiadaj�cych najwi�ksz� InfluenceDifference (opis w klasie Area). Najpierw iteruj� po wszystkich obiektach Area znajduj�cych si� w grze wywo�uj�c metod� InfluenceDifference w celu znalezienia maksymalnej warto�ci MaxInfluenceDifference nast�pnie iteruje ponownie dodaj�c takie obiekty do MaxDifferenceList.
	- private void UpdateStrengthModifiers() - metoda iteruj�ca po planszy - OrganismBoard, wywo�uj�ca dla ka�dego Oganismu metod� UpdateStrength() (opis w klasie Organism)
	- private void CheckEnvironmentalDeaths() - metoda sprawdzaj�ca dla ka�dego Organismu na planszy, czy kt�ry� z Organism�w nie przekroczy� DeathCounterLimit, je�eli tak si� sta�o, Organism ginie (Alive = false)
	- private void InitializeWorld() - metoda inicjalizuj�ca nowy obiekt klasy World, wykonuj� ona poszczeg�lne metody sk�adaj�ce si� na tworzenie nowego �wiata
	- private int GetPlantsAmount(int size) - metoda zwracaj�ca ilo�� Organism�w typu Plant jak� nale�y wygenerowa� w nowym �wiecie wed�ug wzoru toUpper(Size*Size/25)
	- private int GetAnimalsAmount(int size) - metoda zwracaj�ca ilo�� Organism�w typu Animal i Thorn jak� nale�y wygenerowa� w nowym �wiecie wed�ug wzoru toUpper(Size*Size/15)
	- private void InitializeOrganismBoard(int plantsAmount, int animalsAmount) - metoda tworz�ca nowe obiekty poszczeg�lnych klas w nowym �wiecie. Dla danej klasy (Wolf, Mouse, Grass...) tworzy ona okre�lon� przez GetPlantsAmount(int size) ilo�� obiekt�w i umieszcza je na planszy przy uzyciu AddToBoard, dodaj�c je r�wnie� do odpowiedniej listy inicjatyw (np. Init5Wolfs)
	- private void InitializeAreaBoard() - metoda generuj�ca i wstawiaj�ce nowe obiekty Area na nowej plansze AreaBoard. Wykorzystywana podczas inicjalizacji nowej gry. Na pocz�tku plansza wype�niana jest obiektami Area o tymczasowym type = -1. Nast�pnie w czterech losowych miejscach wstawiane s� po jednym obiekcie ka�dego typu (opis typ�w w klasie Area). W momencie dodawania obiektu danego typu obiekty s�siaduj�ce typu -1 maj� modyfikowan� tablic� Influence metod� AddInfluence() i dodawane s� do listy InfluencedLocationsList[]. Istniej� cztery listy InfluencedLocationsList dla ka�dego typu Area. Po wygenerowaniu losowo wybranych punkt�w pocz�tkowych dla ka�dego typu nast�puje p�tla wype�niaj�ca ca�� reszt� planszy. Dodawane jest po kolei po jedynm obiekcie danego typu. W p�tli najpierw sprawdzane jest czy znajduj� si� obiekty na li�cie InfluencedLocationsList[], je�eli tak to pobierany jest z niej losowy obiekt o maksymalnej warto�ci Influence poszukiwanego typu. Je�eli nie to wybierana jest losowa wolna lokacja na AreaBoard (type = -1). Nast�pnie w wybrane miejsce wstawiane jest obiekt aktualnie wstawianego typu. Wybrana lokacja jest usuwana z InfluencedLocationsList[] wszystkich typow. Nastepnie do InfluencedLocationsList[] danego typu dodawni sa wszyscy sasiedzi wybranej lokacji o typie -1. Petla powtarza sie az do wypelnienia planszy. Taki przebieg algorytmu ma wymusic grupowanie sie obszarow konkretnych typow w wieksze grupy, jednoczesnie dopuszczajac mozliwosc powstawania wiekszej ilosci mniejszych grupek, gdy poprzednia grupka podczas generowania nie ma sie gdzie "rozrastac"
	- public void PrintBoard() - metoda u�ywana do rysowania planszy. P�tle w metodzie przechodz� po ca�ej li�cie AreaBoard i OrganismBoard rysuj�c odpowiednie t�o pola w zale�no�ci od Area i odpowiedni Organism je�li taki si� na danym polu znajduj�.
	- public int GenerateEmptyLocation() - metoda generuj�ca dowoln� pust� lokalizacj� na planszy. Generuje losowe lokazilacje dop�ki nie trafi takiej kt�ra nie jest pusta.
	- public int GenerateNearbyEmptyLocation(int currentLocation) - metoda generuj�ca losow� lokacj� wok� danej lokalizacji currentLocation. Wywo�uje metod� PotentialList(currentLocation), nast�pnie z uzyskanej listy odrzuca te lokalizacje kt�re s� zaj�te. Je�eli odrzuci wszystkei lokalizacje z listy, to zwraca -1, je�eli na li�cie co� zostanie, to zwraca losowy jej element.
	- public int GenerateNearbyLocation(int currentLocation) - metoda generuj�ca dowoln� lokalizacj� wok� zadanej lokalizacji currentLocation. Wywo�uje metod� PotentialList(currentLocation), nast�pnie zwraca losowy jej element.
	- private LinkedList<int> PotentialList(int currentLocation) - metoda generujaca liste lokacji sasiadujacych do lokacji currentLocation. Przy pomocy kilku warunkow brzegowych sprawdzajacych czy podana lokacja nie znajduje sie na skraju planszy zaweza ona ilosc generowanych wartosci. 
	- public void AddToBoard(int location, Organism organism) - metoda wstawiajaca w podane miejsce na planszy location podany organism.
	- public void NullToBoard(int location) - metoda wstawiajaca w podane miejsce na planszy location warto�� null.
	- public World(SerializationInfo info, StreamingContext context) - konstruktor wykorzystywany przy deserializacji obiektu - wczytaniu gry z pliku binarnego.
	- public void GetObjectData(SerializationInfo info, StreamingContext context) - metoda wykorzystywana do serializacji obiektu.



Klasa Area - klasa opisuj�ca rodzja pola znajduj�cego si� w danym miejscu na planszy. G��wnym parametrem jest warto�� type okre�laj�ca rodzaj pola (0 - Polana, 1 - Pustynia, 2 - Bagno, 3 - Las). Rodzaj obszaru znajduj�cy si� w danym miejscu planszy ma du�e znaczenie na zachowanie niekt�rych obiekt�w klasy Organism.
	- public int Type - okre�la typ danego obszaru Area
	- public int[] Influence - tablica zawieraj�ca cztery warto�ci odpowiadaj�ce czterem rodzajom terenu, oznaczaj�ce jaki wp�yw ma dany typ terenu na dany obiekt Area.
	- public List<int> MaxInfluenceType - Lista zawieraj�ca typy teren�w kt�re maj� najwy�szy wp�yw na obiekt.
	- public int InfluenceDifference - warto�� r�nicy mi�dzy najwy�szym wp�ywem z tablicy Influence[] i warto�ci� wp�ywu typu w�asciwego danemu obiektowi Area.
	- private int MaxInfluenceValue - warto�� maksymalnego wp�ywa z tablicy Influence[].

	Metody i konstruktory:
	- public Area(int type) - konstruktor podstawowy tworz�cy obiekt podanego typu.
	- public Area(Area area) - konstruktor kopiuj�cy.
	- public Area GetCloned() - metoda zwracaj�ca klon danego obiektu.
	- public void CheckInfuence(World world, LinkedList<int> potentialList) - metoda sprawdzaj�ca wp�yw s�siaduj�cych kom�rek, aktualizuj�ca tablic� InfluenceList[]. Nast�pnie odnajduje warto�� maksymaln� MaxInfluenceValue i por�wnuj�c j� z wp�ywem typu macierzystego uzyskuje InfluenceDifference. Czy�ci i uzupe�nia liste MaxInfluenceType.
	- public void AddInfluence(int type) - metoda inkrementujaca komorke w tablicy Influence[].
	- public void Evolve(Random generator) - metoda wywo�uj�ca zmian� typu danego obiektu i przemiane w typ odpowiadaj�cy losowemu elementowi z tablicy MaxInfluenceType.
	- public Area(SerializationInfo info, StreamingContext context) - konstruktor wykorzystywany przy deserializacji - wczytaniu gry z pliku binarnego. Odczytuje z pliku poszczegolne wartosci danego obiektu i na ich podstawie tworzy nowy obiekt Area.
	- public void GetObjectData(SerializationInfo info, StreamingContext context) - metoda wykorzystywana przy serializacji - zapisu gry do pliku binarnego. Analogiczne dzia�anie do kostruktora powy�ej.



Klasa Organism - klasa bazowa wszystkich organizmow znajdujacych sie w grze. Dziedzicza po niej wszystkie zwierzeta Animal i Plant. Okre�la ona podstawowe cechy i metody abstrakcyjne organizmow w grze. Implementuje tez kilka podstawowych metod wirtualnych.
	- public int Strength - okre�la si�e organizmu, podstawowy czynnik wp�ywaj�cy na rozstrzyganie walk mi�dzy organizmami r�nego typu
	- public int StrengthModifier - tymczasowy modyfikator si�y zale�ny od terenu na kt�rym znajduje si� dany organizm. Domy�lnie r�wny 0. Podczas walki dodawany jest do si�y organizmu Strength.
	- public int Location - okre�la lokalizacj� obiektu na planszy.
	- public char Sign - okre�la znak jakim przestawiany jest obiekt na planszy.
	- public bool Alive - okre�la czy organizm jest �ywy.
	- public int DeathCounter - licznik kt�rego warto�� ro�nie gdy organizm przebywa na terenie dla siebie niebezpiecznym. Kiedy organizm zejdzie z pola niebezpiecznego DeathCounter wraca do warto�ci 0.

	Metody i konstruktory:
	- public abstract void Action(World world) - metoda wywo�ywana gdy dany obiekt ma wykona� sw�j ruch w danej turze. 
	- public abstract void Spawn(World world) - metoda wywo�ywana gdy dany obiekt podejmuje pr�b� rozmno�enia.
	- public abstract void UpdateStrength(Area area) - metoda aktualizuj�ca StrengthModifier danego obiektu.
	- public abstract Organism GetCloned() - metoda zwracaj�ca obiekt b�d�cy dok�adn� kopi� obiektu. Jest nadpisywana w ka�dej klasie i zawsze polega jedynie na zwr�ceniu obiektu danej klasy za pomoc� konstrkura kopiuj�cego.
	- public virtual bool Collision(World world, Organism attackingOrganism) - metoda wirtualna wywo�ywana kiedy organizm zostanie zaatakowany. Zwraca false je�eli organizm atakuj�cy jest s�abszy (ma ni�sz� si��) od atakowanego. Zwraca true je�eli organizm atakuj�cy jest silniejszy, jednocze�nie zmienia warto�� this.alive na false.
	- public virtual void UpdateDeathCounter(Area area) - metoda wirtualna aktualizuj�ca DeathCounter. Je�eli dany organizm znajduje si� na polu gdzie Area.type = 1, to DeathCounter ro�nie, je�eli nie, to spada do 0.
	- public Organism(SerializationInfo info, StreamingContext context) - konstruktor wykorzystywany przy deserializacji - wczytaniu gry z pliku binarnego. Odczytuje z pliku poszczegolne wartosci danego obiektu i na ich podstawie tworzy nowy obiekt Organism.
	- public virtual void GetObjectData(SerializationInfo info, StreamingContext context) - metoda wykorzystywana przy serializacji - zapisu gry do pliku binarnego. Analogiczne dzia�anie do kostruktora powy�ej.



Klasa Plant - klasa bazowa dla wszystkich ro�lin, dziedzicz�ca po Organism. Wszystkie klasy dziedzicz�ce po Plant maj� inicjatyw�. Nadpisuje jedynie metod� kolizji.
        - override public void Action(World world) - ka�da klasa dziedzicz�ca po Plant w swojej turze wykonuje jedynie pr�be rozmno�enia Spawn().
	- override public bool Collision(World world, Organism attackingOrganism) - metoda kolizji dla wi�kszo�ci ro�lin, zawsze zwraca true, a obiekt zaatakowany staje si� martwy.



Klasa Grass - klasa ro�liny dziedzicz�ca po Plant. Posiada si�� 0 i znak 'G'. Trawa nie posiada wielu specjajnych w�a�ciwo�ci. Nadpisuj� metod� Spawn(), dodatkowo nadpisuje jedynie metod� UpdateDeathCounter.
	- override public void Spawn(World world) - metoda spawn wywo�ywana jest w celu podj�cia pr�by rozprzestrzenienia si� danego gatunku ro�liny. Ro�liny maj� 50% szans na podj�cie pr�by rozmno�enia. Je�eli pr�ba zako�czy si� sukcesem to generowana jest lokalizacja nowej ro�liny za pomoc� metody GenerateNearbyEmptyLocation() i wstawiana jest w jej miejsce nowa ro�lina. Je�eli nie ma wolnych lokalizacji wok� ro�liny, to pr�ba ko�czy si� niepowodzeniem.
	- override public void UpdateDeathCounter(Area area) - metoda aktualizuj�ca DeathCounter danego obiektu. W przypadku trawy DeathCounter ro�nie tak�e gdy area.Type == 3, czyli gdy trawa znajduje si� w lesie.



Klasa Guarana - klasa ro�liny dziedzicz�ca po Plant. Posiada si�� 0 i znak 'U'. Specjaln� w�a�ciwo�ci� Guarany jest to �e zwi�ksza si�� obiektu, kt�ry j� zaatakuje o 3 lub o 2 je�eli Guarana znajduje si� na Area.type==0. W zwi�zku z tym Guarana nadpisuje metod� Collision i metod� Spawn(). 
	- override public void Spawn(World world) - metoda spawn wywo�ywana jest w celu podj�cia pr�by rozprzestrzenienia si� danego gatunku ro�liny. Ro�liny maj� 50% szans na podj�cie pr�by rozmno�enia. Je�eli pr�ba zako�czy si� sukcesem to generowana jest lokalizacja nowej ro�liny za pomoc� metody GenerateNearbyEmptyLocation() i wstawiana jest w jej miejsce nowa ro�lina. Je�eli nie ma wolnych lokalizacji wok� ro�liny, to pr�ba ko�czy si� niepowodzeniem.
	- override public bool Collision(World world, Organism attackingOrganism) - metoda kolizji, zwraca true a obiekt zaatakowany staje si� martwy. Dodatkowo zwi�ksza si�� organizmu atakuj�cego o 3 lub o 2 je�eli dana Guarana znajduje si� na Area.type==0 (na polanie).



Klasa Thorn - klasa ro�liny dziedzicz�ca po Plant. Posiada si�� 2 i znak 'T'. Specjaln� w�a�ciwo�ci� Thorn jest to, �e posiada si�� 2, oraz w metodzie Collision je�eli zostanie zjedzony przez obiekt klasy ThornEater, to zwi�ksza jego si�e o 2. W zwi�zku z tym Thorn nadpisuje metod� Collision i metod� Spawn(). 
	- override public void Spawn(World world) - metoda spawn wywo�ywana jest w celu podj�cia pr�by rozprzestrzenienia si� danego gatunku ro�liny. Ro�liny maj� 50% szans na podj�cie pr�by rozmno�enia. Je�eli pr�ba zako�czy si� sukcesem to generowana jest lokalizacja nowej ro�liny za pomoc� metody GenerateNearbyEmptyLocation() i wstawiana jest w jej miejsce nowa ro�lina. Je�eli nie ma wolnych lokalizacji wok� ro�liny, to pr�ba ko�czy si� niepowodzeniem.
	- override public bool Collision(World world, Organism attackingOrganism) - metoda kolizji. Zwraca false je�eli organizm atakuj�cy jest s�abszy (ma ni�sz� si��) od atakowanego. Zwraca true je�eli organizm atakuj�cy jest silniejszy, jednocze�nie zmienia warto�� this.alive na false. Dodatkowo je�eli organizmem kt�ry zatakowa� jest ThornEater zwi�ksza jego si�� o 2.



Klasa Animal - klasa bazowa dla wszystkich zwierz�t, dziedzicz�ca po Organism. Wszystkie klasy dziedzicz�ce po Animal maj� tak� sam� metod� Action(). Nadpisuje r�wnie� metod� UpdateStrength().
        - override public void Action(World world) - metoda wywo�ywana gdy dany obiekt ma wykona� sw�j ruch w danej turze. W przypadku wszystkich zwierz�t wygl�da tak samo. Dane zwierz� stara si� w swojej kolejce przej�� na s�siaduj�ce pole generowane przez GenerateNearbyLocation(). Je�eli wygenerowane pole jest puste to zwierz� na nie przechodzi. Je�eli na wygenerowanym polu znajduje si� obiekt tej samej klasy to wywo�ywana jest metoda Spawn(). Je�eli znajduje si� obiekt innej klasy, to rozpoczyna si� walka mi�dzy tymi obiektami poprzez wywo�anie metody Collision obiektu zatakowanego. Je�eli metoda Collision zwr�ci true, to organizm atakuj�cy wygra� i przesuwa si� na wygenerowane wcze�niej pole. Je�eli zwr�ci false, to przegra�, w efekcie ginie i jest usuwany z planszy.
	- override public void UpdateStrength(Area area) - metoda aktualizuj�ca StrengthModifier danego obiektu. W przypadku wi�kszo�ci zwierz�t StrengthModifier zmniejsza modyfikator si�y na polu Area.type == 1 (na pustyni).



Klasa Mouse - klasa zwierz�cia dziedzicz�ca po Animal. Posiada inicjatyw� 6, si�� 1 i znak 'M'. Specjaln� w�a�ciwo�ci� myszy jest to, �e gdy znajduje si� na Area.type=0 (na polanie), to ma podw�jne szanse na potomstwo. Dodatkowo mysz w metodzie Collision potrafi ucieka� je�li przegra walk�, chyba �e atakuje j� Snake, wtedy nie jest w stanie uciec. 
	- override public bool Collision(World world, Organism attackingOrganism) - metoda Collision r�ni�ca si� do standardowej tym, �e gdy zaatakowana mysz przegra walk�, to stara si� uciec na losowe puste wolne miejsce. Je�eli nie ma akurat �adnego wolnego pola lub gdy organizmem atakuj�cym jest Snake, to myszy nie udaje si� uciec i ginie.
	- override public void Spawn(World world) - metoda wywo�ywana gdy zwierz� danego typu natrafi podczas swojej tury w metodzie Action() na organizm tego samego typu. W przypadku myszy, gdy mysz znajduje si� na Area.type=0, to pr�ba jest podejmowana dwukrotnie. 



Klasa Sheep - klasa zwierz�cia dziedzicz�ca po Animal. Posiada inicjatyw� 4, si�� 4 i znak 'O'. Specjaln� w�a�ciwo�ci� owcy jest to, �e metoda UpdateStrength zmniejsza modyfikator si�y zar�wna na terenie Area.type == 1 jak i Area.type == 3 (na pustyni oraz w lesie)
	- override public void Spawn(World world) - metoda wywo�ywana gdy zwierz� danego typu natrafi podczas swojej tury w metodzie Action() na organizm tego samego typu. W przypadku Owcy nie ma w niej nic specjalnego.
	- override public void UpdateStrength(Area area) - metoda aktualizuj�ca StrengthModifier danego obiektu. W przypadku owcy StrengthModifier zmniejsza modyfikator si�y na polu Area.type == 1 oraz Area.type == 3 (na pustyni oraz w lesie).



Klasa Snake - klasa zwierz�cia dziedzicz�ca po Animal. Posiada inicjatyw� 3, si�� 2 i znak 'S'. Specjaln� w�a�ciwo�ci� w�y jest to, �e kiedy zostanie on zabity podczas metody Collision, to zatruwa on prganizm atakuj�cy, w efekcie r�wnie� organizm atakuj�cy ginie. 
	- override public void Spawn(World world) - metoda wywo�ywana gdy zwierz� danego typu natrafi podczas swojej tury w metodzie Action() na organizm tego samego typu. W przypadku w�y nie ma w niej nic specjalnego.
	- override public bool Collision(World world, Organism attackingOrganism) - metoda Collision r�ni�ca si� do standardowej tym, �e gdy zaatakowany w�� przegra walk�, to zatruwa organizm go atakuj�cy. W efekcie atakuj�cy r�wnie� ginie, metoda zawsze zwraca false.



Klasa ThornEater - klasa zwierz�cia dziedzicz�ca po Animal. Posiada inicjatyw� 4, si�� 4 i znak 'E'. Specjaln� w�a�ciwo�ci� cierniojada jest to, �e gdy znajduje si� na Area.type=3 (w lesie), to ma szanse na podw�jne potomstwo. Dodatkowo gdy ThornEater zje Thorn, to zyskuje +2 si�y (opis w klasie Thorn).
	- override public void Spawn(World world) - metoda wywo�ywana gdy zwierz� danego typu natrafi podczas swojej tury w metodzie Action() na organizm tego samego typu. W przypadku cierno�era, gdy ciernio�er znajduje si� na Area.type=3, to pr�ba jest podejmowana dwukrotnie.



Klasa Wolf - klasa zwierz�cia dziedzicz�ca po Animal. Posiada inicjatyw� 5, si�� 6 i znak 'W'. Wilk nie posiada specjalnych w�a�ciwo�ci.
	- override public void Spawn(World world) - metoda wywo�ywana gdy zwierz� danego typu natrafi podczas swojej tury w metodzie Action() na organizm tego samego typu. W przypadku wilk�w nie ma w niej nic specjalnego.





Przybli�ony podzia� zada�:

Jacek Sudak: klasa World, Mo�liwo�� odtworzenia stanu gry w zakresie ostatnich 5 tur.

Pawe� Lipi�ski: Klasa Main i WPF, klasa Organism, Animal, Plant i ich pochodne.

Miko�aj Kr�l: Klasa Area, mo�liwo�� zapisu do pliku i wczytania z pliku stanu gry. 
