using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace sccs
{
    public class Bar : IEffect
    {
        Texture2D backgroundTexture;

        Texture2D barTexture;

        float barWidth;

        float maxWidth;

        Color baseColor;

        Color color;///current color

        Vector2 position;

        public Action ApplyEffects { get; set; }

        public Bar(Vector2 position, Color baseColor)
        {
            this.position = position;
            this.baseColor = baseColor;


            color = baseColor;
        }

        public void LoadContent(ContentManager content)
        {
            backgroundTexture = content.Load<Texture2D>("UI/UIBarBackground");
            barTexture = content.Load<Texture2D>("UI/UIBar");
            maxWidth = barTexture.Width;
            barWidth = maxWidth;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            ApplyEffects?.Invoke();
            spriteBatch.Draw(backgroundTexture, position);
            Rectangle r = new Rectangle((int)position.X + 1, (int)position.Y + 1, (int)barWidth, barTexture.Height);
            spriteBatch.Draw(barTexture, r, color);
        }

        public void changeColor()
        {
            //using shaders and seeing if that'll work out
            //switch (barWidth / maxwidth)
            //{
            //    case 0.10f:
            //        color.
            //        break;
            //    default:
            //        color = baseColor;
            //        break;
            //}
        }

        /// <summary>
        /// Updates how wide the bar is
        /// </summary>
        /// <param name="value"></param>
        public void changeBar(float percentValue)
        {
            barWidth = maxWidth * percentValue;
        }
    }


}
