using System;

class GameOfLife
{
    static void Main(string[] args)
    {
        const int ROWS = 6;
        const int COLS = 9;
        bool[,] grid = new bool[ROWS, COLS];

        // Ask the user to set the initial state of the grid
        Console.WriteLine("Please enter the row and column numbers of each live cell, separated by a comma. Press enter to start the game.");
        while (true)
        {
            string input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input))
                break;

            string[] parts = input.Split(',');
            if (parts.Length != 2)
            {
                Console.WriteLine("Invalid input. Please enter the row and column numbers of a live cell, separated by a comma.");
                continue;
            }

            if (!int.TryParse(parts[0], out int row) || !int.TryParse(parts[1], out int col))
            {
                Console.WriteLine("Invalid input. Please enter the row and column numbers of a live cell, separated by a comma.");
                continue;
            }

            if (row < 0 || row >= ROWS || col < 0 || col >= COLS)
            {
                Console.WriteLine($"Cell ({row}, {col}) is out of the grid. Please enter correct row and column numbers of a live cell, separated by a comma.");
                continue;
            }

            grid[row, col] = true;
        }


        DrawGrid(grid);


        // Updating the grid each time the user presses enter
        while (true)
        {
            Console.WriteLine("Press enter to update the grid.");
            string input = Console.ReadLine();
            if (input == "q")
                break;

            bool[,] newGrid = UpdateGrid(grid);
            if (AreGridsEqual(grid, newGrid))
            {
                Console.WriteLine("The game is over");
                Console.WriteLine("Press enter to exit the game.");
                Console.ReadLine();
                return;
            }

            DrawGrid(newGrid);
            grid = newGrid;

        }

        Console.WriteLine("Press enter to exit the game.");
        Console.ReadLine();
    }

    //Compare grids 
    static bool AreGridsEqual(bool[,] grid1, bool[,] grid2)
    {
        if (grid1.GetLength(0) != grid2.GetLength(0) || grid1.GetLength(1) != grid2.GetLength(1))
            return false;

        for (int i = 0; i < grid1.GetLength(0); i++)
        {
            for (int j = 0; j < grid1.GetLength(1); j++)
            {
                if (grid1[i, j] != grid2[i, j])
                    return false;
            }
        }

        return true;
    }

    //Update the grid according to the rules

    static bool[,] UpdateGrid(bool[,] grid)
    {
        int rows = grid.GetLength(0);
        int cols = grid.GetLength(1);
        bool[,] newGrid = new bool[rows, cols];


        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                int liveNeighbors = 0;

                // Check each neighboring cell
                for (int i = -1; i <= 1; i++)
                {
                    for (int j = -1; j <= 1; j++)
                    {
                        if (i == 0 && j == 0)
                            continue;

                        int neighborRow = row + i;
                        int neighborCol = col + j;

                        // Check if the neighboring cell is on the grid
                        if (neighborRow < 0 || neighborRow >= rows || neighborCol < 0 || neighborCol >= cols)
                            continue;

                        // Check if the neighboring cell is alive
                        if (grid[neighborRow, neighborCol])
                            liveNeighbors++;

                    }
                }

                // State of the current cell in the next generation
                if (grid[row, col])
                {
                    if (liveNeighbors < 2 || liveNeighbors > 3)
                        newGrid[row, col] = false;
                    else
                        newGrid[row, col] = true;
                }
                else
                {
                    if (liveNeighbors == 3)
                        newGrid[row, col] = true;
                    else
                        newGrid[row, col] = false;
                }
            }
        }

        return newGrid;

    }


    // Print rows and row numbers
    static void DrawGrid(bool[,] grid)
    {
        int rows = grid.GetLength(0);
        int cols = grid.GetLength(1);

        Console.Clear();

        for (int row = rows - 1; row >= 0; row--)
        {
            Console.Write((row + 1) + " ");
            for (int col = 0; col < cols; col++)
            {
                if (grid[row, col])
                    Console.Write("X ");
                else
                    Console.Write("- ");
            }
            Console.WriteLine();
        }

        // Print column numbers
        Console.Write("  ");
        for (int col = 0; col < cols; col++)
        {
            Console.Write((col + 1) + " ");
        }
        Console.WriteLine();



    }
}





