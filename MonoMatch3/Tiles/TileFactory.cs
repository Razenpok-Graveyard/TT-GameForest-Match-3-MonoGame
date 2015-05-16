using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonoMatch3.Tiles
{
    class TileFactory
    {
        private static readonly Tile[] TileSamples = 
        { new Triangle(), new Diamond(), new Ellipsis(), new Hexagon(), new Pentagon(), new Square() };

        static readonly Random random = new Random();
        private static Type[] TileTypes
        {
            get { return TileSamples.Select(tile => tile.GetType()).ToArray(); }
        }

        public static Tile Random()
        {
            return Activator.CreateInstance(TileTypes[random.Next(0, TileTypes.Length)]) as Tile;
        }
    }
}
