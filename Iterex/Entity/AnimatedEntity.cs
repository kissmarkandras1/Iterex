using Iterex.Common.Animation;
using Iterex.Common.TestingUltilities;
using Iterex.Common.TextureAdapter;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iterex.Entity
{
    public class AnimatedEntity : Entity
    {
        protected int _preDirection;
        public int Direction { get; protected set; }
        public bool OnGround { get; protected set; }

        public AnimatedEntity() : base() { }

        public AnimatedEntity(ITextureAdapter texture) 
            : base(texture)
        {

        }

        public AnimatedEntity(Dictionary<string, ITextureAdapter> textures, string firstTexture)
            : base(textures, firstTexture)
        {

        }


        protected bool IsDone()
        {
            return _animationManager.IsDone();
        }

    }
}
