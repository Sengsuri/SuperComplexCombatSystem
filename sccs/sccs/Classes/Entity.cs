using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using sccs.Engines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// Entity: a drawable thing that exist in the world I.E. the player, bulllets, items, etc.
/// </summary>
namespace sccs
{
    public class Entity
    {
        public Vector2 Velocity;

        public float Rotation;

        public float Scale;

        public int Speed;

        public bool doingAction;

        public bool exists;

        protected Vector2 _position;

        protected AnimationEngine animationEngine;

        protected ElementEngine elementEngine;

        protected Texture2D texture;

        public Vector2 Position { get { return _position; } set { _position = value; if (animationEngine != null) animationEngine.position = _position; } }
        public Rectangle dRect
        {
            get
            {
                if (texture != null)
                {
                    return new Rectangle((int)Position.X, (int)Position.Y, texture.Width, texture.Height);
                }
                else if (animationEngine != null)
                {
                    return new Rectangle((int)Position.X, (int)Position.Y, animationEngine.Width, animationEngine.Height);
                }
                else throw new Exception("Error making dRect, textures/animation are null");
            }
        }

        public virtual void LoadTexture(ContentManager content)
        {

        }

        public virtual void Update(GameTime gameTime, List<IPhysics> entities)
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (texture != null)
                spriteBatch.Draw(texture, Position, color: Color.White, scale: new Vector2(Scale, Scale));
            else if (animationEngine != null)
                animationEngine.Draw(spriteBatch);
            else throw new Exception("Error drawing textures, textures/animation are null");
        }
    }
}
