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
    public class Mini1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private float[] _values;
        private int _screenWidth;
        private int _screenHeight;
        private float _min;
        private float _max;

        public Mini1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
            _values = new float[] { 7, 5, 21, 18, 33, 12, 27, 18, 9, 23, 14, 6, 31, 25, 17, 13, 29 };
            _screenWidth = GraphicsDevice.Viewport.Width;
            _screenHeight = GraphicsDevice.Viewport.Height;
            _min = _values.OrderBy(x => x).First();
            _max = _values.OrderBy(x => x).Last();
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
            float xInrement = _screenWidth / _values.Length;
            for (int i = 0; i < _values.Length - 1; i++)
            {
                float x1 = i * xInrement;
                float y1 = _screenHeight - _screenHeight * Normalize(_min, _max, _values[i]);
                float x2 = (i + 1) * xInrement;
                float y2 = _screenHeight - _screenHeight * Normalize(_min, _max, _values[i + 1]);

                _spriteBatch.DrawLine(x1, y1, x2, y2, Color.Black, 1, 0);
            }
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        //can also be said as Inverselerp
        private float Normalize(float min, float max, float val)
        {
            float res = (val - min) / (max - min);
            Debug.Log($"min:{min} msx:{max} val:{val} res :{res}");
            return res;
        }
    }
}