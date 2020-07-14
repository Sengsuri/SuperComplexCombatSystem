using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using sccs.Engines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sccs
{
    /// <summary>
    /// The GameState class is mostly used to initialize and manage most of the background stuff
    /// while the levelState manages the ingame entities and events.
    /// generally it's not necessary to modify the GameState class when modding, and is only a basis for creating levels
    /// </summary>
    public abstract class GameState : State
    {

        protected PhysicsEngine physicsEngine;

        protected List<Entity> entities;

        protected List<IPhysics> interactables;///interactables are things that use physics

        protected RenderTarget2D renderTarget;///the render target is the base resolution that the game is in. the graphics class will then scale the render target to the right size

        protected Camera camera;

        public static int gameHeight;
        public static int gameWidth;

        private static int windowWidth;
        private static int windowHeight;

        private float scale;


        public GameState(game _game, GraphicsDevice graphicsDevice, ContentManager content)
             : base(_game, graphicsDevice, content)
        {
            physicsEngine = new PhysicsEngine();

            windowWidth = graphicsDevice.Viewport.Width;
            windowHeight = graphicsDevice.Viewport.Height;

            renderTarget = new RenderTarget2D(graphicsDevice, 800, 450);

            gameWidth = renderTarget.Width;
            gameHeight = renderTarget.Height;

            interactables = new List<IPhysics>();

            entities = new List<Entity>();

            camera = new Camera(gameWidth, gameHeight);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);


        }

        // the render target doesn't render target
        //cause: the graphicsDevice viewport for whatever reason changes to the render target dimensions, messing with the scale
        //fix: isolate the viewport dimensions into its own variable before the render target is created
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);

            scale = 1f / ((float)renderTarget.Height / windowHeight);

            graphicsDevice.SetRenderTarget(null);
            graphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            spriteBatch.Draw(renderTarget, Vector2.Zero, null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
            spriteBatch.End();
        }


    }
}
