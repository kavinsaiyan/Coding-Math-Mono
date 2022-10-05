using CodingMath.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using CodingMath.InputSystem;
namespace CodingMath.Episodes
{
    public class Episode52 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private long _seed = 14523789;
        private const int NUMBER_OF_DIGITS = 8;
        private const int POINT_SIZE = 20;
        private long _seedLCG = 1;
        private long a = 1103515245;
        private int c = 12345;
        private long m = 0;
        private int _xCoodinate = 0;
        private int _yCoordinate = 0;
        private int _width;
        private int _height;
        public Episode52()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            m = (long)MathF.Pow(2, 31);
        }

        protected override void Initialize()
        {
            base.Initialize();
            _width = GraphicsDevice.Viewport.Width;
            _height = GraphicsDevice.Viewport.Height;
            // m = MathF.Pow(2, 31);
            // Debug.Log(" m " + m);
            // Debug.Log(" m1 " + Math.Abs(1 << 31));
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            if (Input.IsPressedOnce(Keys.Space, Keyboard.GetState()))
            {
                Debug.Log(RandomRange(0, 100) + "");
                // Debug.Log(NextRand() + "");
                // Debug.Log(NextFloat() + "");
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            // GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            Color color = Color.Red;
            // float nextFloat = NextFloat();
            float nextFloat = NextFloatLCG();
            // Debug.Log("" + nextFloat);
            if (nextFloat > 0.5f)
                color = Color.Blue;

            // for (_xCoodinate = 0; _xCoodinate < _width; _xCoodinate++)
            // {
            if (_xCoodinate < _width && _yCoordinate < _height)
                _spriteBatch.DrawPoint(_xCoodinate, _yCoordinate, color, POINT_SIZE, 0);
            // }
            _xCoodinate += POINT_SIZE;
            if (_xCoodinate > _width)
            {
                _xCoodinate = 0;
                if (_yCoordinate < _height)
                    _yCoordinate += POINT_SIZE;
            }
            _spriteBatch.End();

            base.Draw(gameTime);
        }


        private long NextRand()
        { //square the numbers first 
            _seed = _seed * _seed;
            string n = _seed.ToString();
            while (n.Length < NUMBER_OF_DIGITS * 2)
                n = "0" + n;
            // use the middle four digits 
            n = n.Substring((int)MathF.Floor(NUMBER_OF_DIGITS / 2), NUMBER_OF_DIGITS);
            _seed = int.Parse(n);
            return _seed;

        }

        private float NextFloat()
        {
            return ((float)NextRand()) / MathF.Pow(10, NUMBER_OF_DIGITS);
        }


        private long NextRandLCG()
        {
            _seedLCG = ((a * _seedLCG + c)) % m;
            return _seedLCG;
        }

        private float NextFloatLCG()
        {
            float s = NextRandLCG();
            return (float)(s / m);
        }

        private float RandomRange(float min, float max)
        {
            return min + NextFloat() * (max - min);
        }
    }
}