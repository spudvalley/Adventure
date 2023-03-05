using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Adventure.Objects
{
    internal class Player : GameObject
    {
        float playerSpeed;

        // controls
        protected Keys upInput;
        protected Keys downInput;
        protected Keys leftInput;
        protected Keys rightInput;
        protected Keys attackInput;
        protected Keys interactInput;

        public Player(Game myGame, string playerTextureString, Vector2 playerPosition, float playerSpeed, List<Keys> controls)
            : base(myGame)
        {
            this.textureName = playerTextureString;
            this.position = playerPosition;
            this.playerSpeed = playerSpeed;

            // controls
            this.upInput = controls[0];
            this.downInput = controls[1];
            this.leftInput = controls[2];
            this.rightInput = controls[3];
            this.attackInput = controls[4];
            this.interactInput = controls[5];

        }

        
        public override void Update(KeyboardState kState, MouseState mState, GameTime gameTime, GraphicsDeviceManager graphics)
        {
            if (kState.IsKeyDown(upInput))
            {
                position.Y -= playerSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (kState.IsKeyDown(downInput))
            {
                position.Y += playerSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (kState.IsKeyDown(leftInput))
            {
                position.X -= playerSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (kState.IsKeyDown(rightInput))
            {
                position.X += playerSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (position.X >  graphics.PreferredBackBufferWidth - texture.Width / 2)
            {
                position.X = graphics.PreferredBackBufferWidth - texture.Width / 2;
            }
            else if (position.X < texture.Width / 2)
            {
                position.X = texture.Width / 2;
            }

            if (position.Y > graphics.PreferredBackBufferHeight - texture.Height / 2)
            {
                position.Y = graphics.PreferredBackBufferHeight - texture.Height / 2;
            }
            else if (position.Y < texture.Height / 2)
            {
                position.Y = texture.Height / 2;
            }
        }

        public void Render(GameTime gameTime)
        {

        } 

        public void setControls(List<Keys> controls)
        {
            // controls
            this.upInput = controls[0];
            this.downInput = controls[1];
            this.leftInput = controls[2];
            this.rightInput = controls[3];
            this.attackInput = controls[4];
            this.interactInput = controls[5];
        }
    }
}
