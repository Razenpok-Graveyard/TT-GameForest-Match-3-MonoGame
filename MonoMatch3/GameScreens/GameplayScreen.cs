using System;
using System.Linq;
using GameStateManagement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoMatch3
{
    public enum GameState
    {
        None,
        TileMoving,
        TileSelected,
        HasEmptyTiles,
        MovedTile
    }

    class GameplayScreen : GameScreen
    {
        private GameField field = new GameField();
        TileArray tiles;
        ContentManager content;
        SpriteFont font;
        private GameState gameState = GameState.None;
        private Tile selectedTile;
        private Tile swappedTile;

        public override void Activate(bool instancePreserved)
        {
            tiles = new TileArray(8, 8, field);
            if (instancePreserved) return;
            if (content == null)
                content = new ContentManager(ScreenManager.Game.Services, "Content");
            font = content.Load<SpriteFont>("Font");
            TileTextureProvider.Initialize(content);
            ScreenManager.Game.ResetElapsedTime();
            TimeManager.OnTimeUp = OnTimeUp;
            TimeManager.StartTimer();
        }

        private void OnTimeUp()
        {
            ScreenManager.AddScreen(new GameOverScreen(), PlayerIndex.One);
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus,
                                                       bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, false);
            if (!IsActive) return;
            TimeManager.UpdateTimer(gameTime);
            tiles.Update(gameTime);
            gameState = GetGameState();
            switch (gameState)
            {
                case (GameState.TileMoving):
                    return;
                case GameState.HasEmptyTiles:
                    {
                        tiles.Collapse();
                        tiles.Fill();
                        break;
                    }
                case GameState.TileSelected:
                    {
                        if (Mouse.GetState().LeftButton != ButtonState.Pressed)
                            break;
                        var clickedTile = tiles.GetClickedTile();
                        if (clickedTile == null)
                            break;
                        var position = clickedTile.ArrayPosition;
                        var neighbours = tiles.GetNeighbours(selectedTile.ArrayPosition);
                        if (neighbours.Any(pos => pos.X == position.X && pos.Y == position.Y))
                        {
                            selectedTile.StopSpinning();
                            swappedTile = clickedTile;
                            SwapTiles(selectedTile.ArrayPosition, swappedTile.ArrayPosition);
                        }
                        else
                        {
                            selectedTile.StopSpinning();
                            selectedTile = null;
                        }
                        break;
                    }
                case GameState.MovedTile:
                {
                    var matches = tiles.FindMatches().ToList();
                    if (matches.Any())
                    {
                        foreach (var tile in matches)
                        {
                            var position = tile.ArrayPosition; 
                            tile.Remove(() => { tiles[position] = null; });
                            ScoreManager.Add(1);
                        }
                    }
                    else
                    {
                        SwapTiles(swappedTile.ArrayPosition, selectedTile.ArrayPosition);
                    }
                    selectedTile = null;
                    swappedTile = null;
                    break;
                }
                case GameState.None:
                    {
                        var matches = tiles.FindMatches().ToList();
                        if (matches.Any())
                        {
                            foreach (var tile in matches)
                            {
                                ScoreManager.Add(1);
                                var position = tile.ArrayPosition;
                                tile.Remove(() => { tiles[position] = null; });
                            }
                            break;
                        }
                        var clickedTile = tiles.GetClickedTile();
                        if (clickedTile != null)
                        {
                            selectedTile = clickedTile;
                            selectedTile.BeginSpinning();
                        }
                        break;
                    }
            }

        }

        private GameState GetGameState()
        {
            var allTiles = tiles.GetAllTiles().ToList();
            if (allTiles.Where(tile => tile != null).Any(tile => tile.IsMoving || tile.IsRemoving))
                return GameState.TileMoving;
            if (allTiles.Any(tile => tile == null))
                return GameState.HasEmptyTiles;
            if (swappedTile != null)
                return GameState.MovedTile;
            if (selectedTile != null)
                return GameState.TileSelected;
            return GameState.None;
        }

        private void SwapTiles(Point first, Point second)
        {
            var firstTile = tiles[first];
            var secondTile = tiles[second];
            firstTile.MoveTo(field.ToTilePosition(second.X, second.Y).ToVector2(),second);
            tiles[second].MoveTo(field.ToTilePosition(first.X, first.Y).ToVector2(), first);
            tiles[first] = secondTile;
            tiles[second] = firstTile;
        }

        public override void HandleInput(GameTime gameTime, InputState input)
        {
            if (input == null)
                throw new ArgumentNullException("input");

        }

        public override void Draw(GameTime gameTime)
        {
            ScreenManager.GraphicsDevice.Clear(ClearOptions.Target, Color.Black, 0, 0);
            var spriteBatch = ScreenManager.SpriteBatch;
            spriteBatch.Begin();
            spriteBatch.DrawString(font, "Score: " + ScoreManager.Score, new Vector2(10, 25), Color.White);
            spriteBatch.DrawString(font, "Time: " + TimeManager.RemainingTime, new Vector2(10, 50), Color.White);
            spriteBatch.End();
            field.Draw(gameTime);
            tiles.Draw(gameTime);
        }
    }
}