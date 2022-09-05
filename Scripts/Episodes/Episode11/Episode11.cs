using CodingMath.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using System;
using System.Collections.Generic;

namespace CodingMath.Episodes
{
    public class Episode11 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private int _width;
        private int _height;


        private Particle sun;
        private Particle earth;

        public Episode11()
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

            sun = new Particle(Content);
            sun.mass = 100000;
            sun.position = new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2);
            earth = new Particle(Content);
            earth.Scale = new Vector2(0.4f);
            InitEarth();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            sun.Update(gameTime);

            // if (Input.IsPressedOnce(Keys.Space, Keyboard.GetState()))
            {
                earth.GravitateTo(sun);
                earth.Update(gameTime);
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
            earth.position = sun.position + new Vector2(200, 0);
            float vel = MathF.Sqrt(sun.mass / (sun.position - earth.position).GetLengthSquared());

            earth.velocity = new Vector2(0, 18);

            // earth.velocity.SetLength(10);
            // earth.velocity.SetAngle((-MathF.PI / 4) * 0.0174533f);
        }
    }
}