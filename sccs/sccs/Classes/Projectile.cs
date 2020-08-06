using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sccs.Engines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sccs.Classes
{
    public class Projectile : Entity, IPhysics
    {
        public int damage { get; private set; }
        public ElementEngine.Elements element { get; private set; }

        public PhysicsEngine physicsEngine { get; private set; }

        public StatusEffect statusEffect
        {
            get
            {
                //return new StatusEffect()
                //{

                //};
                return null;
            }
        }

        public Rectangle collisionBox => throw new NotImplementedException();

        private float timer;
        public const float lifespan = 20f;

        public Projectile(int damage, ElementEngine.Elements element, int speed, Texture2D texture, PhysicsEngine physicsEngine)
        {
            this.damage = damage;
            this.element = element;
            this.physicsEngine = physicsEngine;
            this.texture = texture;
            Speed = speed;
            exists = true;
        }

        public override void Update(GameTime gameTime, List<IPhysics> entities)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (timer > lifespan)
            {
                exists = false;
            }

            //TODO: make the projectile move
        }
        public void onCollision(IPhysics entity)
        {
            exists = false;
            //TODO: make it so this thing DISAPPEARS. rn it's only a boolean that does nothing
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
