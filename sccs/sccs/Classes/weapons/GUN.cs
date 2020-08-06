using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using sccs.Classes;
using sccs.Engines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sccs
{
    public class GUN : Weapon
    {
        //TODO: set rotatation to the arm
        //and make a cooldown somehow

        Projectile projectile;

        public GUN(ElementEngine elementEngine, PhysicsEngine physicsEngine) : base(elementEngine, physicsEngine)
        {
        }

        public override int damage { get { return 10; } }

        public override void Attack()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// where both the gun and the projectile is loaded
        /// </summary>
        public override void LoadTexture(ContentManager content)
        {
            texture = content.Load<Texture2D>("");
            Texture2D projectileTexture = content.Load<Texture2D>("");
            projectile = new Projectile(10, ElementEngine.Elements.NORMAL, 15, projectileTexture, physicsEngine);
        }

        public override void Update(GameTime gameTime, List<IPhysics> entities)
        {
            base.Update(gameTime, entities);
        }
    }
}
