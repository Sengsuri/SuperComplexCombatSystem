using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sccs
{
    public class Animation
    {
        public int frameCount { get; set; }///the amount of frames in the the sprite
        public int currentFrame { get; set; }
        public int frameHeight { get { return texture.Height; } }
        public int frameWidth { get { return texture.Width / frameCount; } }///the file must be so that the frames are across(columns only)
        public float[] frameTimes { get; set; }///this makes it so each frame can be drawn for a different amount of time from other frames
        public bool isLooping { get; set; }
        public Texture2D texture { get; private set; }


        public Animation(Texture2D texture, int frameCount, float frameSpeed)
        {
            frameTimes = new float[1];
            this.texture = texture;
            this.frameCount = frameCount;
            frameTimes[0] = frameSpeed;
        }

        public Animation(Texture2D texture, int frameCount, float[] frameTimes)
        {
            this.texture = texture;
            this.frameCount = frameCount;
            this.frameTimes = frameTimes;
        }




    }
}
