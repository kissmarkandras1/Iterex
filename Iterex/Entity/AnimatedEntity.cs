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
        protected AnimationManager _animationManager;
        
        public int Direction { get; private set; }

        public AnimatedEntity(List<ITextureAdapter> textures)
            : base(textures)
        {

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //BorderDrawer.DrawRectangle(spriteBatch, CollisionBox, Color.Black, 1);
            //BorderDrawer.DrawRectangle(spriteBatch, TextureBox, Color.White, 1);
            _animationManager.Draw(spriteBatch, Position);
        }

    }
}
