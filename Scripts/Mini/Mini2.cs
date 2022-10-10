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
    public class Mini2 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private int _screenWidth;
        private int _screenHeight;

        private Texture2D _ciricleTexture;
        private Vector2 _position;
        private Vector2 _scale;
        private Color _color;
        private float _time;
        public Mini2()
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
            _position = new Vector2();
            _scale = new Vector2();
            _color = Color.Black;
            _time = 0;
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _ciricleTexture = Content.Load<Texture2D>(GameConstants.CIRCLE_TEXTURE_PATH);
        }

        protected override void Update(GameTime gameTime)
        {
            //if (Input.IsPressedOnce(Keys.Space, Keyboard.GetState()))
            {
                _time += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (_time > 1)
                    _time = 0;
                _position.X = Lerp(200, 400, _time);
                _position.Y = Lerp(200, 400, _time);
                _color.A = (byte)Lerp(0, 255, _time);
                _scale.X = Lerp(1, 1, _time);
                _scale.Y = Lerp(1, 1, _time);
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(blendState: BlendState.AlphaBlend);
            _spriteBatch.Draw(_ciricleTexture, _position, null, _color, 0, GameConstants.circleOrigin, _scale, SpriteEffects.None, 0);
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        private float Lerp(float min, float max, float val)
        {
            return min + (max - min) * val;
        }
    }
}