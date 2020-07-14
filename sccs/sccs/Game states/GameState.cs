﻿using Microsoft.Xna.Framework;
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

        public int screenHeight;
        public int screenWidth;
        private float scale;


        public GameState(game _game, GraphicsDevice graphicsDevice, ContentManager content)
             : base(_game, graphicsDevice, content)
        {
            physicsEngine = new PhysicsEngine();

            renderTarget = new RenderTarget2D(graphicsDevice, 800, 450);

            screenWidth = renderTarget.Width;
            screenHeight = renderTarget.Height;

            interactables = new List<IPhysics>();

            entities = new List<Entity>();

            camera = new Camera(this);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            camera.Follow(entities[0]);

            foreach (Entity entity in entities)
            {
                entity.Update(gameTime, interactables);
            }
        }

        //TODO the render target doesn't render target
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);

            scale = 1f / ((float)renderTarget.Height / graphicsDevice.Viewport.Height);

            graphicsDevice.SetRenderTarget(null);
            graphicsDevice.Clear(Color.CornflowerBlue);


            spriteBatch.Begin();
            spriteBatch.Draw(renderTarget, Vector2.Zero, null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
            spriteBatch.End();
        }


    }
}
