using CodingMath.Utils;
using CodingMath.InputSystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using System;
using System.Collections.Generic;

namespace CodingMath.Episodes
{
    public class Episode16ThreeSpringsRandom : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Particle particle1, particle2, particle3;
        private int _width;
        private int _height;

        public Episode16ThreeSpringsRandom()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();

            _width = GraphicsDevice.Viewport.Width;
            _height = GraphicsDevice.Viewport.Height;
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            particle1 = new Particle(Content);
            particle1.friction = 0.95f;
            particle2 = new Particle(Content);
            particle2.friction = 0.95f;
            particle3 = new Particle(Content);
            particle3.friction = 0.95f;

            SetParticlePositions();

            // particle1.gravity = new Vector2(0, 0.1f);
            // particle2.gravity = new Vector2(0, 0.1f);
        }

        private void SetParticlePositions()
        {
            particle1.position = new Vector2(CommonFunctions.RandomRange(0, _width), CommonFunctions.RandomRange(0, _height));
            SetRandomNextRelativeParticlePosition(particle1, particle2);
            SetRandomNextRelativeParticlePosition(particle2, particle3);

            void SetRandomNextRelativeParticlePosition(Particle particle1, Particle particle2)
            {
                int tempWidthStart = (int)(particle1.position.X - 200);
                int tempWidthEnd = (int)(particle1.position.X + 200);
                int tempHeightStart = (int)(particle1.position.Y - 200);
                int tempHeightEnd = (int)(particle1.position.Y + 200);
                particle2.position = new Vector2(CommonFunctions.RandomRange(tempWidthStart, tempWidthEnd),
                                                CommonFunctions.RandomRange(tempHeightStart, tempHeightEnd));
            }
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (Input.IsPressedOnce(Keys.Space, Keyboard.GetState()))
            {
                SetParticlePositions();
                particle1.velocity = particle2.velocity = particle3.velocity = Vector2.Zero;
            }

            UpdateAllParticles((float)gameTime.ElapsedGameTime.TotalSeconds);

            base.Update(gameTime);
        }

        public void UpdateAllParticles(float deltaTime)
        {
            particle1.SpringTo(particle2, 100);
            particle1.SpringTo(particle3, 100);

            particle2.SpringTo(particle3, 100);

            // particle3.SpringTo(particle2, 100);

            particle1.Update(deltaTime);
            particle2.Update(deltaTime);
            particle3.Update(deltaTime);

            //clamp particle positions
            ClampParticlePosition(particle1);
            ClampParticlePosition(particle2);
            ClampParticlePosition(particle3);
        }

        private void ClampParticlePosition(Particle particle)
        {
            particle.position.X = MathHelper.Clamp(particle.position.X, 0, _width);
            particle.position.Y = MathHelper.Clamp(particle.position.Y, 0, _height);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            _spriteBatch.DrawLine(particle1.position.X, particle1.position.Y, particle2.position.X,
                    particle2.position.Y, Color.Black, 2, 0);
            _spriteBatch.DrawLine(particle1.position.X, particle1.position.Y, particle3.position.X,
                    particle3.position.Y, Color.Black, 2, 0);

            _spriteBatch.DrawLine(particle2.position.X, particle2.position.Y, particle3.position.X,
                    particle3.position.Y, Color.Black, 2, 0);

            particle1.Draw(_spriteBatch);
            particle2.Draw(_spriteBatch);
            particle3.Draw(_spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}