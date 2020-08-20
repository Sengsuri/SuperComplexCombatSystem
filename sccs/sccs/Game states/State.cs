using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sccs
{
    public abstract class State
    {
        protected ContentManager content;

        protected GraphicsDevice graphicsDevice;

        protected game _game;

        public State(game _game, GraphicsDevice graphicsDevice, ContentManager content)
        {
            this.content = content;
            this.graphicsDevice = graphicsDevice;
            this._game = _game;
        }

        public virtual void Initialize()
        {

        }

        public virtual void LoadContent()
        {

        }

        public virtual void Update(GameTime gameTime)
        {

        }
        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

        }

    }
}
