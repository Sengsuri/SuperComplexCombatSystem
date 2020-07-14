using Microsoft.Xna.Framework;
using sccs.Engines;

namespace sccs
{
    public interface IPhysics
    {
        PhysicsEngine physicsEngine { get; }
        Rectangle collisionBox { get; }
        
        void onCollision(IPhysics entity);
    }
}
