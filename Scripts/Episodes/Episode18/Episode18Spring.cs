using CodingMath.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using System;
using System.Collections.Generic;

namespace CodingMath.Episodes
{
    public class Episode18Spring : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private int _width;
        private int _height;
        private ParticleOptimized _weight;
        private Vector2 _mousePos;
        private Vector2 _screenCenter;

        public Episode18Spring()
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

            _weight = new ParticleOptimized(Content);
            _weight.positionX = 250;
            _weight.positionY = 100;
            _weight.friction = 0.95f;

            _screenCenter = new Vector2(_width / 2, _height / 2);
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _mousePos = Mouse.GetState().Position.ToVector2();
            _weight.SpringTo(_mousePos, 100);
            _weight.SpringTo(_screenCenter, 100);
            _weight.Update();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            _weight.Draw(_spriteBatch);
            _spriteBatch.DrawLine(_weight.positionX, _weight.positionY, _mousePos.X, _mousePos.Y, Color.Black, 1, 0);
            _spriteBatch.DrawLine(_weight.positionX, _weight.positionY, _screenCenter.X, _screenCenter.Y, Color.Black, 1, 0);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}