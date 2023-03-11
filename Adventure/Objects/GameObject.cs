using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Adventure.Objects
{
    internal class GameObject
    {
        protected string textureName = "";
        protected Texture2D texture;
        protected Game game;
        protected Vector2 position;

        public GameObject(Game myGame)
        {
            game = myGame;
        }

        public virtual void LoadContent()
        {
            if (textureName != "")
            {
                texture = game.Content.Load<Texture2D>(textureName);
            }
            else
            {
                Console.WriteLine("ERROR: No Object Texture Found");
            }
        }

        public virtual void Update(KeyboardState kState, MouseState mState, GameTime gameTime, GraphicsDeviceManager graphics, List<Keys> controls)
        {
        }

        public virtual void Draw(SpriteBatch batch)
        {
            if (texture != null)
            {
                batch.Draw(
                texture,
                position,
                null,
                Color.White,
                0f,
                new Vector2(texture.Width / 2, texture.Height / 2),
                Vector2.One,
                SpriteEffects.None,
                0f);
            }
        }
    }
}
