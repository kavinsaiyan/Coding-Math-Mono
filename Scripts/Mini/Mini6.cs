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
    public class Mini6 : Game
    {
        private const int SCALE_FACTOR = 4;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private int _screenWidth;
        private int _screenHeight;
        private Texture2D _circleTexture;
        private Color _color;

        public Mini6()
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
                if (distance < (_circleTexture.Width / 2) * SCALE_FACTOR)
                    _color = Color.Gray;
                else
                    _color = Color.Red;
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(transformMatrix: Matrix.CreateTranslation(_screenWidth / 2, _screenHeight / 2, 0));
            _spriteBatch.Draw(_circleTexture, Vector2.Zero, null, _color, 0, GameConstants.circleOrigin, Vector2.One * SCALE_FACTOR, SpriteEffects.None, 0);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}