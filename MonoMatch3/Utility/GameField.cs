using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoMatch3
{
    class GameField: DrawableGameComponent
    {
        private Texture2D backgroundTile;
        private const int Height = 8;
        private const int Width = 8;
        
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
                    var position = new Vector2(100 + x * backgroundTile.Width / 2, y * backgroundTile.Height / 2);
                    spriteBatch.Draw(backgroundTile, position, null, Color.White, 0, Vector2.Zero, 0.5f, SpriteEffects.None, 0f);
                }
            spriteBatch.End();
        }
    }
}
