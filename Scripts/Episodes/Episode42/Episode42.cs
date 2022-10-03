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
    public class Episode42 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        const float BLOCK_HEIGHT = 50;
        const float BLOCK_WIDTH = 100;
        private const int TILE_GRID_SIZE = 4;
        private float[] _heights; public Episode42()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
            _heights = new float[TILE_GRID_SIZE * 2];
            for (int i = 0; i < _heights.Length; i++)
            {
                _heights[i] = CommonFunctions.RandomRange(1, 1);
            }
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
        }

        void DrawBlock(float x, float y, float z, SpriteBatch spriteBatch)
        {
            Vector2 pos = new Vector2((x - y) * BLOCK_WIDTH / 2, (x + y) * BLOCK_HEIGHT / 2);

            Vector2 top0 = new Vector2(0, -z * BLOCK_HEIGHT) + pos;
            Vector2 top1 = new Vector2(BLOCK_WIDTH / 2, BLOCK_HEIGHT / 2 - z * BLOCK_HEIGHT) + pos;
            Vector2 top2 = new Vector2(0, BLOCK_HEIGHT - z * BLOCK_HEIGHT) + pos;
            Vector2 top3 = new Vector2(-BLOCK_WIDTH / 2, BLOCK_HEIGHT / 2 - z * BLOCK_HEIGHT) + pos;

            Vector2 p1 = new Vector2(BLOCK_WIDTH / 2, BLOCK_HEIGHT / 2) + pos;
            Vector2 p2 = new Vector2(0, BLOCK_HEIGHT) + pos;
            Vector2 p3 = new Vector2(-BLOCK_WIDTH / 2, BLOCK_HEIGHT / 2) + pos;

            //draw top
            spriteBatch.DrawLine(top0, top1, Color.Black, 2);
            spriteBatch.DrawLine(top1, top2, Color.Black, 2);
            spriteBatch.DrawLine(top2, top3, Color.Black, 2);
            spriteBatch.DrawLine(top3, top0, Color.Black, 2);

            //draw left
            spriteBatch.DrawLine(top2, top3, Color.Blue);
            spriteBatch.DrawLine(top2, p2, Color.Blue);
            spriteBatch.DrawLine(p2, p3, Color.Blue);
            spriteBatch.DrawLine(p3, top3, Color.Blue);

            //draw right
            spriteBatch.DrawLine(top1, top2, Color.Red);
            spriteBatch.DrawLine(top2, p2, Color.Red);
            spriteBatch.DrawLine(p2, p1, Color.Red);
            spriteBatch.DrawLine(p1, top1, Color.Red);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(transformMatrix: Matrix.CreateTranslation(new Vector3(400, 200, 0)));
            for (int x = 0; x < TILE_GRID_SIZE && x < _heights.Length; x++)
            {
                for (int y = 0; y < TILE_GRID_SIZE; y++)
                {
                    DrawBlock(x, y, _heights[x + y], _spriteBatch);
                }
            }
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}