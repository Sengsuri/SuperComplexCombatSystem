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
    /// <summary>   
    /// weapons are entities, but they must be controlled by an entity in order to function
    /// </summary>
    public class GUN : Weapon
    {
        //TODO: set rotatation to the arm
        //and make a cooldown somehow

        public override string description { get { return "issa gun"; } }

        Projectile projectile;

        public GUN(ElementEngine elementEngine, PhysicsEngine physicsEngine, Entity entity)
            : base(elementEngine, physicsEngine, entity)
        {
            cooldown = 0.5f;
        }

        public override int damage { get { return 10; } }

        public override Vector2 handle => throw new NotImplementedException();

        public override void Attack()
        {
            if (!entity.doingAction)
            {
                var _projectile = projectile.Clone() as Projectile;

                //TODO: set projectile properties here
                _projectile.Rotation = entity.Rotation;

                entity.doingAction = true;
            }
        }
        /// <summary>
        /// where both the gun and the projectile is loaded
        /// </summary>
        public override void LoadTexture(ContentManager content)
        {
            texture = content.Load<Texture2D>("");
            Texture2D projectileTexture = content.Load<Texture2D>("");
            projectile = new Projectile(10, ElementEngine.Elements.NORMAL, 15, projectileTexture,
                physicsEngine, this);
        }

        public override void Update(GameTime gameTime, List<IPhysics> entities)
        {
            base.Update(gameTime, entities);
        }

        public override string ToString()
        {
            return "GUN";
        }
    }
}
