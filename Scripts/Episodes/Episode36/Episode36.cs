using CodingMath.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using CodingMath.InputSystem;
namespace CodingMath.Episodes
{
    public class Episode36 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Texture2D _circleTexture;

        Vector2 _particlePosition;
        Vector2 _particleOldPosition;

        private const float FRICTION = 0.999f;
        private const float BOUNCINESS = 0.90f;
        private const float GRAVITY = 0.5f;

        private int _screenWidth;
        private int _screenHeight;

        public Episode36()
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
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _circleTexture = Content.Load<Texture2D>(GameConstants.CIRCLE_TEXTURE_PATH);
            _particlePosition = new Vector2(100, 100);
            _particleOldPosition = new Vector2(-2f, -2f) + _particlePosition;
        }
        protected override void Update(GameTime gameTime)
        {
            // if (Input.IsPressedOnce(Keys.Space, Keyboard.GetState()))
            {
                Vector2 velocity = new Vector2();
                velocity.X = (_particlePosition.X - _particleOldPosition.X) * FRICTION;
                velocity.Y = (_particlePosition.Y - _particleOldPosition.Y) * FRICTION;

                _particleOldPosition = _particlePosition;
                _particlePosition += velocity;
                _particlePosition.Y += GRAVITY;


                //bounds check
                if (_particlePosition.X > _screenWidth)
                {
                    _particlePosition.X = _screenWidth;
                    _particleOldPosition.X = _particlePosition.X + velocity.X * BOUNCINESS;
                }
                else if (_particlePosition.X < 0)
                {
                    _particlePosition.X = 0;
                    _particleOldPosition.X = _particlePosition.X + velocity.X * BOUNCINESS;
                }

                if (_particlePosition.Y > _screenHeight)
                {
                    _particlePosition.Y = _screenHeight;
                    _particleOldPosition.Y = _particlePosition.Y + velocity.Y * BOUNCINESS;
                }
                else if (_particlePosition.Y < 0)
                {
                    _particlePosition.Y = 0;
                    _particleOldPosition.Y = _particlePosition.Y + velocity.Y * BOUNCINESS;
                }
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            _spriteBatch.Draw(_circleTexture, _particlePosition, null, Color.Black, 0, GameConstants.circleOrigin, Vector2.One, SpriteEffects.None, 0);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}