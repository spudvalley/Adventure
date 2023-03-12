using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

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
        float rotation;
        Random rand;

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
            this.rand = new Random();

            if (this.textureName == "grass")
            {
                int randomRotation = rand.Next(0, 5);
                this.rotation = (float)((randomRotation * 90) * (Math.PI / 180));
            }
            
        }

        private Vector2 positionFromIndex(int x,int y)
        {
            float xPos = (x * (gameWidth / numTilesWide)) + ((gameWidth / numTilesWide) / 2);
            float yPos = (y * (gameHeight / numTilesHigh)) + ((gameHeight / numTilesHigh) / 2);

            return new Vector2(xPos,yPos);
        }

        public override void Draw(SpriteBatch batch)
        {

            if (texture != null)
            {
                batch.Draw(
                texture,
                position,
                null,
                Color.White,
                rotation,
                new Vector2(texture.Width / 2, texture.Height / 2),
                Vector2.One,
                SpriteEffects.None,
                0f);
            }
        }

        public Texture2D getTexture() { return texture; }

        public Vector2 getPosition() { return position; }
        public int getXIndex() { return xIndex; } 
        public int getYIndex() {  return yIndex; }
        public int getCollision() { return collision; }

    }
}
