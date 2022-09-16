public class Garden
{
    public static char fence { get; set; } = '*';
    public static char startPosition { get; set; } = 'S';
    public static char currentPosition { get; set; } = 'C';
    public static char mowed { get; set; } = 'M';
    public static char obj { get; set; } = 'X';
    public static char grass { get; set; } = '0';
    public char[][] GardenObj { get; set; }

    // Constructor

    public Garden(char[][] garden)
    {
        GardenObj = garden;
    }

    // Print garden method

    public void printGarden()
    {
        Console.Clear();
        for (int i = 0; i < this.GardenObj.Length; i++)
        {
            for (int j = 0; j < this.GardenObj[i].Length; j++)
            {
                Console.Write(this.GardenObj[i][j]);
                if (j == this.GardenObj[i].Length - 1)
                {
                    Console.WriteLine();
                }
            }
        }
        Console.WriteLine();
    }

    // Gets the starting position of the grass cutter from the pattern

    public Position getStartPosition()
    {
        for(int i = 1; i < this.GardenObj.Length - 1; i++)
        {
            for (int j = 1; j < this.GardenObj[i].Length - 1; j++)
            {
                if (this.GardenObj[i][j] == startPosition)
                {
                    return new Position(i, j);
                }
            }
        }
        return null;
    }

    
}
