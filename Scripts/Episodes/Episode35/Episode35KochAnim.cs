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
    public class Episode35KochAnim : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Vector2 p0, p1, p2;
        public Episode35KochAnim()
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
            float time = (float)gameTime.TotalGameTime.TotalSeconds;
            Koch(p1, p0, _spriteBatch, 4, time);
            Koch(p2, p1, _spriteBatch, 4, time);
            Koch(p0, p2, _spriteBatch, 4, time);
            _spriteBatch.End();
            base.Draw(gameTime);
        }


        private void Koch(Vector2 p0, Vector2 p1, SpriteBatch spriteBatch, int limit, float time)
        {
            float dx = p1.X - p0.X;
            float dy = p1.Y - p0.Y;
            float length = System.MathF.Sqrt(dx * dx + dy * dy);
            float unit = length / 3;
            float t = System.MathF.Sin(time);
            float angle = System.MathF.Atan2(dy, dx) + t;

            Vector2 pA, pC, pB;
            // WriteTimeBasedPositionsToPoints1(p0, p1, dx, dy, unit, t, angle, out pA, out pC, out pB);
            // WriteTimeBasedPositionsToPoints2(p0, p1, dx, dy, unit, t, angle, out pA, out pC, out pB);
            WriteTimeBasedPositionsToPoints3(p0, p1, dx, dy, unit, t, angle, out pA, out pC, out pB);

            if (limit > 0)
            {
                Koch(p0, pA, spriteBatch, limit - 1, time);
                Koch(pA, pB, spriteBatch, limit - 1, time);
                Koch(pB, pC, spriteBatch, limit - 1, time);
                Koch(pC, p1, spriteBatch, limit - 1, time);
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

        private static void WriteTimeBasedPositionsToPoints1(Vector2 p0, Vector2 p1, float dx, float dy, float unit, float t, float angle, out Vector2 pA, out Vector2 pC, out Vector2 pB)
        {
            pA = new Vector2(p0.X + dx / 3, p0.Y + dy / 3);
            pC = new Vector2(p1.X - dx / 3, p1.Y - dy / 3);
            pB = new Vector2(
                pA.X + System.MathF.Cos(t + angle - System.MathF.PI / 3) * unit,
                pA.Y + System.MathF.Sin(t + angle - System.MathF.PI / 3) * unit
            );
        }

        private static void WriteTimeBasedPositionsToPoints2(Vector2 p0, Vector2 p1, float dx, float dy, float unit, float t, float angle, out Vector2 pA, out Vector2 pC, out Vector2 pB)
        {
            pA = new Vector2(p0.X + (dx / 3) * t, p0.Y + (dy / 3) * t);
            pC = new Vector2(p1.X - (dx / 3) * t, p1.Y - (dy / 3) * t);
            pB = new Vector2(
                            pA.X + System.MathF.Cos(angle - (System.MathF.PI / 3) * t) * unit,
                            pA.Y + System.MathF.Sin(angle - (System.MathF.PI / 3) * t) * unit
                        );
        }
        private static void WriteTimeBasedPositionsToPoints3(Vector2 p0, Vector2 p1, float dx, float dy, float unit, float t, float angle, out Vector2 pA, out Vector2 pC, out Vector2 pB)
        {
            pA = new Vector2(p0.X + dx * t, p0.Y + dy * t);
            pC = new Vector2(p1.X - dx * t, p1.Y - dy * t);
            pB = new Vector2(
                            pA.X + System.MathF.Cos(angle - System.MathF.PI * t) * unit,
                            pA.Y + System.MathF.Sin(angle - System.MathF.PI * t) * unit
                        );
        }

    }
}