using CodingMath.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using System;
using System.Collections.Generic;

namespace CodingMath.Episodes
{
    public class Episode12Wrapping : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private int _width;
        private int _height;
        private Particle _particle;

        public Episode12Wrapping()
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

            _particle = new Particle(Content);
            _particle.position = new Vector2(_width / 2, _height / 2);
            _particle.velocity.SetLength(8f);
            _particle.velocity.SetAngle(CommonFunctions.RandomRange(0, MathF.PI * 2));
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (_particle.position.X - _particle.Radius > _width)
                _particle.position.X = -_particle.Radius;
            if (_particle.position.X + _particle.Radius < 0)
                _particle.position.X = _width + _particle.Radius;
            if (_particle.position.Y - _particle.Radius > _width)
                _particle.position.Y = -_particle.Radius;
            if (_particle.position.Y + _particle.Radius < 0)
                _particle.position.Y = _height + _particle.Radius;
            _particle.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            _particle.Draw(_spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}