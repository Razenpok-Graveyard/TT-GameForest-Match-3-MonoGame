using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoMatch3
{
    abstract class Tile : Clickable
    {
        public abstract Texture2D Texture { get;}
        private Vector2 position;
        public Point ArrayPosition = new Point(-1, -1);
        private float scale = 0.5f;
        private Vector2 targetPosition;
        private float rotation = 0;

        private const float MoveSpeed = 0.5f;
        private const int ShrinkSpeed = 2;
        private const float RotationSpeed = 0.005f;
        private const float ShrinkScale = 0.1f;
        public bool IsMoving { get; private set; }
        public bool IsSpinning { get; private set; }
        public bool IsRemoving { get; private set; }

        public Tile()
        {
            Rectangle = new Rectangle(0,0,75,75);
        }

        public Vector2 Position
        {
            get { return position; }
            set
            {
                position = value;
                if (Texture!=null)
                    RectanglePosition = (value - new Vector2(Texture.Width / 4, Texture.Height/4)).ToPoint();
            }
        }

        public void Remove(Action onRemove)
        {
            IsRemoving = true;
            OnRemove = onRemove;
        }

        public void MoveTo(Vector2 target, Point arrayPosition)
        {
            IsMoving = true;
            targetPosition = target;
            ArrayPosition = arrayPosition;
        }

        public void BeginSpinning()
        {
            IsSpinning = true;
        }

        public void StopSpinning()
        {
            IsSpinning = false;
            rotation = 0;
        }

        public Action OnRemove;

        public override void Update(GameTime gameTime)
        {
            HandleInput();
            HandleMove(gameTime);
            HandleRemove();
            HandleSpinning(gameTime);
        }

        private void HandleSpinning(GameTime gameTime)
        {
            if (!IsSpinning) return;
            rotation += (float)gameTime.ElapsedGameTime.TotalMilliseconds * RotationSpeed;
        }

        private void HandleRemove()
        {
            if (!IsRemoving) return;
            scale -= 0.01f * ShrinkSpeed;
            if (!(scale <= ShrinkScale)) return;
            IsRemoving = false;
            scale = 0;
            if (OnRemove != null)
                OnRemove();
        }

        private void HandleMove(GameTime gameTime)
        {
            if (!IsMoving) return;
            var differenceToTarget = targetPosition - Position;
            if (Math.Abs(differenceToTarget.X) < 1 && Math.Abs(differenceToTarget.Y) < 1)
            {
                IsMoving = false;
                Position = targetPosition;
                return;
            }
            differenceToTarget.Normalize();
            Position  += differenceToTarget * (float)gameTime.ElapsedGameTime.TotalMilliseconds * MoveSpeed;
            
        }

        public override void Draw(GameTime gameTime)
        {
            var spriteBatch = Game1.Instance.SpriteBatch;
            spriteBatch.Begin();
            spriteBatch.Draw(Texture, Position, null, Color.White, rotation, new Vector2(Texture.Width/2, Texture.Height/2), scale,
                        SpriteEffects.None, 0f);
            spriteBatch.End();
        }
    }
}
