using CodingMath.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using System;
using System.Collections.Generic;

namespace CodingMath.Episodes
{
    public class Episode9 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private const int NO_OF_PARTICLES = 80;
        private Particle[] _particles;
        private int _width;
        private int _height;

        public Episode9()
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

            _particles = new Particle[NO_OF_PARTICLES];
            for (int i = 0; i < NO_OF_PARTICLES; i++)
            {
                _particles[i] = new Particle(Content);
                _particles[i].Scale = new Vector2(0.1f, 0.1f);
                _particles[i].velocity.SetLength(CommonFunctions.RandomRange(1, 4) * 4);
                _particles[i].velocity.SetAngle(CommonFunctions.RandomRange(0, MathF.PI * 2));
                _particles[i].gravity = new Vector2(0, 0.1f);
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
            for (int i = 0; i < NO_OF_PARTICLES; i++)
            {
                _particles[i].Update((float)gameTime.ElapsedGameTime.TotalSeconds);
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(transformMatrix: Matrix.CreateTranslation(_width / 2, _height / 4, 0));
            for (int i = 0; i < NO_OF_PARTICLES; i++)
            {
                _particles[i].Draw(_spriteBatch);
            }
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}