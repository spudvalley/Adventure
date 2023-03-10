using Adventure.Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Adventure
{
    public class Game1 : Game
    {
        // controls
        private Keys upInput;
        private Keys downInput;
        private Keys leftInput;
        private Keys rightInput;
        private Keys attackInput;
        private Keys interactInput;


        private Keys quitInput;

        // graphics
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        // game objects
        List<GameObject> objectList;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 800;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // initialize controls 
            // TODO: Make these able to be set by user
            upInput = Keys.Up;
            downInput = Keys.Down;
            leftInput = Keys.Left;
            rightInput = Keys.Right;
            attackInput = Keys.Space;
            interactInput = Keys.E;
            
            quitInput = Keys.Escape;
            List<Keys> controls = new List<Keys> 
            {
                upInput, 
                downInput, 
                leftInput, 
                rightInput, 
                attackInput, 
                interactInput
            };

            // object constants
            string playerTextureString = "player";
            Vector2 playerPosition = new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2);
            float playerSpeed = 150f;

            // create objects
            objectList = new List<GameObject>
            {
                // new Tile(this, "wall", 1, 0, 1, _graphics.PreferredBackBufferHeight, _graphics.PreferredBackBufferWidth, 10, 10),
                // new Tile(this, "grass", 0, 0, 1, _graphics.PreferredBackBufferHeight, _graphics.PreferredBackBufferWidth, 10, 10),
                new Screen(this, _graphics.PreferredBackBufferHeight, _graphics.PreferredBackBufferWidth, 10, 10, 0, 0, ""),
                new Player(this, playerTextureString, playerPosition, playerSpeed, controls)
            };

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            foreach (GameObject gameObject in objectList)
            {
                gameObject.LoadContent();
            }
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(quitInput))
                Exit();

            KeyboardState kState = Keyboard.GetState();
            MouseState mState = Mouse.GetState();

            // update game objects
            foreach (GameObject gameObject in objectList)
            {
                gameObject.Update(kState, mState, gameTime, _graphics);
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            foreach (GameObject gameObject in objectList)
            {
                gameObject.Draw(_spriteBatch);
            }
            base.Draw(gameTime);
            _spriteBatch.End();
        }

        
    }
}