# MyMessagePortal

MyMessagePortal jest prostą aplikacją pozwalającą na publikowanie wiadomości na jednym z dostępnych kanałów.

## Instalacja:

W celu uruchomienia aplikacji należy stworzyć nową bazę danych lub odtworzyć bazę z przykładowymi danymi.

### I. Nowa baza danych 

1. utworzenie nowej bazy danych np. MyMessagePortalDb korzystając ze skryptu ```create database MyMessagePortalDb```
1. podmiana bazy w pliku konfiguracyjnym `appsettings.xml` jeżeli jest to wymagane
1. wykonenie `Update-Database` w VisualStudio z poziomu **Package Manager Console**
1. dodanie przykładowych danych po przez wykonanie skryptu **\scripts\Sample_data.sql** 

### II. SampleDB

1. odworzonie bazy z backupu znajdującego się w katalogu **SampleDB**

## Przykładowe dane:

Przykładowe dane zawierają:
1. dwóch użytkowników (z hasłami: _Qazwsx1@_):
    * `marcin`
    * `tomek`
1. zestaw przykładowych:
    * `kanałów`
    * `wiadomości`
    
## Dostępne moduły:

### Kanały

Moduł pozwala na tworzenie, usuwanie i przeglądanie stworzonych kanałów. Każdy z kanałów po stworzeniu jest wyróżniony losowym kolorem i może zostać oznaczony przez jakiego kolwiek użytkownika jako obserwowany. Kanał mozę zostać usunięty tylko przez użytkownika, który go stworzył. Do każdego z kanałów użytkownicy mogą dodwać swoje wiadomości.

#### Dostępne funkcjonalności
* Tworzenie nowego kanału
* Usuwanie istniejącego kanału, jeżeli nie jest on domyślnym kanałem użytkownika i stworzył go uzytkownik, ktory próbuje go usunąć
* Przeglądanie kanałów
* Oznaczanie kanału jako obserwowanego

### Wiadomości

Moduł pozwala na tworzenie, usuwanie i przeglądanie stworzonych wiadomości. Wiadomości mogą być dodawane do dowolnego kanału i usuwane z niego w przeciągu 10 minut od czasu ich dodania przez użytkownka, który ja stworzył.

#### Dostępne funkcjonalności
* Tworzenie nowych wiadomości
* Usuwanie stworzonej wiadomości o ile nie minęło 10 minut od jej stworzenia
* Przeglądanie stworzonych wiadomości

## Korzystanie z aplikacji:

Do skorzystania z aplikacji użytkownicy nie potrzebują żadnych specjalnych uprawnień. Każdy zareejstrowany i zalogowany użytkownik będzie posiadał swój domyślny kanał, do którego zarówno on jak i inni użytkownicy mogą dodawać nowe wiadomości. Po zalogowaniu każdy z użytkowników na swojej stronie domowej będzie miał wyświetlone wszystkie obserwowane kanały.

##### cdn.
