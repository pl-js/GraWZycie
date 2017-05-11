Tytu³ projektu: Gra w ¿ycie
Autorzy: Miko³aj Król, Pawe³ Lipiñski, Jacek Sudak

Celem projektu jest stworzenie prostego symulatora wirtualnego œwiata w postaci dwuwymiarowej tablicy o zadanych rozmiarach NxN. W œwiecie bêd¹ istnia³y proste formy ¿ycia o odmiennym zachowaniu. Ka¿dy z organizmów zajmuje dok³adnie jedno pole w tablicy, na ka¿dym polu mo¿e znajdowaæ siê co najwy¿ej jeden organizm (w przypadku kolizji jeden z nich usuniêty lub przesuniêty). Symulator ma bêdzie mia³ turowy. W ka¿dej turze wszystkie organizmy istniej¹ce na œwiecie wykonuj¹ akcjê odpowiedni¹ dla swojej klasy. Czêœæ z nich bêdzie siê porusza³a (zwierzêta), czêœæ bêdzie nieruchoma (roœliny). W przypadku kolizji (jeden z organizmów znajdzie siê na tym samym polu, co inny) jeden z organizmów zwyciê¿a, zabijaj¹c konkurenta. Kolejnoœæ ruchów organizmów w turze zale¿y od inicjatywy. Pierwsze ruszaj¹ siê zwierzêta posiadaj¹ce najwy¿sz¹ inicjatywê. Jeœli dwa organizmy maj¹ tak¹ sam¹ inicjatywê, o tym, który wykona akcjê najpierw decyduje wiek organizmu (starszy wykona akcjê pierwszy). Zwyciêstwo przy spotkaniu zale¿y od si³y organizmu, ale dla poszczególnych organizmów byæ mo¿e bêd¹ zaimplementowane metody, które sprawi¹, ¿e bêdzie inaczej. Przy równej sile zwyciê¿a organizm, który zaatakowa³. Przy uruchomieniu programu na planszy pojawi siê po kilka sztuk zwierz¹t i roœlin. W oknie programu bêd¹ wypisywane informacje z wykonanej tury - kto zjad³ kogo, jaki organizm uciek³ przed walk¹ itd.



Opis podstawowych klas:

Klasa World - bêdzie stanowi³a ogóln¹ ramê programu - to w niej bêd¹ siê zawiera³y podstawowe metody programu takie jak “MakeATurn”, “InicialiseTheWorld” oraz metody pomocnicze, np. “GetNearbyEmptyLocation”.

Klasa Organism - podstawowa klasa abstrakcyjna,  od której dziedziczyæ bêd¹ zwierzêta i roœliny. Zawiera takie pola jak “CoordX”, “CoordY”, “Age”, “Initiative”, “Strength”. Wymuszaæ bêdzie wykorzystanie metody “Action”, “Collision ”, “Reproduction” itd.

Klasa Animal i klasy dziedzicz¹ce - dziedziczy po klasie Organism, przy czym w klasie Action obiekty tej klasy wykonuj¹ ruchy po planszy. 

Klasa Plant i klasy dziedzicz¹ce - dziedziczy po klasie Organism, przy czym w klasie Action obiekty tej klasy pozostaj¹ na swoich miejscach, za to z pewnym prawdopodobieñstwem sukcesu dokonuj¹ reprodukcji - rozsiewaj¹ siê. 



Lista zadañ:

Jacek Sudak:
Klasa World 
Funkcja automatycznego wykonywania kolejnych tur w okreœlonych odstêpach czasowych 


Pawe³ Lipiñski
Klasa Organism    
Klasa Animal, Plant i ich pochodne

Miko³aj Król:
Mo¿liwoœæ zapisu do pliku i wczytania z pliku stanu gry
Mo¿liwoœæ odtworzenia stanu gry w zakresie ostatnich 5 tur

WPF - jeszcze siê oka¿e. W póŸniejszym pliku zamieœcimy faktyczny podzia³.
