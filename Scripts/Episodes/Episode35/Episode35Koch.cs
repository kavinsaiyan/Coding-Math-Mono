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
    public class Episode35Koch : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Vector2 p0, p1, p2;
        public Episode35Koch()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
            int width = GraphicsDevice.Viewport.Width;
            int height = GraphicsDevice.Viewport.Height;
            p0 = new Vector2(100, 400);
            p1 = new Vector2(width - 100, 400);
            p2 = new Vector2(width / 2, 100);
        }
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }
        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            // DrawTriangle(p0, p1, p2, _spriteBatch);
            Koch(p1, p0, _spriteBatch, 4);
            Koch(p2, p1, _spriteBatch, 4);
            Koch(p0, p2, _spriteBatch, 4);
            _spriteBatch.End();

            base.Draw(gameTime);
        }


        private void Koch(Vector2 p0, Vector2 p1, SpriteBatch spriteBatch, int limit)
        {
            float dx = p1.X - p0.X;
            float dy = p1.Y - p0.Y;
            float length = System.MathF.Sqrt(dx * dx + dy * dy);
            float unit = length / 3;
            float angle = System.MathF.Atan2(dy, dx);

            Vector2 pA = new Vector2(p0.X + dx / 3, p0.Y + dy / 3);
            Vector2 pC = new Vector2(p1.X - dx / 3, p1.Y - dy / 3);
            Vector2 pB = new Vector2(
                pA.X + System.MathF.Cos(angle - System.MathF.PI / 3) * unit,
                pA.Y + System.MathF.Sin(angle - System.MathF.PI / 3) * unit
            );

            if (limit > 0)
            {
                Koch(p0, pA, spriteBatch, limit - 1);
                Koch(pA, pB, spriteBatch, limit - 1);
                Koch(pB, pC, spriteBatch, limit - 1);
                Koch(pC, p1, spriteBatch, limit - 1);
            }
            else
            {
                // DrawTriangle(p0, p1, p2, spriteBatch);
                spriteBatch.DrawLine(p0, pA, Color.Black, 1, 0);
                spriteBatch.DrawLine(pA, pB, Color.Black, 1, 0);
                spriteBatch.DrawLine(pB, pC, Color.Black, 1, 0);
                spriteBatch.DrawLine(pC, p1, Color.Black, 1, 0);
            }
        }
    }
}