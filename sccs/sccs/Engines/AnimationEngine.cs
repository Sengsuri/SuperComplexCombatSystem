using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sccs
{
    /// <summary>
    /// This is what takes the animation and it's properties and runs it
    /// </summary>
    public class AnimationEngine

    {
        public Animation animation { get; set; }

        private float timer;

        public Vector2 position { get; set; }

        public int Width { get { return animation.frameWidth; } }
        public int Height { get { return animation.frameHeight; } }


        /// <summary>
        /// The parameter is for the starting animation
        /// </summary>
        /// <param name="animation"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            if (animation != null)
            {
                spriteBatch.Draw(animation.texture,
             position,
             new Rectangle(animation.currentFrame * animation.frameWidth,
             0,
             animation.frameWidth,
             animation.frameHeight),
             Color.White);
            }
        }

        /// <summary>
        /// plays a new animation
        /// </summary>
        /// <param name="animation"></param>
        public void Play(Animation animation)
        {
            if (this.animation == animation)
                return;

            this.animation = animation;

            animation.currentFrame = 0;

            timer = 0f;
        }


        public void stop()
        {
            timer = 0;

            animation.currentFrame = 0;
        }

        public void Update(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (animation.frameTimes.Length == 1)///checks if theres is more than one frame speed
            {
                updateFrame(0);
            }
            else
            {
                updateFrame(animation.currentFrame);
            }
        }

        private void updateFrame(int frameTimeIndex)
        {
            if (timer > animation.frameTimes[frameTimeIndex])
            {
                timer = 0f;

                animation.currentFrame++;

                if (animation.currentFrame >= animation.frameCount)
                    animation.currentFrame = 0;
            }
        }

    }
}
