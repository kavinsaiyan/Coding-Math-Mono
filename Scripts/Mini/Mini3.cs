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
    public class Mini3 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private int _screenWidth;
        private int _screenHeight;
        private Vector2 _scale;
        private Texture2D _circleTexture;
        public Mini3()
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
        }

        protected override void Update(GameTime gameTime)
        {
            //if (Input.IsPressedOnce(Keys.Space, Keyboard.GetState()))
            {
                Point mousPos = Mouse.GetState().Position;
                float x = mousPos.X - _screenWidth / 2;
                float y = mousPos.Y - _screenHeight / 2;
                float distance = MathF.Sqrt(x * x + y * y);
                _scale.X = Remap(distance, 0, 400, 0.5f, 4f);
                _scale.Y = Remap(distance, 0, 400, 0.5f, 4f);
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(transformMatrix: Matrix.CreateTranslation(_screenWidth / 2, _screenHeight / 2, 0));
            _spriteBatch.Draw(_circleTexture, Vector2.Zero, null, Color.Black, 0, GameConstants.circleOrigin, _scale, SpriteEffects.None, 0);
            _spriteBatch.End();
            base.Draw(gameTime);
        }

        public float Remap(float value, float low1, float high1, float low2, float high2)
        {
            return low2 + (value - low1) * (high2 - low2) / (high1 - low1);
        }
    }
}