using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoMatch3
{
    class Button: Clickable
    {
        private readonly Texture2D texture;

        public event EventHandler<PlayerIndexEventArgs> Clicked;

        public override void HandleInput()
        {
            base.HandleInput();
            if (IsClicked && Clicked != null)
                Clicked(this, new PlayerIndexEventArgs(PlayerIndex.One));
        }

        public Button(Texture2D texture, Point position)
        {
            this.texture = texture;
            Rectangle = new Rectangle(position, new Point(texture.Width, texture.Height));
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            var color = Color.White;
            if (IsHighlighted)
                color = Color.Wheat;
            if (IsClicked)
                color = Color.Orange;
            Game.SpriteBatch.Begin();
            Game.SpriteBatch.Draw(texture, Rectangle, color);
            Game.SpriteBatch.End();
        }
    }
}
