using CodingMath.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using System;
using System.Collections.Generic;

namespace CodingMath.Episodes
{
    public class Episode14RectRect : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Color _color;
        private RectangleF _rectangle1;
        private RectangleF _rectangle2;

        public Episode14RectRect()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();

            _rectangle1 = new Rectangle(100, 100, 50, 50);
            _rectangle2 = new Rectangle(200, 200, 50, 100);
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
            _rectangle1.X = pos.X;
            _rectangle1.Y = pos.Y;

            float dx1 = (_rectangle1.X + _rectangle1.Width) - _rectangle2.X;
            float dx2 = (_rectangle2.X + _rectangle2.Width) - _rectangle1.X;
            float dy1 = (_rectangle1.Y + _rectangle1.Height) - _rectangle2.Y;
            float dy2 = (_rectangle2.Y + _rectangle2.Height) - _rectangle1.Y;

            // Debug.Log($"dx1 : {dx1} dx2 : {dx2} dy1 : {dy1} dy2 : {dy2}");

            if (dx1 > 0 && dx2 > 0 && dy1 > 0 && dy2 > 0)
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

            _spriteBatch.DrawRectangle(_rectangle1, _color, 1, 0);
            _spriteBatch.DrawRectangle(_rectangle2, _color, 1, 0);

            _spriteBatch.End();

            base.Draw(gameTime);
        }

    }
}