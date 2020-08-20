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

namespace sccs.Classes
{
    class BasicEnemyAI : Entity, IPhysics
    {
        public Character character;

        List<Weapon> weapons;

        Weapon currentWeapon;

        public Rectangle detectionBox;

        public PhysicsEngine physicsEngine { get; private set; }

        public Rectangle collisionBox => throw new NotImplementedException();

        public override void LoadTexture(ContentManager content)
        {
            animationEngine = new AnimationEngine();
            //TODO: create character here. also set the speed

        }

        public override void Update(GameTime gameTime, List<IPhysics> entities)
        {
            //the npc will try to get close to the player, and when the npc is close enough it will attack

            Player player = (Player)entities.Find(x => x is Player);

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

            Thread.Sleep(500);///pause for half a second

            base.Update(gameTime, entities);
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
            throw new NotImplementedException();
        }
    }
}
