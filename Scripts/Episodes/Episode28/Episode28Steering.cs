using CodingMath.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using System;
using System.Collections.Generic;

namespace CodingMath.Episodes
{
    public class Episode28Steering : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D _steeringWheelTexture;
        private Matrix _centerTranslation;

        private float _targetRotation = 0;
        private float _rotation = 0;
        private Vector2 _steeringWheelOrigin;
        private const float EASING = 0.1f;


        public Episode28Steering()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _steeringWheelTexture = Content.Load<Texture2D>(GameConstants.STEERING_WHEEL_TEXTURE_PATH);
            _centerTranslation = Matrix.CreateTranslation(GraphicsDevice.Viewport.Width / 2,
                GraphicsDevice.Viewport.Height / 2, 0);
            _steeringWheelOrigin = new Vector2(_steeringWheelTexture.Width / 2, _steeringWheelTexture.Height / 2);
        }

        protected override void Update(GameTime gameTime)
        {
            Vector2 mousePos = Mouse.GetState().Position.ToVector2();
            _targetRotation = CommonFunctions.Remap(mousePos.X * 2, 0,
                GraphicsDevice.Viewport.Width, -MathF.PI * 2, MathF.PI * 2);
            //aplying easing to user input
            _rotation = (_targetRotation - _rotation) * EASING;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(transformMatrix: _centerTranslation);
            _spriteBatch.Draw(_steeringWheelTexture, Vector2.Zero, null, Color.White, _rotation,
                                        _steeringWheelOrigin, Vector2.One, SpriteEffects.None, 0);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}