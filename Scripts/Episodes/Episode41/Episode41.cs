using CodingMath.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using CodingMath.InputSystem;
namespace CodingMath.Episodes
{
    public class Episode41 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        const float TILE_HEIGHT = 50;
        const float TILE_WIDTH = 100;

        public Episode41()
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
        }

        protected override void Update(GameTime gameTime)
        {
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(transformMatrix: Matrix.CreateTranslation(new Vector3(400, 0, 0)));
            for (int x = 0; x < 10; x++)
            {
                for (int y = 0; y < 10; y++)
                {
                    DrawTile(x, y, _spriteBatch);
                }
            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }

        void DrawTile(float x, float y, SpriteBatch spriteBatch)
        {
            Vector2 pos = new Vector2((x - y) * TILE_WIDTH / 2, (x + y) * TILE_HEIGHT / 2);

            Vector2 p0 = new Vector2(0, 0) + pos;
            Vector2 p1 = new Vector2(TILE_WIDTH / 2, TILE_HEIGHT / 2) + pos;
            Vector2 p2 = new Vector2(0, TILE_HEIGHT) + pos;
            Vector2 p3 = new Vector2(-TILE_WIDTH / 2, TILE_HEIGHT / 2) + pos;

            spriteBatch.DrawLine(p0, p1, Color.Black);
            spriteBatch.DrawLine(p1, p2, Color.Black);
            spriteBatch.DrawLine(p2, p3, Color.Black);
            spriteBatch.DrawLine(p3, p0, Color.Black);
        }
    }
}