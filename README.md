
# StudIP-Downloader 
Dieses Repository ist ein For von yannik995/StudIP-Downloader.

Dieser Fork ergänzt das Original um den Login mittels Selenium und ein Dockerfile mit regelmäßigen Download der Dateien aus StudIP.

Ein kleines Tool, um die Dateien aller Kurse aus dem StudIP herunterzuladen.


### Wie wird das Tool verwendet?

1. Voraussetzungen: docker + docker-compose
2. Variablen in docker-compose.yml ausfüllen oder die Environment Variablen setzen
3. docker-compose up in SolutionItems
4. Nun werden alle Dateien heruntergeladen

In der Standardeinstellung wird alle 60 Minuten ein Download gestartet.



Es werden immer nur die neuen oder geänderten Dateien heruntergeladen.


