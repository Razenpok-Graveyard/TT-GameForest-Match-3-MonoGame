using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MonoMatch3
{
    static class TileTextureProvider
    {
        public static Texture2D Triangle { get; private set; }
        public static Texture2D Square { get; private set; }
        public static Texture2D Diamond { get; private set; }
        public static Texture2D Pentagon { get; private set; }
        public static Texture2D Hexagon { get; private set; }
        public static Texture2D Ellipsis { get; private set; }
        
        public static void Initialize(ContentManager content)
        {
            Triangle = content.Load<Texture2D>("Triangle");
            Square = content.Load<Texture2D>("Square");
            Diamond = content.Load<Texture2D>("Diamond");
            Pentagon = content.Load<Texture2D>("Pentagon");
            Hexagon = content.Load<Texture2D>("Hexagon");
            Ellipsis = content.Load<Texture2D>("Ellipsis");
        }
    }
}
