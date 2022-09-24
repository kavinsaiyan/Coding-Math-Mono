using CodingMath.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using System;
using System.Collections.Generic;

namespace CodingMath.Episodes
{
    public class Episode20 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Vector2[] mulitCurvePoints;

        public Episode20()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
            mulitCurvePoints = new Vector2[]
            {
                new Vector2(200,400),
                new Vector2(300,300),
                new Vector2(400,400),
                new Vector2(500,300),
                new Vector2(600,400),
            };
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            UtilityFunctions.MultiCurve(mulitCurvePoints, _spriteBatch, GraphicsDevice);
            for (int i = 0; i < mulitCurvePoints.Length; i++)
            {
                _spriteBatch.DrawCircle(mulitCurvePoints[i], 4f, 32, Color.Black, 1, 0);
            }

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}