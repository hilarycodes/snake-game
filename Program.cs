using System;
using System.Collections.Generic;
using System.Threading;

Random random = new Random();
Console.CursorVisible = false;
int height = Console.WindowHeight - 1;
int width = Console.WindowWidth - 5;
bool shouldExit = false;

// Player position in the Console
int playerX = 0;
int playerY = 0;

// Food position in the Console
int foodX = 0;
int foodY = 0;

// food states and player states
string[] states = { "('-')", "(^-^)", "(X_X)", "(O-O)", "(>_<)" };
string[] foods = { "@@@@@", "$$$$$", "#####", "OOOO", "90210" };

// player 1
string player = states[0];

// array index of the current food 
int food = 0;

// game starts
InitializeGame();
while (!shouldExit)
{
    Move();
    if (TerminalResized())
    {
        TerminalResized();
        Console.Clear();
        Console.WriteLine("The Console was resized. The Game is now exiting");
        shouldExit = true;
    }
}

// Returns true if the Terminal was resized 
bool TerminalResized()
{
    return height != Console.WindowHeight - 1 || width != Console.WindowWidth - 5;
}

// Displays random food at a random location
void ShowFood()
{
    // Update food to a random index
    food = random.Next(0, foods.Length);

    // Update food position to a random location
    // player.Length is used to prevent the food from being displayed outside the bounds of the Console
    foodX = random.Next(0, width - player.Length);
    foodY = random.Next(0, height - 1);

    // Display the food at the location
    Console.SetCursorPosition(foodX, foodY);
    Console.Write(foods[food]);
}



// Changes the player to match the food consumed
/*void ChangePlayer()
{
    player = states[food];
    Console.SetCursorPosition(playerX, playerY);
    Console.Write(player);
}*/

// Temporarily stops the player from moving
void FreezePlayer()
{
    Thread.Sleep(1000);
    player = states[0];
}

void IncorrectMove()
{
    Console.Clear();
    Console.WriteLine("Invalid move. Please use the arrow keys to move the player.");
    shouldExit = true;
}

// Reads directional input from the Console and moves the player
void Move()
{
    int lastX = playerX;
    int lastY = playerY;

    switch (Console.ReadKey(true).Key)
    {
        case ConsoleKey.UpArrow:
            playerY--; break;
        case ConsoleKey.DownArrow:
            playerY++; break;
        case ConsoleKey.LeftArrow:
            playerX--; break;
        case ConsoleKey.RightArrow:
            playerX++; break;
        case ConsoleKey.Escape:
            shouldExit = true; break;
        default:
            IncorrectMove(); break;
    }

    // Clear the characters at the previous position
    Console.SetCursorPosition(lastX, lastY);
    for (int i = 0; i < player.Length; i++)
    {
        Console.Write(" ");
    }

    //playerX = Math.Max(0, Math.Min(playerX, width));
    //playerX = Math.Max(0, Math.Min(playerY, height));

    // Keep player position within the bounds of the Terminal window
    playerX = (playerX < 0) ? 0 : (playerX >= width ? width : playerX);
    playerY = (playerY < 0) ? 0 : (playerY >= height ? height : playerY);

    // Draw the player at the new location
    Console.SetCursorPosition(playerX, playerY);
    Console.Write(player);
    void ChangeFoodLocation()
    {
        if (lastX == foodX && lastY == foodY)
        {
            ShowFood();
        }
    }
    void ChangePlayer()
    {
        if (lastX == foodX && lastY == foodY)
        {
            ShowNewPlayer();
        }
    }
    ChangePlayer();
    ChangeFoodLocation();
}

void ShowNewPlayer()
{
    player = states[food];
    Console.SetCursorPosition(playerX, playerY);
    Console.Write(player);
}

// Clears the console, displays the food and player
void InitializeGame()
{
    Console.Clear();
    Console.WriteLine("Welcome to the Snake Game I built");
    System.Console.WriteLine("To exit press the 'esc' button");
    System.Console.WriteLine("The game will start shorlty");
    Thread.Sleep(5000);
    Console.Clear();
    ShowFood();
    Console.SetCursorPosition(0, 0);
    Console.Write(player);
}