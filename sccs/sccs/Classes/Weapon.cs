using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sccs.Engines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace sccs
{
    /// <summary>
    /// this class pulls double duty and represents a weapon in game and in the loadout screen
    /// </summary>
    public abstract class Weapon : Entity, IPhysics
    {
        public abstract int damage { get; }
        public abstract string description { get; }
        public abstract Vector2 handle { get; }///where the weapon is held. obviously
        public Rectangle collisionBox => throw new NotImplementedException();
        public PhysicsEngine physicsEngine { get; set; }
        public Entity entity;
        protected float cooldown;///cooldown in seconds, might change to milliseconds
        protected float timer;

        public Weapon(ElementEngine elementEngine, PhysicsEngine physicsEngine, Entity entity)
        {
            this.elementEngine = elementEngine;
            this.physicsEngine = physicsEngine;
            this.entity = entity;
            Scale = 1;
            cooldown = 5f;
        }

        public override void Update(GameTime gameTime, List<IPhysics> entities)
        {
            if (entity.doingAction)
            {
                timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (timer > cooldown)
                {
                    entity.doingAction = false;
                    timer = 0;
                }
            }

        }

        public abstract void Attack();

        public abstract override string ToString();

        public void onCollision(IPhysics entity)
        {
            throw new NotImplementedException();
        }
    }
}
