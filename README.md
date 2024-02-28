Sudoku Puzzle
=============

This 

Features
--------

-   Solver: A Sudoku solver using a recursive backtracking algorithm.
    - The recursive backtracking algorithm systematically fills Sudoku cells, exploring valid digit choices for each empty cell, backtracking when necessary until a solution is found or all possibilities are exhausted.
-   Board Creation: Boards are created based on the difficulty level provided by the user.
    - First, random numbers are placed throughout the board to ensure each puzzle created differs from the last. The board is then solved, where a set amount of cells is set back to 0 based on the difficulty level. 
-   Game Mode: An interactive game mode that allows you to play the generated Sudoku puzzles.
    - The board is interacted with via coordinates, where 0's represent empty cells. Instructions are included to assist users in playing the game.
-   Cell Checking: Cell checking to identify invalid cells by comparing them to the solved board.

Getting Started
---------------

### Prerequisites

-   .NET SDK installed on your machine.

### Installation

1.  Clone the repository:

    bashCopy code
    `https://github.com/billysimmons/sudoku-puzzle.git`

2.  Navigate to the project directory:

    bashCopy code
    `cd sudoku-puzzle`
    `cd SudokuPuzzle`

3.  Build the project:

    bashCopy code
    `dotnet build`

4.  Run the application:

    bashCopy code
    `dotnet run`

