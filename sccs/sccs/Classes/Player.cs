using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using sccs.Engines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace sccs
{
    class Player : Entity, IPhysics
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
                    return new Rectangle((int)Position.X, (int)Position.Y, texture.Width, texture.Height);
                }
                else throw new Exception("Error making collisionBox, textures/animation are null");
            }
        }

        public PhysicsEngine physicsEngine { get; private set; }

        public Player(Vector2 startingPosition, PhysicsEngine physicsEngine)
        {
            Speed = 2;
            Scale = 1;
            Position = startingPosition;
            this.physicsEngine = physicsEngine;
        }


        public override void LoadTexture(ContentManager content)
        {
            animations = new Dictionary<string, Animation>()
            {
                {"Idle",new Animation (content.Load<Texture2D>("eric-griffin/eric-griffin-idle"),2 , new float[]{5f, .5f })},
                {"WalkUp",new Animation (content.Load<Texture2D>("eric-griffin/eric-griffin-walking-forward"), 4, .15f)},
                {"WalkDown",new Animation (content.Load<Texture2D>("eric-griffin/eric-griffin-walking-back"), 1, .15f)},
                {"WalkRight",new Animation (content.Load<Texture2D>("eric-griffin/eric-griffin-walking-right"), 4, .15f)},
                {"WalkLeft",new Animation (content.Load<Texture2D>("eric-griffin/eric-griffin-walking-left"), 4, .15f)}
            };

            animationEngine = new AnimationEngine(animations["Idle"]);

        }
        public override void Update(GameTime gameTime, List<IPhysics> entities)
        {
            KeyboardState keyState = Keyboard.GetState();

            Velocity = Vector2.Zero;

            if (keyState.IsKeyDown(Keys.W))
            {
                Velocity.Y = -Speed;
            }
            if (keyState.IsKeyDown(Keys.A))
            {
                Velocity.X = -Speed;
            }
            if (keyState.IsKeyDown(Keys.S))
            {
                Velocity.Y = Speed;
            }
            if (keyState.IsKeyDown(Keys.D))
            {
                Velocity.X = Speed;
            }

            animationEngine.Update(gameTime);

            physicsEngine.detectCollision(entities, this);

            Move(Velocity);


            base.Update(gameTime, entities);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        private void Move(Vector2 velocity)///change the animation here
        {
            if (velocity == Vector2.Zero)
            {
                animationEngine.Play(animations["Idle"]);
            }
            else if (velocity.X != 0)
            {
                if (velocity.X < 0)
                {
                    animationEngine.Play(animations["WalkLeft"]);
                }
                if (velocity.X > 0)
                {
                    animationEngine.Play(animations["WalkRight"]);
                }
            }
            else if (velocity.Y < 0)
            {
                animationEngine.Play(animations["WalkDown"]);
            }
            else if (velocity.Y > 0)
            {
                animationEngine.Play(animations["WalkUp"]);
            }

            Position += velocity;

        }

        public void onCollision(IPhysics entity)
        {
            if (entity is Tile)///checks if the entity is a Tile and if it has a rectangle
            {
                Velocity = Vector2.Zero;
            }
        }
    }
}
