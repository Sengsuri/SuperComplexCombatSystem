using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace sccs
{

    /// <summary>
    /// This level will not exist in the game and is a testing ground for things
    /// </summary>
    /// Levels are inherited from GameState, which is inherited from State.
    public class LevelZero : GameState
    {

        TileMap tileMap;
        public LevelZero(game _game, GraphicsDevice graphicsDevice, ContentManager content)
             : base(_game, graphicsDevice, content)
        {

            #region Generate TileMap
            tileMap = new TileMap();
            TileMap.Tiles G = TileMap.Tiles.Grass;
            TileMap.Tiles W = TileMap.Tiles.Wall;
            TileMap.Tiles E = TileMap.Tiles.Empty;///DO NOT ADD Empty to tilesUsed, it WILL cause Errors

            tileMap.tilesUsed.Add(G);
            tileMap.tilesUsed.Add(W);

            tileMap.LoadTextures(content);


            //Eventually make it so that it generates a tilemap from a file in order to make it easier to make maps
            //However the LevelState and GameState still won't be irrelevant because they could be used to script tutorial levels
            tileMap.GenerateTileMap(
                new TileMap.Tiles[,]
                {
                    { E,W,W,W,W,W,W,W,W,W,W,W,W,W,W,W,W,W,W,W,W,E},
                    { W,G,G,G,G,G,G,G,G,G,G,G,G,G,G,G,G,G,G,G,G,W},
                    { W,G,G,G,G,G,G,G,G,G,G,G,G,G,G,G,G,G,G,G,G,W},
                    { W,G,G,G,G,G,G,G,G,G,G,G,G,G,G,G,G,G,G,G,G,W},
                    { W,G,G,G,G,G,G,G,G,G,G,G,G,G,G,G,G,G,G,G,G,W},
                    { W,G,G,G,G,G,G,G,G,G,G,G,G,G,G,G,G,G,G,G,G,W},
                    { W,G,G,G,G,G,G,G,G,G,G,G,G,G,G,G,G,G,G,G,G,W},
                    { W,G,G,G,G,G,G,G,G,G,G,G,G,G,G,G,G,G,G,G,G,W},
                    { W,G,G,G,G,G,G,G,G,G,G,G,G,G,G,G,G,G,G,G,G,W},
                    { W,G,G,G,G,G,G,G,G,G,G,G,G,G,G,G,G,G,G,G,G,W},
                    { W,G,G,G,G,G,G,G,G,G,G,G,G,G,G,G,G,G,G,G,G,W},
                    { W,G,G,G,G,G,G,G,G,G,G,G,G,G,G,G,G,G,G,G,G,W},
                    { W,G,G,G,G,G,G,G,G,G,G,G,G,G,G,G,G,G,G,G,G,W},
                    { W,G,G,G,G,G,G,G,G,G,G,G,G,G,G,G,G,G,G,G,G,W},
                    { W,G,G,G,G,G,G,G,G,G,G,G,G,G,G,G,G,G,G,G,G,W},
                    { W,G,G,G,G,G,G,G,G,G,G,G,G,G,G,G,G,G,G,G,G,W},
                    { W,G,G,G,G,G,G,G,G,G,G,G,G,G,G,G,G,G,G,G,G,W},
                    { W,G,G,G,G,G,G,G,G,G,G,G,G,G,G,G,G,G,G,G,G,W},
                    { W,G,G,G,G,G,G,G,G,G,G,G,G,G,G,G,G,G,G,G,G,W},
                    { W,G,G,G,G,G,G,G,G,G,G,G,G,G,G,G,G,G,G,G,G,W},
                    { W,G,G,G,G,G,G,G,G,G,G,G,G,G,G,G,G,G,G,G,G,W},
                    { W,G,G,G,G,G,G,G,G,G,G,G,G,G,G,G,G,G,G,G,G,W},
                    { E,W,W,W,W,W,W,W,W,W,W,W,W,W,W,W,W,W,W,W,W,E} });

            foreach (Tile tile in tileMap.tileMap)
            {
                if (tile.collisionBox != new Rectangle(0, 0, 0, 0))///Rectangle can't be nullable so a recatangle with zeros is used as a placeholder 
                {
                    interactables.Add(tile);
                }
            }
            #endregion


            entities.Add(new Player(new Vector2(50, 50), physicsEngine));
            entities.Add(new EvilSquare(new Vector2(100, 100), physicsEngine));

            //weapons will be managed by each character, they are added within the levelState for testing purposes

                foreach (Entity entity in entities)
                {
                    entity.LoadTexture(content);
                }
            interactables.Add((IPhysics)entities.Find(x => x is Player));
            interactables.Add((IPhysics)entities.Find(x => x is EvilSquare));
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            camera.Follow(entities.Find(x => x is Player));

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                _game.Exit();
            }

            foreach (Entity entity in entities)
            {
                entity.Update(gameTime, interactables);
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

            graphicsDevice.SetRenderTarget(null);
            graphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(transformMatrix: camera.Transform * _game.scale);
            tileMap.Draw(spriteBatch);
            foreach (Entity entity in entities)
            {
                entity.Draw(spriteBatch);
            }
            spriteBatch.End();

            base.Draw(gameTime, spriteBatch);///This line must always be last in the method
        }
    }
}
