using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using System;

namespace CodingMath.Episodes
{
    public class Episode2 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private int _width;
        private int _height;

        private Vector2[] lineStartPoints;
        private Vector2[] lineEndPoints;

        const int NUMBER_OF_LINES = 100;

        public Episode2()
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

            _spriteBatch.Begin(transformMatrix: Matrix.CreateTranslation(new Vector3(0, GraphicsDevice.Viewport.Height / 2, 0)));
            for (float angle = 0; angle < MathF.PI * 12.8f; angle += 0.01f)
            {
                float x = angle * 20;
                float y = MathF.Sin(angle) * 20;

                _spriteBatch.DrawPoint(x, y, Color.Black, 2);

                y = MathF.Cos(angle) * 20;
                _spriteBatch.DrawPoint(x, y, Color.Red, 2);
            }
            _spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
