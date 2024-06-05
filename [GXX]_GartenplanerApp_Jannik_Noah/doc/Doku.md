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
|Sound Probleme behoben|Noah|21.05.2024|
|Fixed User Login Bug & Bilder Transparent|Jannik|22.05.2024|
|Custom Control hinzugefügt & Chemie Funktion hinzugefügt|Noah|22.05.2024|
|Chmie Progess Bar hinzugefügt|Jannik|25.05.2024|
|Verschiedene Anischten bei den Beeten hinzugefügt|Jannik|25.05.2024|
|Profilbild geändert|Jannik|25.05.2024|
|Löschen Button hinzugefügt (ohne Funktion)|Noah|26.05.2024|
|Wasserstand und durchschnittliche Werte von Chemie & Wasser hinzugefügt|Jannik|26.05.2024|
|Warnnachrichten hinzugefügt|Jannik|26.05.2024|
|Pflanzen löschen|Noah|26.05.2024|
|Beete löschen|Noah|26.05.2024|
|Wasserstand kann nicht unter 0% gehen|Jannik|26.05.2024|
|Bewässerungs-Funktion hinzugefügt|Jannik|26.05.2024|
|Formel von Chemie verändert|Noah|26.05.2024|
|Logging Klasse hinzugefügt|Noah|04.06.2024|
|PDF Export Funktion hinzugefügt|Noah|04.06.2024|
|Bewässerungs-Funktion verändert|Noah|04.06.2024|
|Regenvorhersage Diagramm hinzugefügt & Automatische Bewässerung (Beta-Version)|Jannik|04.06.2024|

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

### 21.05.2024
Sound Probleme behoben.

### 22.05.2024
Fixed Bug --> Man konnte gleichen Name (wie schon vorhanden) beim User Login verwenden. Daher wird jetzt eine Exception (MessageBox) geworfen und man muss die Daten neu eingeben.
Die Bilder von den Pflanzen wurden noch transparent gemacht.
Neues Funktion --> Custom Control für die Beete wurde hinzugefügt sowie eine neue Chemie Funktion für die Beete.

### 25.05.2024
Neue Progess Bar mit der Chemie --> Nuget Packet "Syncfusion.SfProgressBar.WPF"
Verschiedene Ansichten bei den Beeten sind nun verfügbar (Standard: einfach nur die Vorschau vom Beet, Chemie: zeigt, wie gut die Pflanzen zueinander passen, Wasser: zeigt den aktuellen Wasserstand an)
Fixed Bug --> Good & BadConnections wurden nicht richtig aus der DB ausgelesen.
Profilbild von Benutzer wurde geändert.

### 26.05.2024
Beete und Pflanzen können von nun an gelöschte werden.
Jetzt kann der Wasserstand der Beete angezeigt werden und auf dem Dashboard findet man die Durchschnittswerte von Wasser und Chemie.
Es werden Warnungen angezeigt (auf dem Dashboard unter Warnungen), wenn die Chemie oder das Wasser unter einen gewissen Wert gefallen sind.

### 29.05.2024
Der Wasserstand kann nicht mehr unter 0% sinken und man kann die Beete jetzt eigenständig mit einem Button bewässern (im Beet-Menü).
Die Formel für die Berechnung der Chemie der Beete wurde geändert.

### 04.06.2024
Logging Klasse und eine PDF Export Funktion wurde hinzugefügt und die Bewässerungs-Funktion verändert.
Ein Regenvorhersagen-Diagramm ist nun auf dem Dashboard ersichtlich und eine Automatische Bewässerungs-Funktion wurde hinzugefügt. Diese sollte, wenn es in den nächsten 24h eine gewisse Menge Wasser regnet, die Beete automatisch bewässern. Die Funktion befindet sich jedoch noch in der **Beta Phase** und es kann noch zu Problemen kommen.

## Nuget Packages
+ LiveCharts.Wpf
    + Für die Regenvorhersage
    + https://www.nuget.org/packages/LiveCharts.Wpf/0.9.7?_src=template
+ Microsoft.Data.Sqlite & Microsoft.Data.Sqlite.Core
    + Für die DB-Anbindung
    + https://www.nuget.org/packages/Microsoft.Data.Sqlite/8.0.4?_src=template
+ OpenMeteo.dotnet
    + Für die Wetter-Daten
    + https://www.nuget.org/packages/OpenMeteo.dotnet/2.0.0?_src=template
+ QuestPDF
    + Für die PDF-Export-Funktion
    + https://www.nuget.org/packages/QuestPDF/2024.3.10?_src=template
+ Serilog & Serilog.Sinks.Console & Serilog.Sinks.File
    + Für das Logging
    + https://www.nuget.org/packages/Serilog.Sinks.Console/5.0.1?_src=template
+ Syncfusion.SfProgressBar.WPF
    + Für die Progess Bar von Chemie und Wasser
    + https://www.nuget.org/packages/Syncfusion.SfProgressBar.WPF/25.2.5?_src=template
+ System.Data.SQLite & System.Data.SQLite.Core
    + Für die DB-Anbindung
    + https://www.nuget.org/packages/System.Data.SQLite/1.0.118?_src=template


