using System;

class TicTacToe
{
    static char[] board = { ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ' };
    static char player = 'X';
    static char computer = 'O';

    static void Main(string[] args)
    {
        bool isPlayerTurn = true;

        do
        {
            Console.Clear();
            PrintBoard();

            if (isPlayerTurn)
            {
                Console.WriteLine($"Player {player}, enter your move (1-9): ");
                int move = int.Parse(Console.ReadLine()) - 1;

                if (move >= 0 && move < 9 && board[move] == ' ')
                {
                    board[move] = player;
                    isPlayerTurn = false;
                }
                else
                {
                    Console.WriteLine("Invalid move, try again.");
                    Console.ReadKey();
                }
            }
            else
            {
                int move = GetBestMove();
                board[move] = computer;
                isPlayerTurn = true;
                Console.WriteLine($"Computer chooses {move + 1}");
                System.Threading.Thread.Sleep(1000);
            }

        } while (!IsWinner() && !IsDraw());

        Console.Clear();
        PrintBoard();

        if (IsWinner())
        {
            Console.WriteLine($"{(isPlayerTurn ? "Computer" : "Player")} wins!");
        }
        else
        {
            Console.WriteLine("It's a draw!");
        }
    }

    static void PrintBoard()
    {
        Console.WriteLine("     |     |     ");
        Console.WriteLine($"  {board[0]}  |  {board[1]}  |  {board[2]}  ");
        Console.WriteLine("_____|_____|_____");
        Console.WriteLine("     |     |     ");
        Console.WriteLine($"  {board[3]}  |  {board[4]}  |  {board[5]}  ");
        Console.WriteLine("_____|_____|_____");
        Console.WriteLine("     |     |     ");
        Console.WriteLine($"  {board[6]}  |  {board[7]}  |  {board[8]}  ");
        Console.WriteLine("     |     |     ");
    }

    static bool IsWinner()
    {
        return (CheckLine(0, 1, 2) || CheckLine(3, 4, 5) || CheckLine(6, 7, 8) ||
                CheckLine(0, 3, 6) || CheckLine(1, 4, 7) || CheckLine(2, 5, 8) ||
                CheckLine(0, 4, 8) || CheckLine(2, 4, 6));
    }

    static bool CheckLine(int pos1, int pos2, int pos3)
    {
        return (board[pos1] == board[pos2] && board[pos2] == board[pos3] && board[pos1] != ' ');
    }

    static bool IsDraw()
    {
        foreach (char c in board)
        {
            if (c == ' ')
            {
                return false;
            }
        }
        return true;
    }

    static int GetBestMove()
    {
        int bestScore = int.MinValue;
        int move = -1;

        for (int i = 0; i < board.Length; i++)
        {
            if (board[i] == ' ')
            {
                board[i] = computer;
                int score = Minimax(board, false);
                board[i] = ' ';

                if (score > bestScore)
                {
                    bestScore = score;
                    move = i;
                }
            }
        }

        return move;
    }

    static int Minimax(char[] boardState, bool isMaximizing)
    {
        if (IsWinner())
        {
            return isMaximizing ? -1 : 1;
        }
        if (IsDraw())
        {
            return 0;
        }

        if (isMaximizing)
        {
            int bestScore = int.MinValue;
            for (int i = 0; i < boardState.Length; i++)
            {
                if (boardState[i] == ' ')
                {
                    boardState[i] = computer;
                    int score = Minimax(boardState, false);
                    boardState[i] = ' ';
                    bestScore = Math.Max(score, bestScore);
                }
            }
            return bestScore;
        }
        else
        {
            int bestScore = int.MaxValue;
            for (int i = 0; i < boardState.Length; i++)
            {
                if (boardState[i] == ' ')
                {
                    boardState[i] = player;
                    int score = Minimax(boardState, true);
                    boardState[i] = ' ';
                    bestScore = Math.Min(score, bestScore);
                }
            }
            return bestScore;
        }
    }
}
