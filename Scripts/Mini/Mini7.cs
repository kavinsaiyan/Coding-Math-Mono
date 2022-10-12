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
    public class Mini7 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private int _screenWidth;
        private int _screenHeight;
        private Texture2D _circleTexture;
        private float _angle;
        private Rectangle _rectangle;

        public Mini7()
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

            _rectangle = new Rectangle(0, -10, 80, 20);
            _angle = DegToRadians(45);
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _circleTexture = Content.Load<Texture2D>(GameConstants.CIRCLE_TEXTURE_PATH);
        }


        protected override void Update(GameTime gameTime)
        {
            KeyboardState state = Keyboard.GetState();
            if (state.IsKeyDown(Keys.Space))
            {
                _angle = DegToRadians(RadiansToDeg(_angle) + 1f);
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(transformMatrix: Matrix.CreateRotationZ(_angle) * Matrix.CreateTranslation(_screenWidth / 2, _screenHeight / 2, 0));
            _spriteBatch.Draw(_circleTexture, Vector2.Zero, null, Color.Black, 0, GameConstants.circleOrigin, Vector2.One, SpriteEffects.None, 0);
            _spriteBatch.DrawRectangle(_rectangle, Color.Black, 2, 0);
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        private float DegToRadians(float deg)
        {
            return (MathF.PI / 180) * deg;
        }

        private float RadiansToDeg(float rad)
        {
            return (180 / MathF.PI) * rad;
        }
    }
}