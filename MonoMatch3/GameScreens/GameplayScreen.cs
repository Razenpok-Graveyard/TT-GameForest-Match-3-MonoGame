using System;
using System.Threading;
using GameStateManagement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoMatch3
{
    class GameplayScreen : GameScreen
    {
        private GameField field = new GameField();
        ContentManager content;
        SpriteFont font;

        readonly InputAction pauseAction;

        public GameplayScreen()
        {
            pauseAction = new InputAction(
                new Buttons[] {},
                new [] { Keys.Escape, },
                true);
        }

        public override void Activate(bool instancePreserved)
        {
            if (instancePreserved) return;
            if (content == null)
                content = new ContentManager(ScreenManager.Game.Services, "Content");
            font = content.Load<SpriteFont>("Font");
            ScreenManager.Game.ResetElapsedTime();
            TimeManager.OnTimeUp = OnTimeUp;
            TimeManager.StartTimer(3);
        }

        private void OnTimeUp()
        {
            ScreenManager.AddScreen(new GameOverScreen(), PlayerIndex.One);
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus,
                                                       bool coveredByOtherScreen)
        {
            
            base.Update(gameTime, otherScreenHasFocus, false);
            if (IsActive)
            {
                TimeManager.UpdateTimer(gameTime);
                // TODO: this game isn't very fun! You could probably improve
                // it by inserting something more interesting in this space :-)
            }
        }

        public override void HandleInput(GameTime gameTime, InputState input)
        {
            if (input == null)
                throw new ArgumentNullException("input");

            PlayerIndex player;
            if (pauseAction.Evaluate(input, ControllingPlayer, out player))
            {
                ScreenManager.AddScreen(new GameOverScreen(), PlayerIndex.One);
            }

            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                ScoreManager.Add(1);
        }

        public override void Draw(GameTime gameTime)
        {
            ScreenManager.GraphicsDevice.Clear(ClearOptions.Target, Color.Black, 0, 0);
            var spriteBatch = ScreenManager.SpriteBatch;
            spriteBatch.Begin();
            spriteBatch.DrawString(font, "Score: " + ScoreManager.Score, new Vector2(10, 25), Color.White);
            spriteBatch.DrawString(font, "Time: " + TimeManager.RemainingTime, new Vector2(10, 75), Color.White);
            spriteBatch.End();
            field.Draw(gameTime);
        }
    }
}