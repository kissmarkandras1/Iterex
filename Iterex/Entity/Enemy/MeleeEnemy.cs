using Iterex.Common.TextureAdapter;
using System;
using System.Collections.Generic;
using System.Text;

namespace Iterex.Entity.Enemy
{
    class MeleeEnemy : Enemy
    {
        public MeleeEnemy() : base() { }

        public MeleeEnemy(ITextureAdapter texture)
            : base(texture)
        {

        }

        public MeleeEnemy(Dictionary<string, ITextureAdapter> textures, string firstTexture)
            : base(textures, firstTexture)
        {

        }
    }
}
