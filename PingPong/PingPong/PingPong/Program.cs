using System;
using System.Threading;

class Program
{
    static int paddle1Y;
    static int paddle2Y;
    static int ballX;
    static int ballY;
    static int ballDirectionX;
    static int ballDirectionY;

    static int player1Score;
    static int player2Score;

    static void Main()
    {
        Console.Title = "Ping Pong Game";
        Console.WindowHeight = 30;
        Console.WindowWidth = 80;
        Console.BufferHeight = 30;
        Console.BufferWidth = 80;

        paddle1Y = Console.WindowHeight / 2 - 3;
        paddle2Y = Console.WindowHeight / 2 - 3;
        ballX = Console.WindowWidth / 2;
        ballY = Console.WindowHeight / 2;
        ballDirectionX = -1;
        ballDirectionY = -1;

        player1Score = 0;
        player2Score = 0;

        while (true)
        {
            Console.Clear();
            DrawPaddles();
            DrawBall();
            DrawScores();

            ConsoleKeyInfo keyInfo;
            if (Console.KeyAvailable)
            {
                keyInfo = Console.ReadKey(true);
                MovePaddles(keyInfo);
            }

            if (MoveBall())
            {
                ResetBall();
            }

            Thread.Sleep(40);
        }
    }

    static void DrawPaddles()
    {
        // Clear the area where the paddles were previously
        for (int i = 0; i < Console.WindowHeight; i++)
        {
            Console.SetCursorPosition(0, i);
            Console.Write(" "); // Clear two characters for each line
            Console.SetCursorPosition(Console.WindowWidth - 2, i);
            Console.Write(" "); // Clear two characters for each line
        }

        // Draw the paddles within the bounds
        for (int i = 0; i < 6 && paddle1Y + i < Console.WindowHeight - 1; i++)
        {
            Console.SetCursorPosition(0, paddle1Y + i);
            Console.Write("|");
        }

        for (int i = 0; i < 6 && paddle2Y + i < Console.WindowHeight - 1; i++)
        {
            Console.SetCursorPosition(Console.WindowWidth - 2, paddle2Y + i);
            Console.Write("|");
        }
    }

    static void DrawBall()
    {
        Console.SetCursorPosition(ballX, ballY);
        Console.Write("()");
    }

    static void DrawScores()
    {
        Console.SetCursorPosition(Console.WindowWidth / 2 - 5, 0);
        Console.Write("Ping Pong Game");
        Console.SetCursorPosition(Console.WindowWidth / 4, 1);
        Console.Write("Player 1: " + player1Score);
        Console.SetCursorPosition(Console.WindowWidth / 2 + Console.WindowWidth / 4 - 10, 1);
        Console.Write("Player 2: " + player2Score);
    }

    static void MovePaddles(ConsoleKeyInfo keyInfo)
    {
        if (keyInfo.Key == ConsoleKey.W && paddle1Y > 0)
        {
            paddle1Y--;
        }
        if (keyInfo.Key == ConsoleKey.S && paddle1Y < Console.WindowHeight - 4)
        {
            paddle1Y++;
        }
        if (keyInfo.Key == ConsoleKey.UpArrow && paddle2Y > 0)
        {
            paddle2Y--;
        }
        if (keyInfo.Key == ConsoleKey.DownArrow && paddle2Y < Console.WindowHeight - 4)
        {
            paddle2Y++;
        }
    }

    static bool MoveBall()
    {
        ballX += ballDirectionX;
        ballY += ballDirectionY;

        // Ball collision with top and bottom walls
        if (ballY <= 0 || ballY >= Console.WindowHeight - 1)
        {
            ballDirectionY *= -1;
        }

        // Ball collision with paddles
        if (ballX == 1 && ballY >= paddle1Y && ballY <= paddle1Y + 3)
        {
            ballDirectionX *= -1;
        }
        if (ballX == Console.WindowWidth - 2 && ballY >= paddle2Y && ballY <= paddle2Y + 3)
        {
            ballDirectionX *= -1;
        }

        // Check if the ball is out of bounds
        if (ballX < 0)
        {
            player2Score++;
            return true; // Ball is out of bounds; return true to reset the ball.
        }
        else if (ballX >= Console.WindowWidth)
        {
            player1Score++;
            return true; // Ball is out of bounds; return true to reset the ball.
        }

        return false;
    }

    static void ResetBall()
    {
        ballX = Console.WindowWidth / 2;
        ballY = Console.WindowHeight / 2;
        ballDirectionX = -1;
        ballDirectionY = -1;
    }
}
