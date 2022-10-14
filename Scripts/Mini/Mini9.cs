using CodingMath.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using System;
using System.Linq;
using System.Collections.Generic;
using CodingMath.InputSystem;
namespace CodingMath.Mini
{
    public class Mini9 : Game
    {
        private const int NO_OF_POINTS = 100000;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private int _screenWidth;
        private int _screenHeight;
        private Vector2[] _points;
        public Mini9()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
            _screenWidth = GraphicsDevice.Viewport.Width;
            _screenHeight = GraphicsDevice.Viewport.Height;
            _points = new Vector2[NO_OF_POINTS];
            for (int i = 0; i < NO_OF_POINTS; i++)
            {
                _points[i] = new Vector2();
                _points[i].X = RandomDistributedRange(0, _screenWidth);
                _points[i].Y = RandomDistributedRange(0, _screenHeight);
            }
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            //if (Input.IsPressedOnce(Keys.Space, Keyboard.GetState()))
            {

            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            for (int i = 0; i < NO_OF_POINTS; i++)
            {
                _spriteBatch.DrawPoint(_points[i], Color.Black, 1, 0);
            }
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        private float RandomDistributedRange(float min, float max, int iteration = 5)
        {
            float total = 0;
            for (int i = 0; i < iteration; i++)
                total += CommonFunctions.RandomRange(min, max);
            return total / iteration;
        }
    }
}