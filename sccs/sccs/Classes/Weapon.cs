using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sccs.Engines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sccs
{
    public abstract class Weapon : Entity
    {
        public abstract int damage { get; }

        protected PhysicsEngine physicsEngine;

        //temp constructor for creative purposes, might be permanent
        public Weapon(ElementEngine elementEngine, PhysicsEngine physicsEngine)
        {
            this.elementEngine = elementEngine;
            this.physicsEngine = physicsEngine;
            Scale = 1;
        }
        public abstract void Attack();

    }
}
