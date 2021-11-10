using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rpg
{
    public class Animation
    {
        //current frame, what we view
        public int CurrentFrame { get; set; }
        //number of frames in texture/image
        public int FrameCount { get; private set; }
        //height of frame
        public int FrameHeight { get { return Texture.Height; } }
        //how quickly we animate through frames, lower is faster
        public float FrameSpeed { get; set; }
        //how wide frames are
        public int FrameWidth { get { return Texture.Width / FrameCount; } }
        //determine whether texture needs to loop, walking does, crouching doesn't
        public bool IsLooping { get; set; }
        //image texture
        public Texture2D Texture { get; private set; }

        public Animation(Texture2D texture, int frameCount)
        {
            Texture = texture;

            FrameCount = frameCount;

            IsLooping = true; //true by default

            FrameSpeed = 0.1f;
        }
    }
}