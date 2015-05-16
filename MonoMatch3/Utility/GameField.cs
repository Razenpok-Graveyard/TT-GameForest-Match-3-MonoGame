using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoMatch3
{
    class GameField: DrawableGameComponent
    {
        private Texture2D backgroundTile;
        private const int Height = 8;
        private const int Width = 8;

        public Point ToIndex(int x, int y)
        {
            var newx = (x - 100)*2/backgroundTile.Width;
            var newy = ((Height - 1)*backgroundTile.Height - y)*2/backgroundTile.Height;
            return new Point(newx, newy);
        }

        public Point ToPosition(int x, int y)
        {
            var newx = 100 + x * backgroundTile.Width / 2;
            var newy = (Height - 1 - y)*backgroundTile.Height/2;
            return new Point(newx, newy);
        }

        public Point ToTilePosition(int x, int y)
        {
            var newx = 100 + x * backgroundTile.Width / 2 + backgroundTile.Width/4;
            var newy = (Height - 1 - y) * backgroundTile.Height / 2 + backgroundTile.Width / 4;
            return new Point(newx, newy);
        }

        public bool InBounds(Point pixelPosition)
        {
            var indexPosition = ToIndex(pixelPosition.X, pixelPosition.Y);
            return indexPosition.X >= 0 &&
                   indexPosition.Y >= 0 &&
                   indexPosition.X < Width &&
                   indexPosition.Y < Height;
        }
        
        public GameField() : base(Game1.Instance)
        {
            backgroundTile = Game.Content.Load<Texture2D>("Background");
        }

        public override void Draw(GameTime gameTime)
        {
            var spriteBatch = Game1.Instance.SpriteBatch;
            spriteBatch.Begin();
            for (var x = 0; x < Width; x++)
                for (var y = 0; y < Height; y++)
                {
                    var position = ToPosition(x,y).ToVector2();
                    spriteBatch.Draw(backgroundTile, position, null, Color.White, 0, Vector2.Zero, 0.5f, SpriteEffects.None, 0f);
                }
            spriteBatch.End();
        }
    }
}
