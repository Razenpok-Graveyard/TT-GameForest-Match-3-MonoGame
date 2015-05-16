using Microsoft.Xna.Framework.Graphics;

namespace MonoMatch3
{
    class Pentagon : Tile
    {
        public override Texture2D Texture
        {
            get { return TileTextureProvider.Pentagon; }
        }
    }
}