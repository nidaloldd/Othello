# Othello (Reversi)

> Othello is a two-player strategy board game played on an 8x8 board. Players take turns placing discs of their color on the board, with the goal of having the most discs of their color at the end of the game. Each move must surround and flip one of the opponent's discs to the player's color. The game was written in C# using the .NET WPF framework.

## Game description
[English](https://en.wikipedia.org/wiki/Reversi) \
[Hungarian](https://hu.wikipedia.org/wiki/Fon%C3%A1kol%C3%B3s)

## How to play

### Two game modes:

- Player vs. Player:
  - Two players using the same computer and same application take turns playing the game one after the other.
- Player vs. AI:
  - The AI will always choose the move that will flip the most disks.
  - If there are multiple moves with the same outcome, it will randomly select one.

> In either game mode, the starting player is chosen at random.

### Score calculation

- In both game modes, both players will receive points for their moves.
- Scores are calculated based on the following criteria:
  - All disks have a common base value that is awarded for flipping them over.
  - If multiple disks are flipped at once, the score is calculated using this formula:
    - `1 * disk_value + 2 * disk_value + ... + n * disk_value`, where `n` is the number of flipped disks.
  - If a move affects two axes (e.g., horizontal and diagonal), the score is doubled. If it affects all three axes (horizontal, vertical, diagonal), the score is tripled.
  - If one player flips all discs to their color, the winner receives bonus points based on this formula:
    - `100 * disk_value * n`, where `n` is the number of all disks on the board.
  - If a player passes their turn, they get `0` points.

### After the game
- The user has the option to either start a new game, in which case they will be taken back to the game mode selection window, or view the High Scores window.
- Finished game details are stored in a JSON file and can be viewed in descending order based on scores in the High Scores window.
> The high scores can be cleared at any time by pressing a button (confirmation is required).

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
