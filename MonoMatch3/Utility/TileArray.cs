using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MonoMatch3
{
    class TileArray
    {
        private readonly int height;
        private readonly Tile[,] tiles;
        private readonly int width;

        public TileArray(int height, int width)
        {
            this.height = height;
            this.width = width;
            tiles = new Tile[width, height];
        }

        public Tile this[Point position]
        {
            get { return tiles[position.X, position.Y]; }
            set { tiles[position.X, position.Y] = value; }
        }

        public IEnumerable<Tile> GetColumnMatches(int columnIndex)
        {
            var column = GetColumn(columnIndex).ToList();
            return GetLineMatches(column);
        }

        public IEnumerable<Tile> GetColumn(int columnIndex)
        {
            if (columnIndex < 0 || columnIndex >= width)
                return null;
            var column = Enumerable.Range(0, height)
                .Select(y => tiles[columnIndex, y]);
            return column;
        }

        public IEnumerable<Tile> GetLineMatches(List<Tile> line)
        {
            var result = new List<Tile>();
            for (var i = 1; i < line.Count - 1; i++)
            {
                var thisName = line[i].GetType();
                var leftName = line[i - 1].GetType();
                var rightName = line[i + 1].GetType();
                if (thisName == leftName && thisName == rightName)
                    result.AddRange(line.GetRange(i - 1, 3));
            }
            return result.Distinct();
        }

        public IEnumerable<Tile> GetRowMatches(int rowIndex)
        {
            var row = GetRow(rowIndex).ToList();
            return GetLineMatches(row);
        }

        public IEnumerable<Tile> GetRow(int rowIndex)
        {
            if (rowIndex < 0 || rowIndex >= height)
                return null;
            var row = Enumerable.Range(0, width)
                .Select(x => tiles[x, rowIndex]);
            return row;
        }

        public IEnumerable<Tile> FindMatches()
        {
            var columnMatches = Enumerable.Range(0, width)
                .SelectMany(column => GetColumnMatches(column));
            var rowMatches = Enumerable.Range(0, height)
                .SelectMany(row => GetRowMatches(row));
            return columnMatches.Concat(rowMatches).Distinct();
        }
    }
}
