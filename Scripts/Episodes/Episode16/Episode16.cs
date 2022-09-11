using CodingMath.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using System;
using System.Collections.Generic;

namespace CodingMath.Episodes
{
    public class Episode16 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Particle _particle;

        public const float k = 0.1f;
        private Vector2 _mousePos;

        public Episode16()
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
            _particle = new Particle(Content);
            _particle.friction = 0.95f;
            _particle.gravity = new Vector2(0, 0.1f);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // particle.acceleration = Input;
            _mousePos = Mouse.GetState().Position.ToVector2();
            //center correction
            Vector2 distane = _mousePos - _particle.position;
            //formula -> F = k * d;
            //  F = force
            //  k = spring constant
            //  d = distacne between the ends of the spring
            distane.SetLength(distane.GetLength() - 100);
            _particle.velocity += GameConstants.SPRING_CONSTANT * distane;
            _particle.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            _spriteBatch.DrawLine(_mousePos.X, _mousePos.Y, _particle.position.X,
                    _particle.position.Y, Color.Black, 2, 0);
            _particle.Draw(_spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}