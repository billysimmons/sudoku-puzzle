using System;
using SudokuGame;

public class Solver
{
    public static bool IsValidCell(int[,] board, int value, int row, int col)
    {
        for (int i = 0; i < 9; i++)
        {
            if (board[row, i] == value || board[i, col] == value)
            {
                return false;
            }
        }

        int startRow = row - (row % 3);
        int startCol = col - (col % 3);

        for (int r = startRow; r < startRow + 3; r++)
        {
            for (int c = startCol; c < startCol + 3; c++)
            {
                if (board[r, c] == value)
                {
                    return false;
                }
            }
        }
        return true;
    }

    public static (bool success, int[,] solvedBoard) SolveBoard(int[,] board, bool willPrint, int row = 0, int col = 0)
    {
        int[,] solvedBoard = (int[,])board.Clone();

        if (row == 9)
        {
            if (willPrint)
            {
                BoardHandler.PrintBoard(solvedBoard);
            }
            return (true, solvedBoard);
        }
        else if (col == 9)
        {
            return SolveBoard(solvedBoard, willPrint, row + 1, 0);
        }
        else if (solvedBoard[row, col] != 0)
        {
            return SolveBoard(solvedBoard, willPrint, row, col + 1);
        }
        else
        {
            for (int value = 1; value < 10; value++)
            {
                if (IsValidCell(solvedBoard, value, row, col))
                {
                    solvedBoard[row, col] = value;

                    var (success, newBoard) = SolveBoard(solvedBoard, willPrint, row, col + 1);
                    if (success)
                    {
                        return (true, newBoard);
                    }

                    solvedBoard[row, col] = 0;
                }
            }
            return (false, solvedBoard);
        }
    }

    public static bool IsSolved(int[,] board)
    {
        for (int r = 0; r < 9; r++)
        {
            for (int c = 0; c < 9; c++)
            {
                if (!IsValidCell(board, board[r, c], r, c))
                {
                    return false;
                }
            }
        }
        return true;
    }
}

