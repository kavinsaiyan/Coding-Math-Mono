using CodingMath.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using System;
using System.Collections.Generic;

namespace CodingMath.Episodes
{
    public class Episode4Sub3 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private int _width;
        private int _height;
        private SpriteFont _arial;
        private List<DrawableString> zeroOneStrings;

        public Episode4Sub3()
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
            zeroOneStrings = new List<DrawableString>();
            for (int y = 0; y < _height; y += 11)
            {
                for (int x = 0; x < _width; x += 10)
                {
                    zeroOneStrings.Add(new DrawableString()
                    {
                        position = new Vector2(x, y),
                        data = CommonFunctions.RandomRange(0, 1) < 0.5f ? "0" : "1"
                    });
                }
            }
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _arial = Content.Load<SpriteFont>(GameConstants.FONT_PATH);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();

            for (int i = 0; i < zeroOneStrings.Count; i++)
            {
                _spriteBatch.DrawString(_arial, zeroOneStrings[i].data, zeroOneStrings[i].position, Color.Green);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        public struct DrawableString
        {
            public string data;
            public Vector2 position;
        }
    }
}