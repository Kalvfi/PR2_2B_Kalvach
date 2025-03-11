# Dokumentace projektu

## Hlavní metody
- `DisplayExplorer` - implementuje hlavní obrazovku pro prohlížení struktury obchodníků
- `DisplayFile` - implementuje obrazovku pro práci se soubory
- `HandleInput` - zpracovává uživatelské vstupy z klávesnice
- `CheckExit` - kontroluje stav ukládání při ukončení aplikace

## Popis funkcionality
1. **Prohlížeč** - umožňuje procházet stromovou strukturu obchodníků, zobrazuje veškeré informace o vybraném obchodníkovi. Umožňuje přidat/odebrat obchodníka do/z aktuálně načteného souboru.
2. **Správa souborů** - umožňuje vytvářet, načítat a ukládat seznamy obchodníků do souborů ve formátu .txt.

Program využívá tlačítka, mezi kterými lze přepínat pomocí šipek a aktivovat je pomocí mezerníku. Na obou obrazovkách lze program opustit pomocí escape.

## Třída: `Salesman`

### Vlastnosti
- `ID` (*int*) - unikátní identifikátor obchodníka
- `Name` (*string*) - jméno obchodníka
- `Surname` (*string*) - příjmení obchodníka
- `Sales` (*int*) - prodeje konkrétního obchodníka
- `Subordinates` (*List\<Salesman>*) - seznam podřízených obchodníků

### Metody
- `BranchSales` - vypočítá celkové prodeje včetně všech podřízených
- `FindSuperior` - najde nadřízeného pro daného obchodníka
- `FindSalesman` - najde obchodníka podle `ID` (při načítání ze souboru)

## Abstraktní třída: `Button`

### Vlastnosti
- `Label` (*string*) - text, který se vypíše při vykreslení tlačítka
- `IsSelected` (*bool*) - indikátor, zda je tlačítko vybrané
- `Action` (*Action*) - delegát metody, kterou tlačítko volá

### Odvozené třídy
- `SalesmanButton` - tlačítko reprezentující obchodníka
- `FileButton` - tlačítko pro přidání/odebrání obchodníka do/ze souboru
- `NavigationButton` - tlačítko pro navigaci v aplikaci
- `BoolButton` - tlačítko reprezentující volbu Ano/Ne

### Funkcionalita
Každé tlačítko má svůj vizuální styl definovaný metodou `Draw` a chování definované pomocí `ExecuteAction`.

## Statická třída: `FileManager`

Poskytuje funkcionalitu pro práci se soubory v aplikaci.

### Statické vlastnosti
- `CurrentFileName` (*string*) - aktuálně otevřený soubor
- `FileContent` (*List\<Salesman>*) - obsah načteného souboru
- `IsSaved` (*bool*) - indikátor, zda jsou změny uloženy

### Metody
- `CreateFile` - vytvoří nový prázdný soubor
- `LoadFile` - načte soubor s obchodníky
- `SaveFile` - uloží aktuální seznam obchodníků do souboru
- `AddToFile` - přidá obchodníka do seznamu
- `RemoveFromFile` - odebere obchodníka ze seznamu
- `SerializeSalesmen` - převede seznam obchodníků na textový řetězec pro uložení
- `DeserializeSalesmen` - převede textový řetězec na seznam obchodníků při načtení

`FileManager` pracuje se soubory ve formátu .txt, kde každý řádek obsahuje `ID` obchodníka.
