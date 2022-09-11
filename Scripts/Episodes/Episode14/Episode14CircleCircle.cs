using CodingMath.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using System;
using System.Collections.Generic;

namespace CodingMath.Episodes
{
    public class Episode14CircleCircle : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Circle _circle1;
        private Circle _circle2;

        public Episode14CircleCircle()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();

            _circle1 = new Circle();
            _circle1.position = new Vector2(100, 100);
            _circle1.radius = 60f;
            _circle1.color = Color.Black;

            _circle2 = new Circle();
            _circle2.position = new Vector2(100, 100);
            _circle2.radius = 100f;
            _circle2.color = Color.Black;
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _circle2.position = Mouse.GetState().Position.ToVector2();

            Vector2 direction = _circle1.position - _circle2.position;
            if (direction.GetLength() < _circle1.radius + _circle2.radius)
            {
                _circle1.color = Color.Red;
                _circle2.color = Color.Red;
            }
            else
            {
                _circle1.color = Color.Black;
                _circle2.color = Color.Black;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            _spriteBatch.DrawCircle(_circle1.position, _circle1.radius, 32, _circle1.color, 1, 0);
            _spriteBatch.DrawCircle(_circle2.position, _circle2.radius, 32, _circle2.color, 1, 0);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}