using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace sccs.Engines
{

    public class PhysicsEngine
    {

        public virtual void detectCollision(List<IPhysics> entities, IPhysics targetEntity)
        {
            Entity _targetEntity = (Entity)targetEntity;
            ///although the name is entities, this list also includes non-entity items such as tiles
            foreach (var entity in entities)
            {

                if (entity == targetEntity)
                    continue;

                if (entity == null)
                    continue;

                if ((_targetEntity.Velocity.X > 0 && IsTouchingLeft(targetEntity.collisionBox, entity.collisionBox, _targetEntity.Velocity))
                    || (_targetEntity.Velocity.X < 0 & IsTouchingRight(targetEntity.collisionBox, entity.collisionBox, _targetEntity.Velocity)))
                    targetEntity.onCollision(entity);

                if ((_targetEntity.Velocity.Y > 0 && IsTouchingTop(targetEntity.collisionBox, entity.collisionBox, _targetEntity.Velocity))
                    || (_targetEntity.Velocity.Y < 0 & IsTouchingBottom(targetEntity.collisionBox, entity.collisionBox, _targetEntity.Velocity)))
                    targetEntity.onCollision(entity);

                //if (targetEntity.collisionBox.Intersects(entity.collisionBox))
                //{
                //    targetEntity.onCollision(entity);
                //}
            }

        }

        #region collision
        protected bool IsTouchingLeft(Rectangle targetRect, Rectangle entityRect, Vector2 velocity)
        {
            return targetRect.Right + velocity.X > entityRect.Left &&
              targetRect.Left < entityRect.Left &&
              targetRect.Bottom > entityRect.Top &&
              targetRect.Top < entityRect.Bottom;
        }

        protected bool IsTouchingRight(Rectangle targetRect, Rectangle entityRect, Vector2 velocity)
        {
            return targetRect.Left + velocity.X < entityRect.Right &&
              targetRect.Right > entityRect.Right &&
              targetRect.Bottom > entityRect.Top &&
             targetRect.Top < entityRect.Bottom;
        }

        protected bool IsTouchingTop(Rectangle targetRect, Rectangle entityRect, Vector2 velocity)
        {
            return targetRect.Bottom + velocity.Y > entityRect.Top &&
              targetRect.Top < entityRect.Top &&
              targetRect.Right > entityRect.Left &&
              targetRect.Left < entityRect.Right;
        }

        protected bool IsTouchingBottom(Rectangle targetRect, Rectangle entityRect, Vector2 velocity)
        {
            return targetRect.Top + velocity.Y < entityRect.Bottom &&
              targetRect.Bottom > entityRect.Bottom &&
              targetRect.Right > entityRect.Left &&
              targetRect.Left < entityRect.Right;
        }

        #endregion
    }
}
