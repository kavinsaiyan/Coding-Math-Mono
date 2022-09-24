using CodingMath.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using System;
using System.Collections.Generic;

namespace CodingMath.Episodes
{
    public class Episode19 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Vector2[] points;

        public Episode19()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
            points = new Vector2[]
            {
                new Vector2(200,200),
                new Vector2(300,100),
                new Vector2(400,200),
                new Vector2(500,100),
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
            for (float i = 0; i < 1; i += 0.01f)
            {
                Vector2 position = CommonFunctions.CubicBezier(points[0], points[1], points[2], points[3], i);

                UtilityFunctions.DrawPixel(
                    position,
                    _spriteBatch,
                    GraphicsDevice
                );
            }

            for (int i = 0; i < points.Length; i++)
            {
                _spriteBatch.DrawCircle(points[i], 4f, 32, Color.Black, 1, 0);
            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}