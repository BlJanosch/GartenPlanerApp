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