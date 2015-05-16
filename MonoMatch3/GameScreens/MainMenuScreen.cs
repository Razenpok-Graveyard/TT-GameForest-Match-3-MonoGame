using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoMatch3
{
    class MainMenuScreen : MenuScreen
    {
        public MainMenuScreen()
        {
            var content = Game1.Instance.Content;
            var playButtonTexture = content.Load<Texture2D>("Play");
            var playButton = new Button(playButtonTexture, new Point(300, 350));
            playButton.Clicked += PlayButtonClicked;
            MenuButtons.Add(playButton);
        }

        void PlayButtonClicked(object sender, PlayerIndexEventArgs e)
        {
            LoadingScreen.Load(ScreenManager, PlayerIndex.One,
                               new GameplayScreen());
        }
    }
}
