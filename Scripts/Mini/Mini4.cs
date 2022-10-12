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
    public class Mini4 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D _circleTexture;
        private Vector2 _position;
        private Rectangle _rectangle;

        public Mini4()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
            _rectangle = new Rectangle();
            _rectangle.X = 100;
            _rectangle.Y = 100;
            _rectangle.Width = 400;
            _rectangle.Height = 200;
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _circleTexture = Content.Load<Texture2D>(GameConstants.CIRCLE_TEXTURE_PATH);
        }

        protected override void Update(GameTime gameTime)
        {
            //if (Input.IsPressedOnce(Keys.Space, Keyboard.GetState()))
            {
                _position = Mouse.GetState().Position.ToVector2();
                _position.X = Clamp(_position.X, _rectangle.X + 8, _rectangle.X + _rectangle.Width - 8);
                _position.Y = Clamp(_position.Y, _rectangle.Y + 8, _rectangle.Y + _rectangle.Height - 8);
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            _spriteBatch.DrawRectangle(_rectangle, Color.Gray, 1, 0);
            _spriteBatch.Draw(_circleTexture, _position, null, Color.Black, 0, GameConstants.circleOrigin, Vector2.One / 4, SpriteEffects.None, 0);
            _spriteBatch.End();
            base.Draw(gameTime);
        }

        private float Clamp(float val, float min, float max)
        {
            return MathF.Min(MathF.Max(val, min), max);
        }
    }
}