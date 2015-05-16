using System.Collections.Generic;
using GameStateManagement;
using Microsoft.Xna.Framework;

namespace MonoMatch3
{
    abstract class MenuScreen : GameScreen
    {
        List<Button> menuButtons = new List<Button>();

        protected IList<Button> MenuButtons
        {
            get { return menuButtons; }
        }

        public override void HandleInput(GameTime gameTime, InputState input)
        {
            foreach (var button in menuButtons)
            {
                button.HandleInput();
            }
        }

        public override void Draw(GameTime gameTime)
        {
            foreach (var button in menuButtons)
            {
                button.Draw(gameTime);
            }
        }
    }
}
