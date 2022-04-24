using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;

namespace CodingMath.Episodes
{
    public class Episode1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private int _width;
        private int _height;

        private Vector2[] lineStartPoints;
        private Vector2[] lineEndPoints;

        const int NUMBER_OF_LINES = 100;

        public Episode1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
            _width = GraphicsDevice.Viewport.Width;
            _height = GraphicsDevice.Viewport.Height;

            lineStartPoints = new Vector2[NUMBER_OF_LINES];
            lineEndPoints = new Vector2[NUMBER_OF_LINES];
            System.Random rnd = new System.Random();
            for (int i = 0; i < 100; i++)
            {
                lineStartPoints[i] = new Vector2((int)(rnd.NextDouble() * _width), (int)(rnd.NextDouble() * _height));
                lineEndPoints[i] = new Vector2((int)(rnd.NextDouble() * _width), (int)(rnd.NextDouble() * _height));
            }
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            for (int i = 0; i < NUMBER_OF_LINES; i++)
            {
                _spriteBatch.DrawLine(lineStartPoints[i], lineEndPoints[i], Color.Black);
            }
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
