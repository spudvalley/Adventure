using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adventure.Objects
{
    internal class Screen : GameObject
    {
        List<Tile> screenTiles;
        int gameHeight; 
        int gameWidth;
        int numTilesHigh;
        int numTilesWide;
        int xMap;
        int yMap;
        string seed;

        public Screen(Game myGame, int gameHeight, int gameWidth, int numTilesHigh, int numTilesWide, int xMap, int yMap, string inputSeed) 
            : base(myGame) 
        {
            this.gameHeight = gameHeight;
            this.gameWidth = gameWidth;
            this.numTilesHigh = numTilesHigh;
            this.numTilesWide = numTilesWide;  
            this.xMap = xMap;
            this.yMap = yMap;

            screenTiles = new List<Tile>();

            this.seed = createScreen(inputSeed);
            buildFromString(this.seed);

        }

        // handles the generation of a
        // TODO: take adjacent screens into account
        private string createScreen(string seedInput)
        {
            if (seedInput != "") 
            { 
                buildFromString(seedInput);
            }

            string seedBuilder = "";
            string seedPlusOne = "";
            int seedLength = numTilesHigh * numTilesWide;

            Random rand = new Random();

            for (int i = 0; i < seedLength; i++)
            {
                int randomVal = rand.Next(0, 2);
                if (randomVal == 0)
                {
                    seedBuilder += "B";
                    seedPlusOne += "B";
                }
                else
                {
                    seedBuilder += "A";
                    seedPlusOne += "A";
                }
            }

            List<Char> seedList = new List<Char>();
            seedList.AddRange(seedBuilder);
            List<Char> seedListPlusOne = new List<Char>();
            seedListPlusOne.AddRange(seedPlusOne);

            const int NUM_ITERS = 1;

            for (int i = 0; i < NUM_ITERS; i++)
            {
                for (int index = 0; index < seedList.Count(); index++)
                {
                    char tile = seedList[index];

                    if (tile == 'B' && surroundingSum(seedList, index) >= 3)
                    {
                        seedListPlusOne[index] = 'B';
                    }
                    else if (tile == 'A' && surroundingSum(seedList, index) >= 3)
                    {
                        seedListPlusOne[index] = 'B';
                    }
                    else
                    {
                        seedListPlusOne[index] = 'A';
                    }
                }
                seedList = new List<Char>(seedListPlusOne);
            }
            
            return new string(seedList.ToArray());
        }

        private int surroundingSum(List<Char> seedString, int index)
        {
            int sum = 0;
            List<int> adjacentList = new List<int>();
               
            // top left corner
            if (index == 0)
            {
                adjacentList.Add(index + 1);
                adjacentList.Add(index + numTilesWide);
                adjacentList.Add(index + numTilesWide + 1);
            }
            // top right corner
            else if (index == (numTilesWide - 1))
            {
                adjacentList.Add(index - 1);
                adjacentList.Add(index + numTilesWide);
                adjacentList.Add(index + numTilesWide - 1);
            }
            // bottom left corner
            else if (index == (numTilesWide * numTilesHigh) - numTilesWide)
            {
                adjacentList.Add(index + 1);
                adjacentList.Add(index - numTilesWide);
                adjacentList.Add(index - numTilesWide + 1);
            }
            // bottom right corner
            else if (index == (numTilesWide * numTilesHigh) - 1)
            {
                adjacentList.Add(index - 1);
                adjacentList.Add(index - numTilesWide);
                adjacentList.Add(index - numTilesWide - 1);
            }
            // top row
            else if (index < numTilesWide)
            {
                adjacentList.Add(index + 1);
                adjacentList.Add(index - 1);
                adjacentList.Add(index + numTilesWide);
                adjacentList.Add(index + numTilesWide + 1);
                adjacentList.Add(index + numTilesWide - 1);
            }
            // bottom row
            else if (index >= (numTilesWide * numTilesHigh) - numTilesWide)
            {
                adjacentList.Add(index + 1);
                adjacentList.Add(index - 1);
                adjacentList.Add(index - numTilesWide);
                adjacentList.Add(index - numTilesWide + 1);
                adjacentList.Add(index - numTilesWide - 1);
            }
            // left column
            else if (index % numTilesWide == 0)
            {
                adjacentList.Add(index + 1);
                adjacentList.Add(index - numTilesWide);
                adjacentList.Add(index - numTilesWide + 1);
                adjacentList.Add(index + numTilesWide);
                adjacentList.Add(index + numTilesWide + 1);
            }
            // right column
            else if ((index + 1) % numTilesWide == 0)
            {
                adjacentList.Add(index - 1);
                adjacentList.Add(index - numTilesWide);
                adjacentList.Add(index - numTilesWide - 1);
                adjacentList.Add(index + numTilesWide);
                adjacentList.Add(index + numTilesWide - 1);
            }
            // all others
            else
            {
                adjacentList.Add(index - 1);
                adjacentList.Add(index + 1);
                adjacentList.Add(index - numTilesWide);
                adjacentList.Add(index - numTilesWide - 1);
                adjacentList.Add(index - numTilesWide + 1);
                adjacentList.Add(index + numTilesWide);
                adjacentList.Add(index + numTilesWide - 1);
                adjacentList.Add(index + numTilesWide + 1);
            }

            foreach (int item in adjacentList) 
            {
                if (seedString[item] == 'B')
                {
                    sum += 1;
                }
            }

            return sum;
        }

        public override void Draw(SpriteBatch batch)
        {
            foreach (Tile tile in screenTiles)
            {
                if (tile.getTexture() != null)
                {
                    batch.Draw(
                    tile.getTexture(),
                    tile.getPosition(),
                    null,
                    Color.White,
                    0f,
                    new Vector2(tile.getTexture().Width / 2, tile.getTexture().Height / 2),
                    Vector2.One,
                    SpriteEffects.None,
                    0f);
                }
            }

        }

        public override void LoadContent()
        {
            foreach (Tile tile in screenTiles)
            {
                tile.LoadContent();
            };
        }

        private void buildFromString(string input) 
        {
            for (int i = 0; i < input.Length; i++)
            {
                Vector2 tilePos = getCoordsFromIndex(i);
                if (input[i] == 'A')
                {
                    screenTiles.Add(new Tile(game, "wall", (int)tilePos.X, (int)tilePos.Y, 1, gameHeight, gameWidth, numTilesHigh, numTilesWide));

                }
                else if (input[i] == 'B') 
                {
                    screenTiles.Add(new Tile(game, "grass", (int)tilePos.X, (int)tilePos.Y, 1, gameHeight, gameWidth, numTilesHigh, numTilesWide));
                }
            }
        }

        private Vector2 getCoordsFromIndex(int index)
        {
            int x = index % numTilesWide;
            int y = index / numTilesHigh;

            return new Vector2(x, y);
        }


        
    }
}
