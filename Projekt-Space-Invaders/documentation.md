# Space Invaders WPF

Imitace klasícké retro hry Space Invaders ve WPF.

## Window: `MainWindow`

Hlavní okno aplikace se čtyřmi zobrazeními:

### 1. `Menu` (*Grid*)

Hlavní menu s názvem a tlačítky pro začátek hry, přesun na leaderboard a ukončení hry.

### 2. `Game` (*Canvas*)

Hlavní zobrazení hry s počítadlem skóre.

#### Hlavní Metody

- `RenderGame` - načte hlavní prvky hry a spustí `gameTimer`, který vyvolává změnu stavu hry každých 16 milisekund (cca 60 fps)
- `GameLoop` - jeden snímek (tick) hry. Vypočítává posuny hráče, nepřátel a střel, kontroluje kolize, a volá metodu pro střelbu nepřátel. Případně nechá generovat novou vlnu nepřátel, nebo spouští game over screen.

### 3. `GameOver` (*Grid*)

Zobrazuje obrazovku po konci hry, s možností uložit si své skóre pod přezdívkou, nebo jen odejít do menu.

### 4. `Leaderboard` (*Grid*)

Zobrazuje uložené skóre v přehledné tabulce.

#### Metody

- `SaveScore` - načte `leaderboard.json` a přidá do něj nové skóre
- `RenderBoard` - načte `leaderboard.json` a zformátuje data do tabulky

## UserControl: `PlayerShip`

Reprezentuje hráčskou loď a stará se o její hitbox.

### Vlastnosti
- `Speed` (*double*) - počet pixelů o který se loď posune za jeden frame
- `Hitbox` (*Rect*) - hitbox lodi pro kontrolu kolizí

### Metody
- `UpdateHitbox` - přepočítává pozici hitboxu na pozici lodi

## UserControl: `EnemyShip`

Reprezentuje nepřátelskou loď a stará se o její hitbox.

### Vlastnosti
- `ShipType` (*ShipType*) - hodnota enumerace určující typ lodi (hodnotu, šířku, sprite)
- `Value` (*int*) - skóre, které hráč získá za zneškodnění lodi
- `Speed` (*double*) - počet pixelů o který se loď posune za jeden frame
- `Hitbox` (*Rect*) - hitbox lodi pro kontrolu kolizí

### Metody
- `SwitchDirection` - obrácení směru pohybu
- `Move` - posunutí lodi
- `MoveDown` - posunutí lodi směrem dolů
- `UpdateHitbox` - přepočítává pozici hitboxu na pozici lodi
- `IsOutOfBounds` (*bool*) - určuje, zda je loď mimo obrazovku

## UserControl: `Bullet`

Reprezentuje střelu a stará se o její hitbox.

### Vlastnosti
- `IsFromPlayer` (*bool*) - určuje zda je střela od hráče či nikoliv (směr, barvu)
- `Speed` (*double*) - počet pixelů o který se střela posune za jeden frame
- `Hitbox` (*Rect*) - hitbox střely pro kontrolu kolizí

### Metody
- `Move` - posunutí střely
- `UpdateHitbox` - přepočítává pozici hitboxu na pozici střely
- `IsOutOfBounds` (*bool*) - určuje, zda je střela mimo obrazovku