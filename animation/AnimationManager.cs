﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rpg
{
    public class AnimationManager
    {
        // current animation we are currently performing
        private Animation animation;
        // timer to tell when to increment current frame
        private float timer;
        // where on the screen texture is
        public Vector2 Position { get; set; }

        public AnimationManager(Animation animation)
        {
            this.animation = animation;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(animation.Texture,
                             Position,
                             new Rectangle(animation.CurrentFrame * animation.FrameWidth,
                                           0,
                                           animation.FrameWidth,
                                           animation.FrameHeight),
                             Color.White);
        }

        public void Play(Animation animation)
        {
            if (this.animation == animation)
                return;

            this.animation = animation;

            this.animation.CurrentFrame = 0;

            timer = 0;
        }

        public void Stop()
        {
            timer = 0f;

            animation.CurrentFrame = 0;
        }

        public void Update(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (timer > animation.FrameSpeed)
            {
                timer = 0f;

                animation.CurrentFrame++;

                if (animation.CurrentFrame >= animation.FrameCount)
                    animation.CurrentFrame = 0;
            }
        }
    }
}