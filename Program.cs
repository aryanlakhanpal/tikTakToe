using System;

class TicTacToe
{
    static char[,] board = {
        { '1', '2', '3' },
        { '4', '5', '6' },
        { '7', '8', '9' }
    };
    static char currentPlayer = 'X';

    static void Main(string[] args)
    {
        int turns = 0;
        bool gameWon = false;

        Console.WriteLine("Welcome to Tic-Tac-Toe!");
        Console.WriteLine("Player X and Player O take turns to mark the grid.");
        Console.WriteLine("The first player to align three marks in a row, column, or diagonal wins.");
        Console.WriteLine("\nPress any key to start the game...");
        Console.ReadKey();

        while (!gameWon && turns < 9)
        {
            DisplayBoard();
            PlayerMove();
            gameWon = CheckWin();

            if (!gameWon)
            {
                SwitchPlayer();
                turns++;
            }
        }

        DisplayBoard();

        if (gameWon)
        {
            Console.WriteLine($"\nCongratulations! Player {currentPlayer} wins!");
        }
        else
        {
            Console.WriteLine("\nIt's a draw!");
        }

        Console.WriteLine("Thanks for playing! Press any key to exit.");
        Console.ReadKey();
    }

    static void DisplayBoard()
    {
        Console.Clear();
        Console.WriteLine("   Tic-Tac-Toe");
        Console.WriteLine("   -----------");
        Console.WriteLine($"     {board[0, 0]} | {board[0, 1]} | {board[0, 2]} ");
        Console.WriteLine("    ---+---+---");
        Console.WriteLine($"     {board[1, 0]} | {board[1, 1]} | {board[1, 2]} ");
        Console.WriteLine("    ---+---+---");
        Console.WriteLine($"     {board[2, 0]} | {board[2, 1]} | {board[2, 2]} ");
        Console.WriteLine("   -----------");
        Console.WriteLine($"\nPlayer {currentPlayer}'s turn. Choose a number to place your mark:");
    }

    static void PlayerMove()
    {
        int choice;
        bool validMove = false;

        while (!validMove)
        {
            string input = Console.ReadLine();

            if (int.TryParse(input, out choice) && choice >= 1 && choice <= 9)
            {
                int row = (choice - 1) / 3;
                int col = (choice - 1) % 3;

                if (board[row, col] != 'X' && board[row, col] != 'O')
                {
                    board[row, col] = currentPlayer;
                    validMove = true;
                }
                else
                {
                    Console.WriteLine("That spot is already taken. Try again:");
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a number between 1 and 9:");
            }
        }
    }

    static void SwitchPlayer()
    {
        currentPlayer = (currentPlayer == 'X') ? 'O' : 'X';
    }

    static bool CheckWin()
    {
        // Check rows
        for (int i = 0; i < 3; i++)
        {
            if (board[i, 0] == currentPlayer && board[i, 1] == currentPlayer && board[i, 2] == currentPlayer)
            {
                return true;
            }
        }

        // Check columns
        for (int i = 0; i < 3; i++)
        {
            if (board[0, i] == currentPlayer && board[1, i] == currentPlayer && board[2, i] == currentPlayer)
            {
                return true;
            }
        }

        // Check diagonals
        if ((board[0, 0] == currentPlayer && board[1, 1] == currentPlayer && board[2, 2] == currentPlayer) ||
            (board[0, 2] == currentPlayer && board[1, 1] == currentPlayer && board[2, 0] == currentPlayer))
        {
            return true;
        }

        return false;
    }
}
