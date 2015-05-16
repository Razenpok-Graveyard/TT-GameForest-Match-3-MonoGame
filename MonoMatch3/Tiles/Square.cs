using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace MonoMatch3
{
    class Square: Tile
    {
        public override Texture2D Texture {
            get { return TileTextureProvider.Square; }
        }
    }
}
