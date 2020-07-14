using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using sccs.Engines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sccs
{

    /// <summary>
    /// Tile sides (as in the png):
    /// 
    ///   2
    /// 0 T 1
    ///   3       
    ///   
    /// </summary>
    class Tile : IPhysics
    {
        protected Texture2D texture { get; private set; }
        protected Dictionary<string, Animation> animations;///just in case
        private float _rotation;
        public Vector2 position;
        public float rotation { get { return _rotation; } set { _rotation = MathHelper.ToRadians(value); } }

        public Rectangle collisionBox { get; private set; }

        public PhysicsEngine physicsEngine => throw new NotImplementedException();

        public bool isAnimated;
        public int northSide;///the side that's facing north

        public Tile(Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            this.position = position;
            isAnimated = false;
            collisionBox = new Rectangle(0, 0, 0, 0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="texture"></param>
        /// <param name="position"></param>
        /// <param name="rotation">Rotation in Degrees</param>
        public Tile(Texture2D texture, Vector2 position, float rotation)
        {
            this.texture = texture;
            this.position = position;
            this.rotation = rotation;
            collisionBox = new Rectangle(0, 0, 0, 0);
        }

        public Tile(Texture2D texture, Vector2 position, bool isCollidable)
        {
            this.texture = texture;
            this.position = position;
            if (isCollidable == true)
            {
                collisionBox = new Rectangle((int)position.X, (int)position.Y, 32, 32);
            }
        }

        public Tile(Texture2D texture, Vector2 position, float rotation, bool isCollidable)
        {
            this.texture = texture;
            this.position = position;
            this.rotation = rotation;
            if (isCollidable == true)
            {
                collisionBox = new Rectangle((int)position.X, (int)position.Y, 32, 32);
            }
        }

        public void Update()
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, rotation: rotation, origin: new Vector2(16, 16));
        }

        public void onCollision(IPhysics entity)
        {
            throw new NotImplementedException();
        }
    }
}
