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
    public class Mini10 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private int _screenWidth;
        private int _screenHeight;
        private Particle[] _particles;
        private const int NO_OF_PARTICLES = 100;
        public Mini10()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
            _screenWidth = GraphicsDevice.Viewport.Width;
            _screenHeight = GraphicsDevice.Viewport.Height;
            _particles = new Particle[NO_OF_PARTICLES];
            for (int i = 0; i < NO_OF_PARTICLES; i++)
            {
                _particles[i] = new Particle(Content);
                _particles[i].Scale = new Vector2(0.1f, 0.1f);
                _particles[i].position = new Vector2(_screenWidth / 2, _screenHeight / 2);
                _particles[i].velocity.SetLength(CommonFunctions.RandomRange(10, 40));
                _particles[i].velocity.SetAngle(CommonFunctions.RandomRange(0, 2 * MathF.PI));
                _particles[i].friction = 0.99f;
            }
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

        }

        protected override void Update(GameTime gameTime)
        {
            //if (Input.IsPressedOnce(Keys.Space, Keyboard.GetState()))
            {
                for (int i = 0; i < NO_OF_PARTICLES; i++)
                {
                    _particles[i].Update();
                }
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            for (int i = 0; i < NO_OF_PARTICLES; i++)
            {
                _particles[i].Draw(_spriteBatch);
            }
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}