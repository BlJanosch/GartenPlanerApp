# Dokumentation Gartenplaner App

## Arbeitsaufteilung

|Arbeitsschritt|Beauftragter|Datum|
|:------------:|:----------:|:------:|
|Design für Dashboard erstellen|Jannik|23.04.2024|
|Erste Funktionen einfügen (Wetter, Uhr)|Noah|23.04.2024|
|User Fenster hinzugefügt (Wetter funktioniert nicht mehr)|Jannik|24.04.2024|
|User Login hinzugefügt und Wetter Bug gefixed|Jannik|24.04.2024|
|Passwort ändern können|Noah|25.04.2024|
|User kann Standort und Namen ändern|Jannik|25.04.2024|
|User kann sich nun auch wieder Ausloggen|Jannik|26.04.2024|
|Beete Menü Prototyp|Noah|28.04.2024|
|Window zum Beete hinzufügen|Noah|28.04.2024|
|Beete Menü Bug beheben|Jannik|29.04.2024|
|Beete Hinzufügen Designen und Beete Übersicht hinzufügen|Jannik|30.04.2024|
|Neue Funktion Info Button bei den Beeten|Jannik|01.05.2024|
|Pages für die einzelnen Menü-Punkte wurde hinzugefügt|Noah|02.05.2024|
|Fixed User Menü Background Bug & Beete Menu Bug|Jannik|03.05.2024|
|Beete Bearbeiter wurde hinzugefügt (mit ein paar Bugs)|Jannik|05.05.2024|
|Beete Bearbeiter wurde optimiert und verschönert|Noah|05.05.2024|
|Beete Menü Bug wurde behoben|Jannik|05.05.2024|
|Fixed Login OK Button & User Menu Logout Button Bug|Jannik|08.05.2024|
|Default Name für Beet hinzugefügt|Jannik|08.05.2024|
|Plus Buttons für Beete hinzugefügt|Jannik|08.05.2024|
|WindowAddPlant wurde hinzugefügt|Jannik|08.05.2024|
|Styles.cs hinzugefügt und MainUser Bug gefixed|Jannik|08.05.2024|
|PagePlantMenu.xmal.cs & Plant.cs & PlantManager.cs hinzugefügt|Jannik|08.05.2024|
|Datenbank hinzugefügt und mit Pflanzen Menü verbunden|Noah|10.05.2024|
|Pflanzen Auswähl-Funktion hinzugefügt|Noah|13.05.2024|
|Neue Tables zur DB hinzugefügt|Jannik|13.05.2024|
|Ein paar Änderunge vorgenommen|Noah|13.05.2024|
|Merge Konflikt behoben|Noah|14.05.2024|
|Styles.cs gefixed|Noah|15.05.2024|
|GetFontStyle zu Styles.cs hinzugefügt|Jannik|15.05.2024|
|GetAllPlants und GetAllBeete aktualisiert|Jannik|15.05.2024|
|Plant und PlantManager aktualisiert|Jannik|15.05.2024|
|Funktion zum Hinzufügen von Pflanzen zu Beeten hinzugefügt|Noah|15.05.2024|
|Merge Konflikt behoben|Noah|15.05.2024|
|Plant.cs und Datenbank aktualisiert|Jannik|15.05.2024|
|X und Y Koordinaten von Pflanzen im Beet geändert|Noah|15.05.2024|
|User Login Bug gefixed|Jannik|15.05.2024|
|Pflanzen Info ist nun verfügbar|Noah|15.05.2024|
|Pflanzen im Beet haben nun ein Bild|Noah|16.05.2024|
|Beete werden von nun in der DB gespeichert|Jannik|16.05.2024|
|Merge Konflikt behoben|Jannik|16.05.2024|
|Errors gefixed|Jannik|16.05.2024|
|Registrieren und Anmelden Option hinzugefügt|Jannik|17.05.2024|
|PageUserSignIn hinzugefügt|Jannik|17.05.2024|
|SignIn hat nun Funktionen|Jannik|18.05.2024|
|Background Music hinzugefügt|Noah|18.05.2024|
|Merge Konflikt behoben|Noah|18.05.2024|
|Falscher Name und Passwort Bug behoben|Jannik|19.05.2024|
|Internet Bug behoben|Jannik|19.05.2024|
|Zwei User mit demselben Namen Bug behoben|Jannik|19.05.2024|
|Beete wurden nicht angezeigt Bug behoben|Jannik|19.05.2024|
|Bugs gefixed|Noah|19.05.2024|
|Merge Konflikt behoben|Noah|19.05.2024|
|Alter User Bug behoben|Jannik|19.05.2024|
|Merge Konflikt behoben|Jannik|19.05.2024|
|Plant Menu neu Designed|Jannik|19.05.2024|

## Tagebuch

### 24.04.2024
Programm um User-Login erweitert --> Wenn noch keine Daten vorhanden sind wird eine .csv Datei erstellt und man muss sich anmelden. Anderenfalls werden die Daten aus der .csv Datei geladen und man muss sich nicht mehr anmelden.

### 25.04.2024
Programm um ein User-Login erweitert. Daten werden searilisiert und in .csv Datei gespeichert (Passwort wird gehashed). Passwort kann unter Reiter YOU geändert werden (man muss alter Passwort angeben um ein neues erstellen zu können). Weitere Funktion: User kann seinen Namen ändern sowie sein Standort. Mit Beet angefangen d.h. Design ausprobiert...

### 26.04.2024
Programm um ein Logout System erweitert. User kann sich nun ausloggen und wieder neu anmelden. Die Datei Login.csv wird dabei wieder zurückgesetzt und neu überschrieben.

