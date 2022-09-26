using CodingMath.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using System;
using System.Collections.Generic;

namespace CodingMath.Episodes
{
    public class Episode30 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D _circleTexture;
        private EasingTrailParticles[] _easingParticles;
        private float _ease = 0.1f;
        const int NUMBER_OF_PARTICLES = 100;
        public Episode30()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
            _easingParticles = new EasingTrailParticles[NUMBER_OF_PARTICLES];
            for (int i = 0; i < NUMBER_OF_PARTICLES; i++)
            {
                _easingParticles[i] = new EasingTrailParticles();
            }
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _circleTexture = Content.Load<Texture2D>(GameConstants.CIRCLE_TEXTURE_PATH);
        }

        protected override void Update(GameTime gameTime)
        {
            // if (Input.IsPressedOnce(Keys.Space, Keyboard.GetState()))
            {
                Vector2 leaderPosition = Mouse.GetState().Position.ToVector2();
                for (int i = 0; i < NUMBER_OF_PARTICLES; i++)
                {
                    _easingParticles[i].position = EaseTo(leaderPosition, _easingParticles[i].position, _ease);
                    leaderPosition = _easingParticles[i].position;
                }
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            for (int i = 0; i < NUMBER_OF_PARTICLES; i++)
            {
                _spriteBatch.Draw(_circleTexture, _easingParticles[i].position, null, Color.Black,
                    0, GameConstants.circleOrigin, Vector2.One / 2, SpriteEffects.None, 0);
            }
            _spriteBatch.End();


            base.Draw(gameTime);
        }

        Vector2 EaseTo(Vector2 target, Vector2 currentPosition, float ease)
        {
            Vector2 direction = (target - currentPosition);
            currentPosition = currentPosition + direction * ease;
            return currentPosition;
        }

        class EasingTrailParticles
        {
            public Vector2 position;
        }
    }
}