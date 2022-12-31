# Othello (Reversi)

> The strategy board game Othello made in C# with the .NET WPF framework.

## Game description
[English](https://en.wikipedia.org/wiki/Reversi) \
[Hungarian](https://hu.wikipedia.org/wiki/Fon%C3%A1kol%C3%B3s)

## How to play

### Two game modes:

- Player vs. Player:
  - Same computer, same application, taking turns one after another.
- Player vs. AI:
  - The AI will always pick the move where it's going to flip the most disks.
  - In case theres multiple identical outcome moves, it will pick one at random.

> In either mode, the starting player is chosen at random.

### Score calculation

- Both halves will get points for their moves in both game modes.
- Scores are calculated based on the following criteria:
  - All disks have a common base value that you're granted for flipping them over.
  - If you flip multiple disks at once, the score will be calculated using this formula:
    - `1 * disk_value + 2 * disk_value + ... + n * disk_value`, where `n` is the number of flipped disks.
  - Following the above calculations, if your move affected two axes (i.e. horizontal and diagonal), then the score is doubled. If it affected all 3 axes (horizontal, vertical, diagonal), then the score is tripled.
  - If one player happens to flip all disks to their colour, the winner gets bonus points based on this formula:
    - `100 * disk_value * n`, where `n` is the number of all disks on the board.
  - If a player passes their turn, they get `0` points.

### After the game
- The user is given the option to either start a new game, in which case they're taken back to the game mode selection window, or go to the High Scores window.
- All finished game details are stored in a JSON file, and can be seen in a descending order based on scores in the High Scores window.
> High scores can be cleared any time with the press of a button. (requires user confirmation)

## Screenshots
- Main Menu 
> <img src="https://i.imgur.com/UxbgKFQ.jpg" alt="MainMenu" width="300"/>
- Game Mode Selection
> <img src="https://i.imgur.com/JlNOBd2.jpg" alt="MainMenu" width="300"/>
- Name Input
> <img src="https://i.imgur.com/P5h83JV.jpg" alt="MainMenu" width="300"/>
- Game Window
> <img src="https://i.imgur.com/OVE60R7.jpg" alt="MainMenu" width="300"/>
- High Scores
> <img src="https://i.imgur.com/gw5mp0z.jpg" alt="MainMenu" width="300"/>