### 28.04.2024
Beete-Menü hat nun Funktion und es gibt ein neues Fenster zum Hinzufügen von neuen Beeten (Spalten, Reihen).

### 29.04.2024
Bug --> Wenn man von User-Menü zu Beete-Menü gewechselt hat, hat sich der Background nicht resetet. Dieser Bug wurde nun behoben.

### 30.04.2024
Beete Hinzufügen Fenster Designed und sobald man nun auf das Plus klickt erscheint ein neues "Quadrat" mit einer kurzen Übersicht (Name, kleiner Version vom Beet, Info Button). Plan --> Wenn man auf den Info Button klickt, soll der Beete-Designer geöffnet werden.

### 01.05.2024
Info Button hat nun Funktion und es erschein das ausgewählte Beet in detaillierter Ansicht.

### 02.05.2024
Programm wurde durch Pages verbesserter/übersichtlicher gemacht.

### 03.05.2024
Ein paar Bugs fixes --> Beim User Menu wurde der falsche Background angezeigt oder garnicht / Beete wurden nicht richtig angezeigt bzw. wie wir uns es vorgestellt hatten.

### 05.05.2024
BeetBearbeiter hinzugefügt mit ein paar Bugs, anschließend optimiert und verschönert und zu guter Letzt den Bug behoben.

### 08.05.2024
Bugs gefixed --> Login Button (OK) hat nicht ganz funktioniert bzw. wie er sollte und der Logout Button ebenso nicht. 
Neue Funktion --> Sobald kein Name beim Erstellen eines Beets eingegebn wird, wird ein Default-Name also Beet 1, ... vergeben.
Erweiterung --> Plus Buttons zum Hinzufügen von Pflanzen zu einem Beet.
WindowAddPlant wurde erstellt um die gewünschte Pflanze auswählen zu können.
Styles.cs wurde hinzugefügt um vorgefertigte Styles zu bekommen um sich somit Code zu sparen.
Probleme mit dem MainUser, dieses wurde jedoch behoben.
PagePlantMenu.xmal.cs & Plant.cs & PlantManger.cs wurden hinzugefügt und der Button für das Pflanzen Menü hat nun Funktion.

### 10.05.2024
Datenbank wurde hinzugefügt und mit dem Plant Menu verbunden --> sprich die Daten aus der DB wurde im Plant Menu angezeigt.

### 13.05.2024
Sobald nun eine Pflanze ins Beet eingefügt wird, wird ihr Name angezeigt.
Neuer Table zu der Datenbank (GartenPlaner.db) hinzugefügt --> tblUser und tblBeet
Programm wurde noch optimiert und verbessert.

### 14.05.2024
Merge Konflikt wurde behoben.

### 15.05.2024
Der Fehler in der Datei --> Styles.cs wurde behoben.
GetFontStyle wurde zu Styles.cs hinzugefügt um einfach auf die Standard-Schriftart zuzugreifen zu können.
GetAllPlants gibt nun einen Liste mit Pflanzen zurück anstatt einen mit Strings und GetAllBeete wurde hinzugefügt um alle Beete aus der DB zu bekommen.
Plant hat nun auch eine ID und GetAllPlants gibt nun einen PlantManger zurück.
Von nun an kann man Beete und User-Daten in der DB speichern.
Funktion zu hinzufügen von Pflanzen zu Beeten wurde hinzugefügt/erweitert (mit Bild von Pflanze).
Merge Konflikt wurde behoben.
Es wurde versucht die Pflanzen von einem Beet in der DB zu speichern und von dort dann auch wieder zu lesen.
X und Y Koordinaten von den Pflanzen in den jeweiligen Beeten wurde geändert.
User Login Bug behoben --> Wurde mit einem User ein Beet erstellt und nacher ein anderer User erstellt hatte dieser immer noch die Beete vom vorherigen User.
Nun gibt es eine genaue Information zu den einzelnen Pflanzen im Pflanzen Menü.

### 16.05.2024
Bild-Funktion von den Pflanzen in den Beeten wurde erweitert.
Beete werden nun richtig mit den Pflanzen in der DB gespeichert.
Merge Konflikt wurde behoben.
Mehrere Errors wurden behoben.

### 17.05.2024
Jetzt gibt es einen Button um sich zu Registrieren und einem um sich anzumelden.
Einen Page um sich anzumelden wurde hinzugefügt (PageuserSignIn).

### 18.05.2024 
Nun kann man sich auch wirklich mit einem vorherigem User wieder anmelden.
Background Music wurde hinzugefügt und ein Merge Konflikt wurde behoben.

### 19.05.2024
Bug wurde gefixed --> Sobald man einen falschen Namen oder ein falsches Passwort eingegeben hat (beim Anmelden) ist das Programm abgestürzt.
Bug wurde gefixed --> Wenn man kein Internet ist das Programm nicht gestartet bzw. abgestürzt, da der Zugriff auf die Wetter API nicht möglich war. Jetzt wird, wenn man keine Internetverbindung hat, einfach N/A angezeigt.
Bug wurde gefixed --> Zwei User konnten den selben Namen haben.
Bug wurde gefixed --> Beete wurden nicht angezeigt, wenn man vom User Menü wieder ins Beete Menü gewechselt hat.
Merge Konflikt wurde behoben.
Bug wurde gefixed --> Nach dem Registrieren bzw. Anmelden wurde beim Programm immer noch der alte User benutzt.
Merge Konflikt wurde behoben.
Plant Menü wurde neu Designed (noch nicht ganz fertig).


