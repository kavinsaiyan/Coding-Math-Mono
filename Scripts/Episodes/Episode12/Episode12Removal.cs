using CodingMath.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using System;
using System.Collections.Generic;

namespace CodingMath.Episodes
{
    public class Episode12Removal : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private int _width;
        private int _height;
        private List<Particle> _particles;

        public Episode12Removal()
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

            _particles = new List<Particle>();
            for (int i = 0; i < 100; i++)
            {
                _particles.Add(new Particle(Content));
                _particles[i].position = new Vector2(_width / 2, _height / 2);
                _particles[i].friction = 1f;
                _particles[i].Scale = new Vector2(0.4f, 0.4f);
                _particles[i].velocity.SetLength(CommonFunctions.RandomRange(16, 24));
                _particles[i].velocity.SetAngle(CommonFunctions.RandomRange(0, MathF.PI * 2));
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
            for (int i = 0; i < _particles.Count; i++)
            {
                _particles[i].Update((float)gameTime.ElapsedGameTime.TotalSeconds);

                if (_particles[i].position.X + _particles[i].Radius < 0 ||
                _particles[i].position.X - _particles[i].Radius > _width ||
                _particles[i].position.Y + _particles[i].Radius < 0 ||
                _particles[i].position.Y - _particles[i].Radius > _height)
                {
                    _particles.Remove(_particles[i]);
                }
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            for (int i = 0; i < _particles.Count; i++)
            {
                _particles[i].Draw(_spriteBatch);
            }
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}