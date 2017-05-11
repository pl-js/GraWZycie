Tytu� projektu: Gra w �ycie
Autorzy: Miko�aj Kr�l, Pawe� Lipi�ski, Jacek Sudak

Celem projektu jest stworzenie prostego symulatora wirtualnego �wiata w postaci dwuwymiarowej tablicy o zadanych rozmiarach NxN. W �wiecie b�d� istnia�y proste formy �ycia o odmiennym zachowaniu. Ka�dy z organizm�w zajmuje dok�adnie jedno pole w tablicy, na ka�dym polu mo�e znajdowa� si� co najwy�ej jeden organizm (w przypadku kolizji jeden z nich usuni�ty lub przesuni�ty). Symulator ma b�dzie mia� turowy. W ka�dej turze wszystkie organizmy istniej�ce na �wiecie wykonuj� akcj� odpowiedni� dla swojej klasy. Cz�� z nich b�dzie si� porusza�a (zwierz�ta), cz�� b�dzie nieruchoma (ro�liny). W przypadku kolizji (jeden z organizm�w znajdzie si� na tym samym polu, co inny) jeden z organizm�w zwyci�a, zabijaj�c konkurenta. Kolejno�� ruch�w organizm�w w turze zale�y od inicjatywy. Pierwsze ruszaj� si� zwierz�ta posiadaj�ce najwy�sz� inicjatyw�. Je�li dwa organizmy maj� tak� sam� inicjatyw�, o tym, kt�ry wykona akcj� najpierw decyduje wiek organizmu (starszy wykona akcj� pierwszy). Zwyci�stwo przy spotkaniu zale�y od si�y organizmu, ale dla poszczeg�lnych organizm�w by� mo�e b�d� zaimplementowane metody, kt�re sprawi�, �e b�dzie inaczej. Przy r�wnej sile zwyci�a organizm, kt�ry zaatakowa�. Przy uruchomieniu programu na planszy pojawi si� po kilka sztuk zwierz�t i ro�lin. W oknie programu b�d� wypisywane informacje z wykonanej tury - kto zjad� kogo, jaki organizm uciek� przed walk� itd.



Opis podstawowych klas:

Klasa World - b�dzie stanowi�a og�ln� ram� programu - to w niej b�d� si� zawiera�y podstawowe metody programu takie jak �MakeATurn�, �InicialiseTheWorld� oraz metody pomocnicze, np. �GetNearbyEmptyLocation�.

Klasa Organism - podstawowa klasa abstrakcyjna,  od kt�rej dziedziczy� b�d� zwierz�ta i ro�liny. Zawiera takie pola jak �CoordX�, �CoordY�, �Age�, �Initiative�, �Strength�. Wymusza� b�dzie wykorzystanie metody �Action�, �Collision �, �Reproduction� itd.

Klasa Animal i klasy dziedzicz�ce - dziedziczy po klasie Organism, przy czym w klasie Action obiekty tej klasy wykonuj� ruchy po planszy. 

Klasa Plant i klasy dziedzicz�ce - dziedziczy po klasie Organism, przy czym w klasie Action obiekty tej klasy pozostaj� na swoich miejscach, za to z pewnym prawdopodobie�stwem sukcesu dokonuj� reprodukcji - rozsiewaj� si�. 



Lista zada�:

Jacek Sudak:
Klasa World 
Funkcja automatycznego wykonywania kolejnych tur w okre�lonych odst�pach czasowych 


Pawe� Lipi�ski
Klasa Organism    
Klasa Animal, Plant i ich pochodne

Miko�aj Kr�l:
Mo�liwo�� zapisu do pliku i wczytania z pliku stanu gry
Mo�liwo�� odtworzenia stanu gry w zakresie ostatnich 5 tur

WPF - jeszcze si� oka�e. W p�niejszym pliku zamie�cimy faktyczny podzia�.