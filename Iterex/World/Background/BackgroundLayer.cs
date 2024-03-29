﻿using Iterex.Common;
using Iterex.Common.TextureAdapter;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iterex.World.Background
{
    public class BackgroundLayer
    {
        private List<Sprite> _sprites;
        private float _depth;
        private float _scrollingSpeed;
        private bool _selfMoving;
        private int _repeat;

        public BackgroundLayer(ITextureAdapter texture, float scrollingSpeed, float depth, bool selfMoving, int repeat)
        {
            _sprites = new List<Sprite>();
            _scrollingSpeed = scrollingSpeed;
            _depth = depth;
            _selfMoving = selfMoving;
            _repeat = repeat;

            for (int i = -1; i < _repeat -1; i++)
            {
                _sprites.Add(new Sprite(new Dictionary<string, ITextureAdapter>() { { texture.Name, texture } }, 
                                       texture.Name)
                {
                    Position = new Vector2(i * texture.Width - _repeat*i, Game1.ScreenHeight - texture.Height),
                    IsSolid = false
                });

            }
        }

        public void Update(GameTime gameTime)
        {
            MovingLayer(gameTime);

            CirclingSprite();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Sprite sprite in _sprites)
            {
                sprite.Draw(spriteBatch, _depth);
            }
        }

        private void MovingLayer(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            float speed = deltaTime * _scrollingSpeed;

            if (!_selfMoving || Global.Player.Velocity.X != 0)
                speed += Global.Player.Velocity.X * deltaTime;

            foreach (Sprite sprite in _sprites)
            {
                sprite.Position.X -= speed;
            }
        }

        private void CirclingSprite()
        {
            for (int i = 0; i < _repeat; i++)
            {
                Sprite sprite = _sprites[i];

                if (sprite.TextureBox.Right < 0)
                {
                    int lastId = (i - 1 < 0) ? _repeat - 1 : i - 1;
                    sprite.Position.X = _sprites[lastId].TextureBox.Right - _repeat;
                } 
            }
        }
    }
}
