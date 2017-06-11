Tytuł projektu: Gra w życie

Autorzy: Mikołaj Król, Paweł Lipiński, Jacek Sudak

Celem projektu jest stworzenie prostego symulatora wirtualnego świata w postaci dwuwymiarowej tablicy o zadanych rozmiarach NxN. W świecie będą istniały proste formy życia o odmiennym zachowaniu. Każdy z organizmów zajmuje dokładnie jedno pole w tablicy, na każdym polu może znajdować się co najwyżej jeden organizm (w przypadku kolizji jeden z nich usunięty lub przesunięty). Symulator ma będzie miał turowy. W każdej turze wszystkie organizmy istniejące na świecie wykonują akcję odpowiednią dla swojej klasy. Część z nich będzie się poruszała (zwierzęta), część będzie nieruchoma (rośliny). W przypadku kolizji (jeden z organizmów znajdzie się na tym samym polu, co inny) jeden z organizmów zwycięża, zabijając konkurenta. Kolejność ruchów organizmów w turze zależy od inicjatywy. Pierwsze ruszają się zwierzęta posiadające najwyższą inicjatywę. Jeśli dwa organizmy mają taką samą inicjatywę, o tym, który wykona akcję najpierw decyduje wiek organizmu (starszy wykona akcję pierwszy). Zwycięstwo przy spotkaniu zależy od siły organizmu, ale dla poszczególnych organizmów być może będą zaimplementowane metody, które sprawią, że będzie inaczej. Przy równej sile zwycięża organizm, który zaatakował. Przy uruchomieniu programu na planszy pojawi się po kilka sztuk zwierząt i roślin. W oknie programu będą wypisywane informacje z wykonanej tury - kto zjadł kogo, jaki organizm uciekł przed walką itd.



Opis podstawowych klas:

Klasa World - będzie stanowiła ogólną ramę programu - to w niej będą się zawierały podstawowe metody programu takie jak “MakeATurn”, “InicialiseTheWorld” oraz metody pomocnicze, np. “GetNearbyEmptyLocation”.

Klasa Organism - podstawowa klasa abstrakcyjna,  od której dziedziczyć będą zwierzęta i rośliny. Zawiera takie pola jak “CoordX”, “CoordY”, “Age”, “Initiative”, “Strength”. Wymuszać będzie wykorzystanie metody “Action”, “Collision ”, “Reproduction” itd.

Klasa Animal i klasy dziedziczące - dziedziczy po klasie Organism, przy czym w klasie Action obiekty tej klasy wykonują ruchy po planszy. 

Klasa Plant i klasy dziedziczące - dziedziczy po klasie Organism, przy czym w klasie Action obiekty tej klasy pozostają na swoich miejscach, za to z pewnym prawdopodobieństwem sukcesu dokonują reprodukcji - rozsiewają się. 



Lista zadań:

Jacek Sudak: Klasa World, Funkcja automatycznego wykonywania kolejnych tur w określonych odstępach czasowych 


Paweł Lipiński: Klasa Organism, Klasa Animal, Plant i ich pochodne


Mikołaj Król: Możliwość zapisu do pliku i wczytania z pliku stanu gry, Możliwość odtworzenia stanu gry w zakresie ostatnich 5 tur


WPF - jeszcze się okaże. W późniejszym pliku zamieścimy faktyczny podział.


Edit 11.06.2017:
Zgodnie z zaleceniami prowadzących rozszerzymy projekt aplikacji o dodatkową klasę Area. Klasa ta reprezentować będzie pola, typu np: pustynia, łąka, las, bagno, jezioro. Będą one wpływały na organizmy na nich się znajdujące lub na organizmy przyległe, np. tymczasowo modyfikując ich siłę czy inicjatywę lub szanse rozmnożenia (w przypadku klasy Plant). Dodatkowo niektóre z tych pól będą się cyklicznie, co określoną liczbe rund, zmieniać w inne pole specjalne, np: bagno zmienia się w jezioro, itp.
