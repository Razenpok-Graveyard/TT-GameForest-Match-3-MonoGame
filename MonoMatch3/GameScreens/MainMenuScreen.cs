using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoMatch3
{
    class MainMenuScreen : MenuScreen
    {
        public MainMenuScreen()
        {
            var playButtonTexture = Game1.Instance.Content.Load<Texture2D>("Play");
            var playButton = new Button(playButtonTexture, new Point(300, 200));
            playButton.Clicked += PlayButtonClicked;
            MenuButtons.Add(playButton);
        }

        void PlayButtonClicked(object sender, PlayerIndexEventArgs e)
        {
            LoadingScreen.Load(ScreenManager, new GameplayScreen());
        }
    }
}
