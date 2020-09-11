using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using sccs.Classes;
using sccs.Classes.characters;
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
        public Character character;

        Input Input;

        List<Weapon> weapons;

        Weapon currentWeapon;

        public Vector2 centerOfScreen { private get; set; }

        /// <summary>
        /// The PowerPoint system determines how powerful a loadout is and is for balancing purposes
        /// </summary>
        public int powerPoints { get { return (int)character.powerPoints; } }

        const int sprintStamina = 1;

        public Rectangle collisionBox
        {
            get
            {
                if (animationEngine != null)
                {
                    return
                        new Rectangle((int)Position.X + 16, (int)Position.Y + 16, animationEngine.Width, animationEngine.Height);
                }
                else if (character.texture != null)
                {
                    return new Rectangle((int)Position.X, (int)Position.Y, character.texture.Width, character.texture.Height);
                }
                else throw new Exception("Error making collisionBox, textures/animation are null");
            }
        }

        public PhysicsEngine physicsEngine { get; private set; }

        public Player(Vector2 startingPosition, PhysicsEngine physicsEngine)
        {
            Scale = 1;
            Position = startingPosition;
            this.physicsEngine = physicsEngine;
            exists = true;
            Input = new Input();
        }

        //TODO: have the level state send the character being used to the player
        public override void LoadTexture(ContentManager content)
        {
            animationEngine = new AnimationEngine();
            //just remember when using multiples of the same character to alway create a new instance of that character 
            //character = new EricGriffin(animationEngine, elementEngine);
            //TODO: reference the elementEngine because it is never initialised 
            character = new DefaultCharacter(animationEngine, elementEngine);
            character.LoadTextures(content);
            Speed = character.speed;
            if (character.texture != null)
            {
                texture = character.texture;
            }
            else
            {
                animationEngine.animation = character.startingAnimation();
            }

        }
        public override void Update(GameTime gameTime, List<IPhysics> entities)
        {
            ///basic inputs such as moving and basic attacks are handled by this class.
            ///any special attacks are handled by the character class, as well as any additional inputs

            KeyboardState keyState = Keyboard.GetState();

            int boost = 0;
            Velocity = Vector2.Zero;

            //ihatethisihatethisihatethisihatethisihatethisihatethisihatethisihatethisihatethis
            //TODO:god i want to chnage this implementation so bad
            if (keyState.IsKeyDown(Input.Sprint) && character.stamina > 0)
            {
                boost = character.maxSpeed - Speed;
            }
            else if (!keyState.IsKeyDown(Input.Sprint))
            {
                character.RegenStamina(gameTime);
            }
            if (keyState.IsKeyDown(Input.Up))
            {
                Velocity.Y = -character.speed - boost;
            }
            if (keyState.IsKeyDown(Input.Left))
            {
                Velocity.X = -character.speed - boost;
            }
            if (keyState.IsKeyDown(Input.Down))
            {
                Velocity.Y = character.speed + boost;
            }
            if (keyState.IsKeyDown(Input.Right))
            {
                Velocity.X = character.speed + boost;
            }
            if (keyState.IsKeyDown(Input.Primary))
            {
                doingAction = true;
                character.BasicAttack(currentWeapon.Attack);
            }

            if (boost > 0)
            {
                character.SubtractStamina(sprintStamina, gameTime);
            }


            MouseState mouse = Mouse.GetState();
            Vector2 mousePosition = new Vector2(mouse.X, mouse.Y);

            Vector2 direction = mousePosition - centerOfScreen;
            direction.Normalize();

            character.armRotation = (float)Math.Atan2(direction.Y, direction.X) + MathHelper.ToRadians(270);

            animationEngine.Update(gameTime);

            physicsEngine.detectCollision(entities, this);

            doingAction = false;

            Move(Velocity);

            base.Update(gameTime, entities);
        }

        private void Move(Vector2 velocity)///change the animation here
        {
            if (velocity == Vector2.Zero)
            {
                character.Idle();
            }
            else if (velocity.X != 0)
            {
                if (velocity.X < 0)
                {
                    character.WalkLeft();
                }
                if (velocity.X > 0)
                {
                    character.WalkRight();
                }
            }
            else if (velocity.Y > 0)
            {
                character.WalkDown();
            }
            else if (velocity.Y < 0)
            {
                character.WalkUp();
            }

            Position += velocity;
        }


        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            character.Draw(spriteBatch, Position);
        }

        public void onCollision(IPhysics entity)
        {
            if (entity is Tile)///checks if the entity is a Tile and if it has a rectangle
            {
                Velocity = Vector2.Zero;
            }
            if (entity is Projectile)
            {
                Projectile projectile = (Projectile)entity;
                character.SubtractHealth(projectile.damage, projectile.element, projectile.statusEffect);
            }

            //TODO:implement enemy collisions
            //also TODO: make a better implementation
        }
    }
}
