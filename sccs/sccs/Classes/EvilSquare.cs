using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using sccs.Engines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sccs
{
    public class EvilSquare : Entity, IPhysics
    {

        public Rectangle collisionBox
        {
            get
            {
                if (animationEngine != null)
                {
                    return new Rectangle((int)Position.X + 16, (int)Position.Y + 16, animationEngine.Width, animationEngine.Height);
                }
                else if (texture != null)
                {
                    return new Rectangle((int)Position.X + 16, (int)Position.Y + 16, (int)(texture.Width * Scale), (int)(texture.Height * Scale));
                }
                else throw new Exception("Error making collisionBox, textures/animation are null");
            }
        }

        public PhysicsEngine physicsEngine { get; private set; }

        public EvilSquare(Vector2 startingPosition, PhysicsEngine physicsEngine)
        {
            this.physicsEngine = physicsEngine;
            Position = startingPosition;
            Scale = .5f;
            Speed = 5;
        }


        public override void LoadTexture(ContentManager content)
        {
            texture = content.Load<Texture2D>("evil-square");
        }

        public void onCollision(IPhysics entity)
        {
            if (entity is Tile)///checks if the entity is a Tile and if it has a rectangle
            {
                Velocity = Vector2.Zero;
            }

        }

        public override void Update(GameTime gameTime, List<IPhysics> entities)
        {
            KeyboardState keyState = Keyboard.GetState();

            Velocity = Vector2.Zero;

            //if (keyState.IsKeyDown(Keys.W))
            //{
            //    Velocity.Y = -Speed;
            //}
            //if (keyState.IsKeyDown(Keys.A))
            //{
            //    Velocity.X = -Speed;
            //}
            //if (keyState.IsKeyDown(Keys.S))
            //{
            //    Velocity.Y = Speed;
            //}
            //if (keyState.IsKeyDown(Keys.D))
            //{
            //    Velocity.X = Speed;
            //}


            physicsEngine.detectCollision(entities, this);

            Move(Velocity);

            base.Update(gameTime, entities);
        }

        private void Move(Vector2 velocity)
        {
            Position += velocity;
        }
    }
}
