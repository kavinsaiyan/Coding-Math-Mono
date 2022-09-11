using CodingMath.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using System;
using System.Collections.Generic;

namespace CodingMath.Episodes
{
    public class Episode14RectPoint : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Color _color;
        private RectangleF _rectangle;
        private Vector2 _point;

        public Episode14RectPoint()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();

            _rectangle = new Rectangle(100, 100, 50, 50);
            _point = new Vector2(200, 200);
            _color = Color.Black;
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            Vector2 pos = Mouse.GetState().Position.ToVector2();
            _rectangle.X = pos.X;
            _rectangle.Y = pos.Y;
            bool xIsInRange = _point.X >= _rectangle.X && _point.X <= (_rectangle.X + _rectangle.Width);
            bool yIsInRange = _point.Y >= _rectangle.Y && _point.Y <= (_rectangle.Y + _rectangle.Height);
            if (xIsInRange && yIsInRange)
            {
                _color = Color.Red;
            }
            else
            {
                _color = Color.Black;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            _spriteBatch.DrawRectangle(_rectangle, _color, 1, 0);
            _spriteBatch.DrawPoint(_point, _color, 3, 0);

            _spriteBatch.End();

            base.Draw(gameTime);
        }

    }
}