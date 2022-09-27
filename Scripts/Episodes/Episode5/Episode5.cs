using CodingMath.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using System;
using System.Collections.Generic;

namespace CodingMath.Episodes
{
    public class Episode5 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private int _width;
        private int _height;

        private Matrix _translationMatrix;
        private Matrix _rotationMatrix;

        private LineEpisode5[] _lines;
        private Vector2 _position;

        public Episode5()
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

            _translationMatrix = Matrix.CreateTranslation(_width / 2, _height / 2, 0);
            _lines = new LineEpisode5[3];
            _lines[0] = new LineEpisode5(new Vector2(0, 0), new Vector2(36, 0)); //horizontal stright line of the arrow
            _lines[1] = new LineEpisode5(_lines[0].end, new Vector2(28, 8));
            _lines[2] = new LineEpisode5(_lines[0].end, new Vector2(28, -8));

            _position = new Vector2();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _position.X = 100 * MathF.Sin((float)gameTime.TotalGameTime.TotalSeconds);
            _position.Y = 100 * MathF.Cos((float)gameTime.TotalGameTime.TotalSeconds);

            MouseState mouseState = Mouse.GetState();
            Vector2 direction = mouseState.Position.ToVector2() - Vector2.Transform(_position, _translationMatrix);
            _rotationMatrix = Matrix.CreateRotationZ(MathF.Atan2(direction.Y, direction.X));

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(transformMatrix: _translationMatrix);
            for (int i = 0; i < _lines.Length; i++)
            {
                Vector2 tempStart = Vector2.Transform(_lines[i].start, _rotationMatrix);
                Vector2 tempEnd = Vector2.Transform(_lines[i].end, _rotationMatrix);
                _spriteBatch.DrawLine(_position + tempStart, _position + tempEnd, Color.Black);
            }
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}