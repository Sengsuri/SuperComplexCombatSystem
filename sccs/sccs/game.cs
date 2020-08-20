using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace sccs
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class game : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private State currentState;
        private State nextState;

        public Matrix scale;

        const int targetWidth = 800;

        const int targetHeight = 450;

        public game()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {

            base.Initialize();///initializes the graphics device and calls LoadContent(). should have all the code below so the tilemap textures are loaded before it's generated          

            IsMouseVisible = true;
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferWidth = 1920;///this is the actual size of the window 
            graphics.PreferredBackBufferHeight = 1080;
            graphics.ApplyChanges();


            float scaleX = graphics.PreferredBackBufferWidth / targetWidth;
            float scaleY = graphics.PreferredBackBufferHeight / targetHeight;
            scale = Matrix.CreateScale(scaleX, scaleY, 1);

        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            /// Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            currentState = new MenuState(this, graphics.GraphicsDevice, Content);
        }

        public void ChangeState(State state)
        {
            nextState = state;
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (nextState != null)
            {
                currentState = nextState;

                nextState = null;
            }

                currentState.Update(gameTime);

                base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            currentState.Draw(gameTime, spriteBatch);

            base.Draw(gameTime);
        }
    }
}
