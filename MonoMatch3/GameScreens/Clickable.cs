using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MonoMatch3
{
    class Clickable: DrawableGameComponent
    {
        protected Rectangle Rectangle { get; set;}

        public Point Position
        {
            get { return new Point(Rectangle.X, Rectangle.Y); }
            set { Rectangle = new Rectangle(Position, value);}
        }

        protected bool IsHighlighted { get; private set; }
        protected bool IsClicked;

        protected new Game1 Game { get { return (Game1)base.Game; } }

        protected Clickable(Rectangle targetRectangle): base(Game1.Instance)
        {
            Rectangle = targetRectangle;
        }

        protected Clickable() : base(Game1.Instance)
        {
        }

        public virtual void HandleInput()
        {
            IsHighlighted = false;
            IsClicked = false;
            var mouseState = Mouse.GetState();
            if (!Rectangle.Contains(new Point(mouseState.X, mouseState.Y))) return;
            IsHighlighted = true;
            IsClicked = mouseState.LeftButton == ButtonState.Pressed;
        }
    }
}