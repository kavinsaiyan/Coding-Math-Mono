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
    public class Mini8 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private int _screenWidth;
        private int _screenHeight;
        private Texture2D _circleTexture;
        private const float GRID_SIZE = 40f;
        private Vector2 _position;
        public Mini8()
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
                _position.X = RoundNearest(mousPos.X, GRID_SIZE);
                _position.Y = RoundNearest(mousPos.Y, GRID_SIZE);
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            for (float x = 0; x < _screenWidth; x += GRID_SIZE)
            {
                _spriteBatch.DrawLine(x, 0, x, _screenHeight, Color.Black, 1, 0);
            }
            for (float y = 0; y < _screenHeight; y += GRID_SIZE)
            {
                _spriteBatch.DrawLine(0, y, _screenWidth, y, Color.Black, 1, 0);
            }
            _spriteBatch.Draw(_circleTexture, _position, null, Color.Black, 0, GameConstants.circleOrigin, Vector2.One / 4, SpriteEffects.None, 0);
            _spriteBatch.End();
            base.Draw(gameTime);
        }

        float RoundToPlaces(float number, float places)
        {
            float mult = MathF.Pow(10, places);
            return MathF.Round(mult * number) / mult;
        }
        float RoundNearest(float number, float nearest)
        {
            return MathF.Round(number / nearest) * nearest;
        }
    }
}