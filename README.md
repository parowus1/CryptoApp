# Changelog Aplikacji Kryptograficznej 

## [1.0.0] - 2025-10-12
### Dodane
- Zaimplementowano szkielet aplikacji okienkowej (Windows Forms) w C#.
- Zdefiniowano architekturę modułową (Core/UI) opartą na interfejsie ICipher, aby ułatwić dodawanie kolejnych algorytmów.
- **Implementacja Szyfru Cezara:** Dodano klasę `CaesarCipher` do szyfrowania/deszyfrowania tekstu i plików.
- Funkcjonalność szyfrowania i deszyfrowania tekstu z wykorzystaniem klucza.
- Funkcjonalność szyfrowania i deszyfrowania plików.

## [1.1.0] - 2025-10-20
### Dodane
Nowe Algorytmy Kryptograficzne:
- Wprowadzono klasę `VigenereCipher` (Szyfr Vigenère'a), jako drugi algorytm implementujący interfejs ICipher.
- Szyfr Vigenère'a operuje na kluczu słownym (string) i jest kompatybilny z łacińskim alfabetem (A-Z).
- Wprowadzono klasę `RunningKeyCipher` (Szyfr z Kluczem Bieżącym), jako trzeci algorytm implementujący interfejs ICipher.
- Algorytm ten jest wariantem Szyfru Vigenère'a i wymaga, aby klucz był co najmniej tak długi jak czysty tekst (po odrzuceniu znaków niebędących literami).

Dynamiczny Wybór Algorytmu (UI):
- Dodano kontrolkę ComboBox (listę rozwijaną) do formularza MainForm.cs, umożliwiającą użytkownikowi przełączanie między zaimplementowanymi algorytmami (Cezar, Vigenère) w czasie rzeczywistym.
- Zaimplementowano obsługę zdarzenia SelectedIndexChanged, która aktualizuje aktywnie używany obiekt _currentCipher.

Wskazówki dla Klucza:
- Dodano dynamiczną zmianę domyślnej wartości w polu klucza (txtKey) w zależności od wybranego algorytmu (np. '3' dla Cezara, 'SECRET' dla Vigenère'a, 'DŁUGI KLUCZ O DŁUGOŚCI WIADOMOŚCI' dla RunningKeyCipher).

### Zmienione
- Architektura MainForm.cs: Konstruktor został zmodyfikowany w celu inicjalizacji i powiązania listy algorytmów (_algorithms) z nową kontrolką cmbAlgorithms.
- Logika Plikowa: Operacje na plikach (EncryptFileAsync, DecryptFileAsync) zostały zaktualizowane, aby dynamicznie wywoływać metody aktualnie wybranego szyfru (_currentCipher).
- Wskazówki dla Klucza: Zaktualizowano metodę CmbAlgorithms_SelectedIndexChanged, aby dostosować sugerowany tekst klucza (txtKey.Text) dla Szyfru z Kluczem Bieżącym, informując użytkownika o wymaganym formacie.

## [1.2.0] - 2025-10-27
### Dodane
Nowy Algorytm Kryptograficzny:
- Wprowadzono klasę AESCipher (Advanced Encryption Standard, AES-256), stanowiącą czwarty algorytm implementujący ICipher.
- Implementacja Tekstowa: Używa trybu AES-256 GCM do szyfrowania tekstu, haszując klucz użytkownika za pomocą SHA256 i kodując wynik w Base64 (zawiera IV, Tag i szyfrogram).
- Implementacja Plikowa (Strumieniowa): Wprowadzono szyfrowanie bajtowe dla plików dowolnego typu z użyciem AesCryptoServiceProvider w trybie CBC. Pliki są przetwarzane strumieniowo (CryptoStream), a Wektor Inicjujący (IV) jest zapisywany na początku zaszyfrowanego pliku.

### Zmienione
- Architektura Kryptograficzna: Projekt przeszedł z wyłącznie szyfrów znakowych na architekturę obsługującą zarówno algorytmy znakowe (Cezar, Vigenère, Bieżący Klucz) jak i bajtowe/strumieniowe (AES).
- Inicjalizacja: Zaktualizowano MainForm.cs o nowy algorytm AES, ustawiając go jako domyślny.

## [1.3.0] - 2025-11-24
### Dodane
Asymetryczny Algorytm Kryptograficzny:
- Wprowadzono klasę RSACipher (RSA-2048), jako piąty i pierwszy algorytm klucza publicznego implementujący ICipher.
- Generowanie Kluczy: Dodano funkcję GenerateKeys() oraz przycisk w UI do generowania par kluczy publiczny/prywatny (w formacie XML).
- Implementacja RSA: Szyfrowanie i deszyfrowanie wykorzystuje wbudowaną klasę System.Security.Cryptography.RSA z bezpiecznym schematem wypełnienia OAEP (SHA-256).
- Zarządzanie Kluczami: Klucz publiczny jest ustawiany automatycznie w polu txtKey po generacji, a klucz prywatny kopiowany do schowka.
- Nowa Kontrolka UI: Dodano przycisk btnGenerateKeys do generowania par kluczy RSA.

### Zmienione
- Architektura Kryptograficzna: Projekt został rozszerzony o algorytmy asymetryczne, zmieniając koncepcję "klucza" w interfejsie UI (klucz RSA jest długim ciągiem XML).
- UI Dynamiczne: Zaktualizowano CmbAlgorithms_SelectedIndexChanged, aby ukrywać/pokazywać przycisk generowania kluczy w zależności od wybranego algorytmu.

## [1.4.0] - 2025-11-25
### Dodane
- System Logowania i Analizy: Wprowadzono LogManager i okno LogWindow.cs do rejestrowania i wyświetlania wszystkich operacji kryptograficznych.- Generowanie Kluczy: Dodano funkcję GenerateKeys() oraz przycisk w UI do generowania par kluczy publiczny/prywatny (w formacie XML).
- Analiza Krok po Kroku: Dodano funkcję wyświetlającą szczegółowy raport (algorytm, status, klucz) dla ostatniej wykonanej operacji.
- Rejestracja Zdarzeń: Rejestrowanie statusu (SUCCESS/ERROR) po każdej próbie szyfrowania/deszyfrowania tekstu i plików, oraz generowania kluczy.

