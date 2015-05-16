using Microsoft.Xna.Framework.Graphics;

namespace MonoMatch3
{
    class Hexagon : Tile
    {
        public override Texture2D Texture
        {
            get { return TileTextureProvider.Hexagon; }
        }
    }
}