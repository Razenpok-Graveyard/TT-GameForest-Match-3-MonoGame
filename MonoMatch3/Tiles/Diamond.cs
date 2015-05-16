using Microsoft.Xna.Framework.Graphics;

namespace MonoMatch3
{
    class Diamond : Tile
    {
        public override Texture2D Texture
        {
            get { return TileTextureProvider.Diamond; }
        }
    }
}