using System;

public sealed class MapGrid
{
    public int Width { get; private set; }
    public int Height { get; private set; }
    public MapTile[,] Tiles { get; private set; }

    public MapGrid(int width, int height)
    {
        if (width <= 0 || height <= 0)
        {
            throw new ArgumentException("Width and height must be greater than zero.");
        }

        Width = width;
        Height = height;

        Tiles = new MapTile[Width, Height];

        for (int y = 0; y < Width; y++)
        {
            for (int x = 0; x < Height; x++)
            {
                Tiles[y, x] = new MapTile();
            }
        }
    }
}