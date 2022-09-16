// See https://aka.ms/new-console-template for more information
Console.WriteLine("Grass Cutter Robot!");

// Initializes the garden object as a pattern

Garden garden = new Garden(new char[][]
{
    new char[] { Garden.fence, Garden.fence, Garden.fence, Garden.fence, Garden.fence, Garden.fence, Garden.fence, Garden.fence, Garden.fence, Garden.fence, Garden.fence },
    new char[] { Garden.fence, Garden.grass, Garden.grass, Garden.grass, Garden.grass, Garden.obj,   Garden.obj,   Garden.grass, Garden.grass, Garden.grass, Garden.fence },
    new char[] { Garden.fence, Garden.grass, Garden.grass, Garden.grass, Garden.grass, Garden.grass, Garden.grass, Garden.grass, Garden.grass, Garden.grass, Garden.fence},
    new char[] { Garden.fence, Garden.grass, Garden.obj,   Garden.grass, Garden.grass, Garden.grass, Garden.grass, Garden.grass, Garden.obj,   Garden.grass, Garden.fence },
    new char[] { Garden.fence, Garden.grass, Garden.grass, Garden.grass, Garden.grass, Garden.grass, Garden.grass, Garden.grass, Garden.obj,   Garden.grass, Garden.fence },
    new char[] { Garden.fence, Garden.grass, Garden.grass, Garden.grass, Garden.grass, Garden.grass, Garden.startPosition, Garden.grass, Garden.grass, Garden.grass, Garden.fence },
    new char[] { Garden.fence, Garden.grass, Garden.grass, Garden.grass, Garden.obj,   Garden.obj,   Garden.obj,   Garden.obj,   Garden.grass, Garden.grass, Garden.fence },
    new char[] { Garden.fence, Garden.fence, Garden.fence, Garden.fence, Garden.fence, Garden.fence, Garden.fence, Garden.fence, Garden.fence, Garden.fence, Garden.fence }
});


// Printing the garden

garden.printGarden();
Console.ReadKey();
Console.Clear();

// Initializing GrassCutter object with garden object

GrassCutter gc = new GrassCutter(garden);

// Start cutting the grass

gc.cut();