# ISEM - Insert SQL Easy Manager dla Insert GT
## Cel programu
SQLEasyManager to aplikacja okienkowa stworzona w technologii WPF (Windows Presentation Foundation), która umożliwia zarządzanie bazami danych SQL Server poprzez przyjazny interfejs graficzny. Aplikacja umożliwia administratorom łatwe zarządzanie dostępami do baz danych, dodawanie i usuwanie użytkowników dla wybranych baz.
## Funkjonalności
* Wyświetlanie baz danych: Lista wszystkich baz danych SQL Server dostępnych na wybranym serwerze.
* Zarządzanie użytkownikami:
  * Dodawanie użytkowników do roli db_owner dla wybranych baz danych.
  * Usuwanie użytkowników z roli db_owner.
  * Automatyczne tworzenie użytkownika w bazie danych, jeśli nie istnieje.
Usuwanie użytkownika z bazy danych po jego odłączeniu od roli db_owner.
* Filtrowanie baz danych:
  * Filtrowanie baz danych po nazwie.
  * Filtrowanie baz danych z dostępem dla wybranego użytkownika.
* Operacje grupowe:
  * Dodawanie lub usuwanie użytkowników z wielu baz danych jednocześnie.
  * Grupowe zaznaczanie lub odznaczanie baz danych.
* Informacje o bazie danych:
  * Wyświetlanie ważnych informacji o każdej bazie, takich jak NIP (numer identyfikacji podatkowej) oraz typ księgowości (Rachmistrz, Rewizor, Ryczałt).
  * Śledzenie właścicieli baz: Automatyczne śledzenie właścicieli baz danych w odniesieniu do wybranego użytkownika.

## Sposób użycia
### Główne funkcjonalności:
#### Ładowanie baz danych:

1. Po uruchomieniu aplikacja łączy się z serwerem SQL i ładuje listę dostępnych baz danych.
2. Każda baza danych wyświetla dodatkowe informacje, takie jak NIP, typ księgowości oraz inne szczegóły.
#### Zarządzanie użytkownikami:

1. Wybierz użytkownika z rozwijanej listy i przypisz go lub usuń z roli db_owner dla wybranych baz danych.
2. Aplikacja automatycznie tworzy użytkownika w bazie, jeśli nie istnieje.
3. Możesz całkowicie usunąć użytkownika z bazy danych.
#### Operacje grupowe:

Zaznacz kilka baz danych i wykonaj operacje grupowe, takie jak dodanie lub usunięcie użytkownika z wielu baz jednocześnie.

#### Dodatkowe funkcje:
Kopiowanie uprawnień użytkownika: Funkcja Kopiowanie użytkownika pozwala na skopiowanie uprawnień z jednego użytkownika do drugiego we wszystkich wybranych bazach danych.
Filtrowanie baz danych: Użyj pola wyszukiwania, aby szybko znaleźć wybraną bazę po nazwie lub filtrować bazy dostępne dla wybranego użytkownika.
 
## Technologie:
* C#: Język programowania używany do implementacji logiki aplikacji.
* WPF (Windows Presentation Foundation): Technologia służąca do tworzenia interfejsu użytkownika.
* SQL Server: System bazodanowy, którym zarządza aplikacja.
* ADO.NET: Używany do połączeń z bazą danych oraz wykonywania zapytań SQL.
* Plik konfiguracyjny aplikacji: Przechowuje informacje o serwerze oraz użytkownikach, w tym zaszyfrowane hasło użytkownika SQL.

## Licencja
Ten projekt jest dostępny na licencji MIT. Zobacz plik LICENSE po więcej informacji.

## Współpraca
Współpraca mile widziana! Jeśli chcesz wnieść swoje poprawki lub ulepszenia, wykonaj następujące kroki:

* Sforkuj repozytorium.
* Stwórz nową gałąź dla swojego rozwiązania.
* Złóż pull request, abyśmy mogli przejrzeć zmiany.

## Kontakt
W razie pytań lub sugestii, proszę o kontakt:

Dominik Kowalczyk: dominik99d14@gmail.com
