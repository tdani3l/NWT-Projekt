SignalR - Chat

Konfiguration:
Da die Anmeldeinformationen der Benutzer in einer SQL Datenbank
gespeichert werden, muss zunächst diese Datenbank erstellt werden.
Dazu muss das SQL-Skript "createUserDtbse.sql" im SQL-Server-Management
ausgeführt werden. Dort sind auch Default-User angelegt, mit denen man
sich für Testzwecke einloggen kann.
Der Connectionstring muss im Client in APP.config angepasst werden, um
dem Programm Zugriff auf die Datenbank zu geben.
Danach muss der Server gestartet werden und im Hintergrund laufen.
Schließlich kann man beliebig viele Clients starten.

Features:
- Registrierung und Anmledung zur Nutzung des Chats erforderlich
  (Es gibt Nutzernamen und Passwort Restriktionen)
- Graphische Oberfläche
- Datum und Absender der Nachricht ersichtlich
- Alle Chat-Teilnehmer sind durch eine "ONLINE-Liste" ersichtlich
  (Liste wird beim Ein- und Ausloggen aktualisiert)

Video:"Tutorial_Video.mkv"
Eine Walkthrough der Konfiguration und Nutzung