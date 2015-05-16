using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoMatch3
{
    class GameOverScreen : MenuScreen
    {
        private Texture2D GameOverLabel;

        public GameOverScreen()
        {
            var content = Game1.Instance.Content;
            var okButtonTexture = content.Load<Texture2D>("OK");
            var okButton = new Button(okButtonTexture, new Point(300, 400));
            okButton.Clicked += OkButtonClicked;
            MenuButtons.Add(okButton);
            GameOverLabel = content.Load<Texture2D>("GameOver");
        }

        void OkButtonClicked(object sender, PlayerIndexEventArgs e)
        {            
            LoadingScreen.Load(ScreenManager, new MainMenuScreen());
        }

        public override void Draw(GameTime gameTime)
        {
            var spriteBatch = Game1.Instance.SpriteBatch;
            spriteBatch.Begin();
            spriteBatch.Draw(GameOverLabel, new Vector2(200, 100), Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
