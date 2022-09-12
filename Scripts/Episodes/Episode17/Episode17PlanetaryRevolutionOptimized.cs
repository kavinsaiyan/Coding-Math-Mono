using CodingMath.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using System;
using System.Collections.Generic;

namespace CodingMath.Episodes
{
    public class Episode17PlanetaryRevolutionOptimized : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private int _width;
        private int _height;
        private ParticleOptimized sun;
        private ParticleOptimized earth;

        public Episode17PlanetaryRevolutionOptimized()
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
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            sun = new ParticleOptimized(Content);
            sun.mass = 100000;
            sun.positionX = GraphicsDevice.Viewport.Width / 2;
            sun.positionY = GraphicsDevice.Viewport.Height / 2;
            earth = new ParticleOptimized(Content);
            earth.scale = new Vector2(0.4f);
            InitEarth();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            sun.Update();

            // if (Input.IsPressedOnce(Keys.Space, Keyboard.GetState()))
            {
                earth.GravitateTo(sun);
                earth.Update();
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            sun.Draw(_spriteBatch);
            earth.Draw(_spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        void InitEarth()
        {
            earth.positionX = sun.positionX + 200;
            earth.positionY = sun.positionY + 200;
            earth.velocityY = 18;
        }
    }
}