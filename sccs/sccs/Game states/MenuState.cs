using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sccs
{
    public class MenuState : State
    {
        List<Button> buttons = new List<Button>();

        public MenuState(game _game, GraphicsDevice graphicsDevice, ContentManager content)
            : base(_game, graphicsDevice, content)
        {
            var newGameButton = new Button(content.Load<Texture2D>("UI/BUTTON"), content.Load<SpriteFont>("SpriteFonts/defaultSpriteFont"))
            {
                Position = new Vector2(100, 100),
                Rectangle = new Rectangle(200, 100, 400, 150),
                Text = "New Game"
            };

            newGameButton.Click += NewGame_Click;

            var quit = new Button(content.Load<Texture2D>("UI/BUTTON"), content.Load<SpriteFont>("SpriteFonts/defaultSpriteFont"))
            {
                Position = new Vector2(100, 150),
                Rectangle = new Rectangle(200, 300, 400, 150),
                Text = "Quit"
            };

            quit.Click += quit_Click;

            buttons.Add(newGameButton);
            buttons.Add(quit);
        }


        private void NewGame_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new LevelZero(_game, graphicsDevice, content));
        }
        private void quit_Click(object sender, EventArgs e)
        {
            _game.Exit();
        }
        public override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                _game.Exit();

            foreach (var button in buttons)
            {
                button.Update(gameTime);
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            graphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            foreach (var button in buttons)
            {
                button.Draw(gameTime, spriteBatch);
            }

            spriteBatch.End();
        }

    }
}
