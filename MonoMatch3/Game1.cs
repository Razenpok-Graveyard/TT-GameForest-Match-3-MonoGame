using GameStateManagement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoMatch3
{
    public class Game1 : Game
    {
        public static Game1 Instance;

        GraphicsDeviceManager graphics;
        public SpriteBatch SpriteBatch;

        ScreenManager screenManager;

        public Game1()
        {
            IsMouseVisible = true;
            Instance = this;
            graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = 800,
                PreferredBackBufferHeight = 600
            };
            Content.RootDirectory = "Content";

            var screenFactory = new ScreenFactory();
            Services.AddService(typeof(IScreenFactory), screenFactory);

            screenManager = new ScreenManager(this);
            Components.Add(screenManager);
        }

        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);
            screenManager.AddScreen(new MainMenuScreen(), null);
           // LoadingScreen.Load(screenManager, new GameplayScreen());
        }

        protected override void UnloadContent()
        {
            Content.Unload();
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            base.Draw(gameTime);
        }
    }
}
