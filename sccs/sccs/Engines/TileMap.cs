using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace sccs
{
    /// <summary>
    /// This class generates the tiles in a level
    /// The tiles are stored in arrays, and are identified with the enum
    /// first the textures are loaded(theoretically)
    /// then the tiles are created in GenerateTileMap
    /// then the tiles are drawn
    /// </summary>
    class TileMap
    {
        public enum Tiles ///list of all the tile names used in the game, Make sure the names here match the names of the files
        {
            Grass, Wall, Empty
        }

        Dictionary<string, Texture2D> tileTextures;

        public List<Tiles> tilesUsed = new List<Tiles>();///used as an easy way to create a tile map from outside the class without dealing with creating tiles, etc

        public List<Tile> tileMap { get; private set; }



        public void GenerateTileMap(Tiles[,] tiles)
        {
            Random rand = new Random();
            tileMap = new List<Tile>();

            for (int x = 0; x <= tiles.GetUpperBound(1); x++)
            {
                for (int y = 0; y <= tiles.GetUpperBound(0); y++)
                {
                    Tiles getTile = tiles[y, x];///don't ask me why this is backwards, idk either

                    Vector2 tilePosition = new Vector2(x * 32, y * 32);///32 is the size of the tile

                    switch (getTile)
                    {
                        case Tiles.Grass:

                            float rotation = 0f;
                            switch (rand.Next(4))
                            {
                                case 0:
                                    rotation = 0f;
                                    break;
                                case 1:
                                    rotation = 90f;
                                    break;
                                case 2:
                                    rotation = 180f;
                                    break;
                                case 3:
                                    rotation = 270f;
                                    break;
                            }
                            tileMap.Add(new Tile(tileTextures["Grass"], tilePosition, rotation));
                            break;

                        case Tiles.Wall:
                            if ((x != tiles.GetUpperBound(1) && x != tiles.GetLowerBound(1)) &&
                                (tiles[y, x + 1].Equals(Tiles.Wall) || (tiles[y, x - 1].Equals(Tiles.Wall))))
                            {
                                tileMap.Add(new Tile(tileTextures["Wall"], tilePosition, 90f, true));
                            }
                            else
                            {
                                tileMap.Add(new Tile(tileTextures["Wall"], tilePosition, true));
                            }
                            break;
                        case Tiles.Empty:
                            continue;
                        default:
                            break;
                    }
                }
            }
        }


        public void LoadTextures(ContentManager content)
        {
            tileTextures = new Dictionary<string, Texture2D>();
            foreach (Tiles tile in tilesUsed)
            {
                tileTextures.Add(tile.ToString(), content.Load<Texture2D>("tiles/" + tile.ToString()));
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Tile tile in tileMap)
            {
                tile.Draw(spriteBatch);
            }

        }
    }
}