using System;
using SudokuGame;

class BoardHandler
{
    public static int[,] board = new int[9, 9];
    public static int[,] solvedBoard = new int[9, 9];
    private List<(int, int)> fixedCells { get; set; }
    bool willPrint = false;

    public BoardHandler(int[,] emptyBoard)
    {
        board = emptyBoard;
        fixedCells = new List<(int, int)>();
    }

    public void NewBoard(string difficulty)
    {
        FillBoard(difficulty);
        PrintBoard(board, null, fixedCells);
    }

    public void UpdateBoard(int value, int row, int col)
    {
        UpdateCell(value, row, col);
        PrintBoard(board, null, fixedCells);
    }

    public void CheckBoard()
    {
        List<(int, int)> wrongCells = new();

        for (int row = 0; row < 9; row++)
        {
            for (int col = 0; col < 9; col++)
            {
                if (board[row, col] != 0 && board[row, col] != solvedBoard[row, col])
                {
                    wrongCells.Add((row, col));
                }
            }
        }
        PrintBoard(board, wrongCells, null);
    }

    public void FillBoard(string difficulty)
    {
        Random random = new Random();
        int values = 0;

        while (values < 5)
        {
            int row, col, value = random.Next(1, 10);

            if (values == 0)
            {
                row = 0;
                col = 0;
            }
            else if (values == 1)
            {
                row = 0;
                col = 1;
            }
            else if (values == 2)
            {
                row = 0;
                col = 2;
            }
            else
            {
                row = random.Next(9);
                col = random.Next(9);
            }

            if (board[row, col] == 0)
            {
                if (Solver.IsValidCell(board, value, row, col))
                {
                    board[row, col] = value;
                    values++;
                }
            }
        }

        int r = 0, c = 0;
        var solveResult = Solver.SolveBoard(board, willPrint, r, c);
        solvedBoard = solveResult.solvedBoard; // Save the solved board for later use when checking the board.

        int[,] tempBoard = (int[,])solveResult.solvedBoard.Clone();

        int difficultyValue = 0;
        if (difficulty == "easy")
        {
            difficultyValue = 81 - 40; // This number is how many numbers will be removed. Lower numbers removed from board for easier game.
        }
        else if (difficulty == "medium")
        {
            difficultyValue = 81 - 30;
        }
        else if (difficulty == "hard")
        {
            difficultyValue = 81 - 25;
        }
        else
        {
            Console.WriteLine("Error. Cannot determine difficulty value.");
        }

        bool cellsFilled = false;
        int counter = 1;

        while (!cellsFilled)
        {
            int row = random.Next(9);
            int col = random.Next(9);

            if (tempBoard[row, col] != 0)
            {
                int previousValue = tempBoard[row, col];
                tempBoard[row, col] = 0;

                int r1 = 0, c1 = 0;
                var solveResult1 = Solver.SolveBoard(tempBoard, willPrint, r1, c1);

                if (!solveResult1.success)
                {
                    tempBoard[row, col] = previousValue;

                    for (int i = 0; i < 9; i++)
                    {
                        for (int j = 0; j < 9; j++)
                        {
                            if (tempBoard[i, j] != 0)
                            {
                                fixedCells.Add((i, j));
                            }
                        }
                    }
                    cellsFilled = true;
                }
                else if (counter >= difficultyValue)
                {
                    tempBoard[row, col] = previousValue;

                    for (int i = 0; i < 9; i++)
                    {
                        for (int j = 0; j < 9; j++)
                        {
                            if (tempBoard[i, j] != 0)
                            {
                                fixedCells.Add((i, j));
                            }
                        }
                    }
                    cellsFilled = true;
                }
                else
                {
                    counter++;
                }
            }
        }
        board = (int[,])tempBoard.Clone();
    }

    private void UpdateCell(int value, int row, int col)
    {
        board[row - 1, col - 1] = value;
    }

    public bool IsCellEditable(int row, int col)
    {
        return !fixedCells.Any(cell => cell == (row, col));
    }

    public static void PrintBoard(int[,] board, List<(int, int)>? wrongCells = null, List<(int, int)>? fixedCells = null)
    {
        Console.WriteLine("\n    1 2 3   4 5 6   7 8 9  ");
        Console.WriteLine("   ----------------------- ");

        for (int row = 0; row < 9; row++)
        {
            if (row == 3 || row == 6)
            {
                Console.WriteLine("  |-------|-------|-------|");
            }
            Console.Write((row + 1) + " | ");

            for (int col = 0; col < 9; col++)
            {
                if (col == 2 || col == 5)
                {
                    if (wrongCells != null && wrongCells.Any(cell => cell == (row, col)))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(board[row, col]);
                        Console.ResetColor();
                        Console.Write(" | ");
                    }
                    else if (fixedCells != null && fixedCells.Any(cell => cell == (row, col)))
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write(board[row, col]);
                        Console.ResetColor();
                        Console.Write(" | ");
                    }
                    else
                    {
                        Console.Write(board[row, col] + " | ");
                    }
                }
                else
                {
                    if (wrongCells != null && wrongCells.Any(cell => cell == (row, col)))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(board[row, col]);
                        Console.ResetColor();
                        Console.Write(" ");
                    }
                    else if (fixedCells != null && fixedCells.Any(cell => cell == (row, col)))
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write(board[row, col]);
                        Console.ResetColor();
                        Console.Write(" ");
                    }
                    else
                    {
                        Console.Write(board[row, col] + " ");
                    }
                }
            }
            Console.WriteLine("|");
        }
        Console.WriteLine("   ----------------------- ");
    }
}