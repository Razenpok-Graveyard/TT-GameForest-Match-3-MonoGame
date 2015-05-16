using Microsoft.Xna.Framework.Graphics;

namespace MonoMatch3
{
    class Triangle : Tile
    {
        public override Texture2D Texture {
            get { return TileTextureProvider.Triangle; }
        }
    }
}