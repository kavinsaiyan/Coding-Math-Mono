using CodingMath.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using System;
using System.Collections.Generic;

namespace CodingMath.Episodes
{
    public class Episode17SpringOptimized : Game
    {
        private const int SPRING_LENGTH = 100;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private ParticleOptimized _particle;
        private Vector2 _mousePos;

        public Episode17SpringOptimized()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();

            for (int i = 0; i < Components.Count; i++)
                Components[i].Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            _particle = new ParticleOptimized(Content);
            _particle.friction = 0.95f;
            _particle.gravity = 0.1f;
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // particle.acceleration = Input;
            _mousePos = Mouse.GetState().Position.ToVector2();
            //formula -> F = k * d;
            //  F = force
            //  k = spring constant
            //  d = distacne between the ends of the spring
            float dx = _mousePos.X - _particle.positionX,
                    dy = _mousePos.Y - _particle.positionY,
                    distance = System.MathF.Sqrt(dx * dx + dy * dy),
                    springForce = (distance - SPRING_LENGTH) * GameConstants.SPRING_CONSTANT,
                    ax = dx / (distance * springForce),
                    ay = dy / (distance * springForce);

            _particle.velocityX += ax;
            _particle.velocityY += ay;
            Debug.Log("" + ax);
            Debug.LogWarning("" + ay);

            _particle.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            _spriteBatch.DrawLine(_mousePos.X, _mousePos.Y, _particle.positionX,
                    _particle.positionY, Color.Black, 2, 0);
            _particle.Draw(_spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}