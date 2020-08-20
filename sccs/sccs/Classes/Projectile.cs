using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sccs.Engines;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sccs
{
    public class Projectile : Entity, IPhysics
    {
        public int damage { get; private set; }
        public ElementEngine.Elements element { get; private set; }
        public PhysicsEngine physicsEngine { get; private set; }
        public Weapon parent;
        public StatusEffect statusEffect
        {
            get
            {
                return new StatusEffect(10f);
                //return null;
            }
        }
        public Rectangle collisionBox => throw new NotImplementedException();

        private float timer;
        public const float lifespan = 20f;

        public Projectile(int damage, ElementEngine.Elements element, int speed, Texture2D texture,
            PhysicsEngine physicsEngine, Weapon parent)
        {
            this.damage = damage;
            this.element = element;
            this.physicsEngine = physicsEngine;
            this.texture = texture;
            this.parent = parent;
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
            //I can probably assume that c#'s garbage collector is really smart and will automagically dispose the object
            //if it's removed from the entity list and not in use
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
