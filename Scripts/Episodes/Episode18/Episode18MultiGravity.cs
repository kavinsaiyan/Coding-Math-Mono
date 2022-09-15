using CodingMath.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using System;
using System.Collections.Generic;

namespace CodingMath.Episodes
{
    public class Episode18MultiGravity : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private int _width;
        private int _height;
        private Particle _sun1;
        private Particle _sun2;
        private Particle[] _particles;
        private const int NO_OF_PARTICLES = 100;
        private Vector2 _emitter;

        public Episode18MultiGravity()
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
            _emitter = new Vector2(0, 0);

            _sun1 = new Particle(Content);
            _sun1.position = new Vector2(250, 100);
            _sun1.mass = 10000;
            _sun2 = new Particle(Content);
            _sun2.position = new Vector2(400, 500);
            _sun2.SetColor(Color.Yellow);
            _sun2.mass = 20000;

            _particles = new Particle[NO_OF_PARTICLES];
            for (int i = 0; i < NO_OF_PARTICLES; i++)
            {
                _particles[i] = new Particle(Content);
                _particles[i].position = _emitter;
                _particles[i].velocity.SetLength(CommonFunctions.RandomRange(7, 8));
                _particles[i].velocity.SetAngle(CommonFunctions.RandomRange(MathF.PI / 4, MathF.PI / 4 + MathF.PI / 2));
                _particles[i].gravity = new Vector2(0, 0);
                _particles[i].Scale = new Vector2(0.4f, 0.4f);
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

            _sun1.Update();

            // if (Input.IsPressedOnce(Keys.Space, Keyboard.GetState()))
            {
                _sun1.Update();
                _sun2.Update();

                for (int i = 0; i < NO_OF_PARTICLES; i++)
                {
                    _particles[i].GravitateTo(_sun1);
                    _particles[i].GravitateTo(_sun2);
                    _particles[i].Update();
                    if (_particles[i].position.X > _width || _particles[i].position.X < 0 ||
                        _particles[i].position.Y > _height || _particles[i].position.Y < 0)
                    {
                        _particles[i].position = _emitter;
                        _particles[i].velocity.SetLength(CommonFunctions.RandomRange(7, 8));
                        _particles[i].velocity.SetAngle(CommonFunctions.RandomRange(MathF.PI / 4, MathF.PI / 4 + MathF.PI / 2));
                    }
                }
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            _sun1.Draw(_spriteBatch);
            _sun2.Draw(_spriteBatch);
            for (int i = 0; i < NO_OF_PARTICLES; i++)
            {
                _particles[i].Draw(_spriteBatch);
            }
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}