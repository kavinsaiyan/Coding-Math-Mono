using CodingMath.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using System;
using System.Collections.Generic;

namespace CodingMath.Episodes
{
    public class Episode12BounceDVD : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private int _width;
        private int _height;
        private Particle _particle;

        private const float BOUNCE = -1f;

        public Episode12BounceDVD()
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

            _particle = new Particle(Content, GameConstants.DVD_LOGO_TEXTURE_PATH);
            _particle.Scale = new Vector2(0.4f, 0.4f);
            _particle.friction = 1f;
            _particle.position = new Vector2(_width / 2, _height / 2);
            _particle.velocity.SetLength(4f);
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

            float halfWidth = (_particle.Size.X / 2f) * _particle.Scale.X;
            float halfHeight = (_particle.Size.Y / 2f) * _particle.Scale.Y;
            if (_particle.position.X + halfWidth > _width)
            {
                // _particle.position.X = _width - halfWidth;
                _particle.velocity.X *= BOUNCE;
            }
            if (_particle.position.X - halfWidth < 0)
            {
                // _particle.position.X = halfWidth;
                _particle.velocity.X *= BOUNCE;
            }
            if (_particle.position.Y + halfHeight / 2 > _height)
            {
                // _particle.position.Y = _height - halfHeight;
                _particle.velocity.Y *= BOUNCE;
            }
            if (_particle.position.Y - halfHeight < 0)
            {
                // _particle.position.Y = halfHeight;
                _particle.velocity.Y *= BOUNCE;
            }
            _particle.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            _particle.DrawWithOrigin(_spriteBatch, new Vector2(_particle.TextureWidth / 2, _particle.TextureHeight / 2));
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}