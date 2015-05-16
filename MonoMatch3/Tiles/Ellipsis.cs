using Microsoft.Xna.Framework.Graphics;

namespace MonoMatch3
{
    class Ellipsis : Tile
    {
        public override Texture2D Texture
        {
            get
            {
                return TileTextureProvider.Ellipsis;
            }
        }
    }
}