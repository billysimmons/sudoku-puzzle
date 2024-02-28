namespace SudokuGame
{
    class GameController
    {
        static void PrintInstructions()
        {
            Console.WriteLine("INSTRUCTIONS:");
            Console.WriteLine("Please first enter your intended difficulty for the game (easy, medium, hard)");
            Console.WriteLine("A board will then be generated. Where 0's depict empty cells.");
            Console.WriteLine("To update a cell value, simply enter the number followed by the coordinates inside a bracket (row, col).");
            Console.WriteLine("For example, if your number is 4 and you want to change a cell at the coordinates (3,4), simply enter:\n");
            Console.WriteLine("4 (3,4)\n");
            Console.WriteLine("The board will then update to reflect your change.");
            Console.WriteLine("To view instructions at anytime, enter \"instructions\".");
            Console.WriteLine("To quit at any time, enter \"quit\".");
            Console.WriteLine("To see the solved solution, enter \"solve\".");
            Console.WriteLine("Have fun and Good Luck!\n\n");
        }

        static bool ValueEntryCheck(BoardHandler boardHandler, string entry, out int value, out int row, out int col)
        {
            if (IsValidEntry(entry, out value, out row, out col) && boardHandler.IsCellEditable(row - 1, col - 1))
            {
                return true;
            }

            Console.WriteLine("Eror. Invalid Input. Try again\n");
            return false;
        }

        static bool IsValidEntry(string entry, out int value, out int row, out int col)
        {
            value = 0;
            row = 0;
            col = 0;

            entry = entry.Replace(" ", "");

            if (entry.Length != 6)
            {
                return false;
            }

            if (!int.TryParse(entry.Substring(0, 1), out int intValue) ||
                !int.TryParse(entry.Substring(2, 1), out int intRow) ||
                !int.TryParse(entry.Substring(4, 1), out int intCol))
            {
                return false;
            }

            if (intValue < 0 || intRow < 0 || intCol < 0)
            {
                return false;
            }

            value = intValue;
            row = intRow;
            col = intCol;

            return true;
        }


        static bool DifficultyEntryCheck(string difficulty)
        {
            if (difficulty == "easy" || difficulty == "medium" || difficulty == "hard")
            {
                return true;
            }
            return false;
        }

        static void Main()
        {

            int[,] emptyBoard = new int[9, 9];
            BoardHandler boardHandler = new BoardHandler(emptyBoard);

            bool quit = false, validDifficulty = false;

            Console.WriteLine("WELCOME TO SUDOKU!\n");
            PrintInstructions();

            while (!validDifficulty)
            {
                Console.Write("Enter difficulty (easy, medium, hard): ");
                string difficulty = Console.ReadLine();
                Console.WriteLine();

                difficulty = difficulty.Replace(" ", "");


                if (difficulty != null && DifficultyEntryCheck(difficulty))
                {
                    boardHandler.NewBoard(difficulty);
                    validDifficulty = true;
                }
                else
                {
                    Console.WriteLine("Invalid Difficulty. Try again.\n");
                }
            }

            while (!quit)
            {
                string entry = Console.ReadLine();

                if (entry != null)
                {
                    switch (entry)
                    {
                        case "instructions":
                            PrintInstructions();
                            break;

                        case "quit":
                            quit = true;
                            break;

                        case "check":
                            boardHandler.CheckBoard();

                            break;

                        case "solve":
                            int solveRow = 0, solveCol = 0;
                            bool willPrint = true;

                            Solver.SolveBoard(BoardHandler.board, willPrint, solveRow, solveCol);
                            break;

                        default:
                            int value, row, col;

                            if (ValueEntryCheck(boardHandler, entry, out value, out row, out col))
                            {
                                boardHandler.UpdateBoard(value, row, col);
                            }
                            break;

                    }
                }
            }
        }
    }
}