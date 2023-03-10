using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Adventure.Objects
{
    internal class Tile : GameObject
    {
        int collision;
        int xIndex;
        int yIndex;
        int gameHeight;
        int gameWidth;
        int numTilesHigh;
        int numTilesWide;

        public Tile(Game myGame, string textureString, int xIndex, int yIndex, int collision, int gameHeight, int gameWidth, int numTilesHigh, int numTilesWide)
            : base(myGame)
        {
            this.textureName = textureString;
            this.xIndex = xIndex;
            this.yIndex = yIndex;
            this.collision = collision;
            this.gameHeight = gameHeight;
            this.gameWidth = gameWidth;
            this.numTilesHigh = numTilesHigh;
            this.numTilesWide = numTilesWide;
            this.position = positionFromIndex(xIndex, yIndex);
        }

        private Vector2 positionFromIndex(int x,int y)
        {
            float xPos = (x * (gameWidth / numTilesWide)) + ((gameWidth / numTilesWide) / 2);
            float yPos = (y * (gameHeight / numTilesHigh)) + ((gameHeight / numTilesHigh) / 2);

            return new Vector2(xPos,yPos);
        }

        public Texture2D getTexture() { return texture; }

        public Vector2 getPosition() { return position; }
        public int getXIndex() { return xIndex; } 
        public int getYIndex() {  return yIndex; }
        public int getCollision() { return collision; }

    }
}
