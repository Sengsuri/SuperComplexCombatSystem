using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace sccs
{
    public class Camera
    {
        public Matrix Transform { get; private set; }

        private static int screenWidth;
        private static int screenHeight;

        public Camera(int _screenWidth, int _screenHeight)
        {
            screenWidth = _screenWidth;
            screenHeight = _screenHeight;
        }

        public void Follow(Entity target)
        {
            Vector2 mousePosition = new Vector2(Mouse.GetState().X - screenWidth / 2, Mouse.GetState().Y - screenHeight / 2);
            ///position of the target
            var position = Matrix.CreateTranslation(
               -target.Position.X - (target.dRect.Width / 2),
               -target.Position.Y - (target.dRect.Height / 2),
                0);


            ///offset the target to the middle of the screen
            //TODO: include the mouse position so the camera moves with the mouse
            var offset = Matrix.CreateTranslation(
            screenWidth / 2,
            screenHeight / 2,
             0);           

            Transform = position * offset;
        }

    }
}
