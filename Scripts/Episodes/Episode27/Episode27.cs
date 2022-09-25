using CodingMath.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using System;
using System.Collections.Generic;

namespace CodingMath.Episodes
{
    public class Episode27 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D _circleTexture;
        private Point _target;
        private Vector2 _position;

        private const float EASING = 0.1f;

        public Episode27()
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
            _target = Mouse.GetState().Position;

            float diffX = _target.X - _position.X;
            float diffY = _target.Y - _position.Y;

            _position.X += diffX * EASING;
            _position.Y += diffY * EASING;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            _spriteBatch.Draw(_circleTexture, _position, null, Color.Black, 0, GameConstants.circleOrigin, Vector2.One, SpriteEffects.None, 0);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}