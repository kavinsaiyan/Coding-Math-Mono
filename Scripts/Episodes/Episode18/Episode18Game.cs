using CodingMath.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using System;
using System.Collections.Generic;

namespace CodingMath.Episodes
{
    public class Episode18Game : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private int _width;
        private int _height;
        private Bar _oscillatingBar;

        public Episode18Game()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
            _width = GraphicsDevice.Viewport.Width;
            _height = GraphicsDevice.Viewport.Height;

            _oscillatingBar.positon = new Vector2(_width / 6, _height - _height / 3);
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _oscillatingBar = new Bar(Content.Load<Texture2D>(GameConstants.RECTANGLE_TEXTURE_PATH));
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _oscillatingBar.Value = MathF.Sin((float)gameTime.TotalGameTime.TotalSeconds * MathF.PI * 2) * 0.5f + 0.5f;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            _oscillatingBar.Draw(_spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        class Bar
        {
            public Vector2 positon;
            private Vector2 _scale;
            private Texture2D _rectangleTexture;
            private readonly Vector2 _scaleBG;
            private readonly Vector2 _offset;

            public float Value
            {
                get => _scale.X;
                set
                {
                    _scale.Y = Math.Clamp(value, 0, 1);
                }
            }

            public Bar(Texture2D rectangle)
            {
                _rectangleTexture = rectangle;
                _scale = new Vector2(0.5f, 1);
                _scaleBG = new Vector2(0.5f, 1);
                _offset = new Vector2(_rectangleTexture.Width, _rectangleTexture.Height) * _scaleBG;
            }
            public void Draw(SpriteBatch spriteBatch)
            {
                spriteBatch.Draw(_rectangleTexture, positon, null, Color.Black, 0, Vector2.One, _scaleBG, SpriteEffects.None, 0);
                spriteBatch.Draw(_rectangleTexture, positon + _offset, null, Color.White, MathF.PI, Vector2.One, _scale, SpriteEffects.None, 0);
            }
        }
    }
}