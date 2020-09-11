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

        protected List<Bar> bars;///yo

        protected List<IPhysics> interactables;///interactables are things that use physics

        //protected RenderTarget2D renderTarget;///the render target is the base resolution that the game is in. the graphics class will then scale the render target to the right size

        protected RenderTarget2D UIRender;

        protected Camera camera;

        public static int gameHeight;
        public static int gameWidth;

        //private static int windowWidth;
        //private static int windowHeight;

        Bar stamina;
        Bar mana;
        Bar health;

        Effect colorChanger;

        public GameState(game _game, GraphicsDevice graphicsDevice, ContentManager content)
             : base(_game, graphicsDevice, content)
        {
            physicsEngine = new PhysicsEngine();


            //windowWidth = graphicsDevice.Viewport.Width;
            //windowHeight = graphicsDevice.Viewport.Height;

            //outputAspect = windowWidth / (float)windowHeight;

            //if (outputAspect <= preferredAspect)
            //{
            //    // output is taller than it is wider, bars on top/bottom
            //    int presentHeight = (int)((windowWidth / preferredAspect) + 0.5f);
            //    int barHeight = (windowHeight - presentHeight) / 2;
            //    dst = new Rectangle(0, barHeight, windowWidth, presentHeight);
            //}
            //else
            //{
            //    // output is wider than it is tall, bars left/right
            //    int presentWidth = (int)((windowHeight * preferredAspect) + 0.5f);
            //    int barWidth = (windowWidth - presentWidth) / 2;
            //    dst = new Rectangle(barWidth, 0, presentWidth, windowHeight);
            //}            

            //renderTarget = new RenderTarget2D(graphicsDevice, 800, 450);


            //gameWidth = renderTarget.Width;
            //gameHeight = renderTarget.Height;

            gameWidth = 800;
            gameHeight = 450;

            colorChanger = content.Load<Effect>("Effects/ColorChanger");

            interactables = new List<IPhysics>();

            entities = new List<Entity>();

            health = new Bar(new Vector2(25, 475), Color.LawnGreen);
            mana = new Bar(new Vector2(25, 440), Color.SteelBlue);
            stamina = new Bar(new Vector2(25, 405), Color.LightGray);

            bars = new List<Bar> {
                health,
                mana,
                stamina
            };

            foreach (Bar bar in bars)
            {
                bar.LoadContent(content);
            }

            camera = new Camera(gameWidth, gameHeight);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);           
            ///update UI logic
            Player player = (Player)entities.Find(x => x is Player);

            health.changeBar(player.character.health / player.character.maxHealth);
            stamina.changeBar(player.character.stamina / player.character.maxStamina);
            mana.changeBar(player.character.mana / player.character.maxMana);
        }

        // the render target doesn't render target
        //cause: the graphicsDevice viewport for whatever reason changes to the render target dimensions, messing with the scale
        //fix: isolate the viewport dimensions into its own variable before the render target is created
        //new fix: lol just use matrices
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);

            //scale = 1f / ((float)renderTarget.Height / windowHeight);
            //graphicsDevice.SetRenderTarget(null);
            //graphicsDevice.Clear(Color.CornflowerBlue);


            //spriteBatch.Begin(SpriteSortMode.Immediate, transformMatrix: _game.scale);
            //spriteBatch.Draw(renderTarget, Vector2.Zero, null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
            ////spriteBatch.Draw(renderTarget, dst, Color.White);

            //spriteBatch.End();

            //here's how effects will work. if 

            

            ///update UI draw
            spriteBatch.Begin(SpriteSortMode.Immediate, transformMatrix: _game.scale);

            foreach (Bar bar in bars)
            {
                bar.Draw(spriteBatch);
            }
            spriteBatch.End();
        }
    }
}
