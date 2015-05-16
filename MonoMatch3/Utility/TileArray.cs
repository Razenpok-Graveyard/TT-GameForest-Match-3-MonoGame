using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoMatch3.Tiles;

namespace MonoMatch3
{
    class TileArray
    {
        private GameField gameField;
        private readonly int height;
        private readonly Tile[,] tiles;
        private readonly int width;

        public TileArray(int height, int width, GameField field)
        {
            gameField = field;
            this.height = height;
            this.width = width;
            tiles = new Tile[width, height];
            Fill();
        }

        public void Fill()
        {
            for (var x = 0; x < width; x++)
                for (var y = 0; y < height; y++)
                {
                    if (this[new Point(x, y)] == null)
                        SpawnTile(x);
                }
        }

        public void Collapse()
        {
            for (var x = 0; x < width; x++)
                for (var y = 0; y < height; y++)
                {
                    var currentTile = new Point(x, y);
                    if (this[currentTile] != null) continue;
                    var onlyEmptyLeft = true;
                    for (var i = y + 1; i < height; i++)
                    {
                        var nextTile = new Point(x, i);
                        if (this[nextTile] == null) continue;
                        this[currentTile] = this[nextTile];
                        this[nextTile].MoveTo(gameField.ToTilePosition(x,y).ToVector2(), new Point(x, y));
                        this[nextTile] = null;
                        onlyEmptyLeft = false;
                        break;
                    }
                    if (onlyEmptyLeft)
                        break;
                }
        }

        private void SpawnTile(int column)
        {
            for (var y = 0; y < height; y++)
            {
                var currentTile = new Point(column, y);
                if (this[currentTile] != null) continue;
                var tile = TileFactory.Random();
                tile.Position = gameField.ToTilePosition(column, height).ToVector2();
                tile.ArrayPosition = currentTile;
                this[currentTile] = tile;
                tile.MoveTo(gameField.ToTilePosition(column, y).ToVector2(), new Point(column, y));
                break;
            }
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
                var toCompare = line.GetRange(i - 1, 3);
                if (toCompare.Any(tile => tile == null)) continue;
                var type = line[i].GetType();
                if (toCompare.Any(tile => tile.GetType() != type)) continue;
                result.AddRange(toCompare);
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

        public IEnumerable<Tile> GetAllTiles()
        {
            return Enumerable.Range(0, height).SelectMany(GetRow);
        }

        public Tile GetClickedTile()
        {
            return GetAllTiles().FirstOrDefault(tile => tile.IsClicked);
        }

        public IEnumerable<Point> GetNeighbours(Point target)
        {
            var offsets = new[] { -1, 1 };
            return offsets.SelectMany(offset => new[] { new Point(offset, 0), new Point(0, offset) })
                .Select(point => point + target);
        }

        public void Update(GameTime gameTime)
        {
            for (var x = 0; x < width; x++)
                for (var y = 0; y < height; y++)
                {
                    if (tiles[x, y] == null) continue;
                    tiles[x, y].Update(gameTime);
                }
        }

        public void Draw(GameTime gameTime)
        {
            for (var x = 0; x < width; x++)
                for (var y = 0; y < height; y++)
                {
                    if (tiles[x, y] == null) continue;
                    tiles[x,y].Draw(gameTime);
                }
        }
    }
}
