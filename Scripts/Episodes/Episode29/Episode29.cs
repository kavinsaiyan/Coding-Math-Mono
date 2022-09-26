using CodingMath.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using System;
using System.Collections.Generic;

namespace CodingMath.Episodes
{
    public class Episode29 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Texture2D _circleTexture;
        private Vector2 _position;
        private float _ease = 0.1f;

        private bool _shouldDraw = true;

        public Episode29()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _circleTexture = Content.Load<Texture2D>(GameConstants.CIRCLE_TEXTURE_PATH);
        }

        protected override void Update(GameTime gameTime)
        {
            // if (Input.IsPressedOnce(Keys.Space, Keyboard.GetState()))
            {
                _position = EaseTo(Mouse.GetState().Position.ToVector2(), _position, _ease, ref _shouldDraw);
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            if (_shouldDraw)
            {
                _spriteBatch.Begin();
                if (_shouldDraw)
                    _spriteBatch.Draw(_circleTexture, _position, null, Color.Black, 0,
                        GameConstants.circleOrigin, Vector2.One, SpriteEffects.None, 0);
                _spriteBatch.End();
            }

            base.Draw(gameTime);
        }

        Vector2 EaseTo(Vector2 target, Vector2 currentPosition, float ease, ref bool shouldDraw)
        {
            Vector2 direction = (target - currentPosition);
            currentPosition = currentPosition + direction * ease;

            if (MathF.Abs(direction.X) > 0.1f || MathF.Abs(direction.Y) > 0.1f)
                shouldDraw = true;
            else
                shouldDraw = false;

            return currentPosition;
        }
    }
}