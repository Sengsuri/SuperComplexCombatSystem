using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using sccs.Engines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace sccs
{
    class BasicEnemyAI : Entity, IPhysics
    {
        public Character character;

        List<Weapon> weapons;

        Weapon currentWeapon;

        public Rectangle detectionBox;

        const float wait = 0.5f;

        const float action = 3f;

        Timer timer;

        Player player;

        public PhysicsEngine physicsEngine { get; private set; }

        public Rectangle collisionBox
        {
            get
            {
                if (character.texture != null)
                {
                    return new Rectangle((int)Position.X, (int)Position.Y, character.texture.Width, character.texture.Height);
                }
                else if (animationEngine != null)
                {
                    return
       new Rectangle((int)Position.X + 16, (int)Position.Y + 16, animationEngine.Width, animationEngine.Height);
                }
                else throw new Exception("Error making collisionBox, textures/animation are null");
            }
        }

        public BasicEnemyAI(Vector2 startingPosition, PhysicsEngine physicsEngine)
        {
            Scale = 1;
            Position = startingPosition;
            this.physicsEngine = physicsEngine;
            exists = true;
            timer = new Timer();
            timers.Add(timer);
            timer.SetCoolDown(action, MoveToPlayer);
        }

        public override void LoadTexture(ContentManager content)
        {
            animationEngine = new AnimationEngine();
            character = new EvilSquare(animationEngine, elementEngine);
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
            player = (Player)entities.Find(x => x is Player);
            
            if (timer.timerDone)
            {
                if (timer.isCooldown)
                {
                    timer.SetCountDown(wait, null);
                }
                else
                {
                    timer.SetCoolDown(action, MoveToPlayer);
                }
            }

            base.Update(gameTime, entities);
        }

        public void MoveToPlayer()
        {
            //the npc will try to get close to the player, and when the npc is close enough it will attack
            Vector2 targetPosition = player.Position;
            Vector2 velocity = Vector2.Zero;
            if (Position.X != targetPosition.X)
            {
                velocity.X = (targetPosition.X > Position.X) ? Speed : -Speed;
            }

            if (Position.Y != targetPosition.Y)
            {
                velocity.Y = (targetPosition.Y > Position.Y) ? Speed : -Speed;
            }

            if (detectionBox.Intersects(player.collisionBox))
            {
                character.BasicAttack(currentWeapon.Attack);
            }

            Move(velocity);
        }

        public void Move(Vector2 velocity)
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
            else if (velocity.Y < 0)
            {
                character.WalkDown();
            }
            else if (velocity.Y > 0)
            {
                character.WalkUp();
            }

            Position += velocity;
        }

        public void onCollision(IPhysics entity)
        {
        }
    }
}
