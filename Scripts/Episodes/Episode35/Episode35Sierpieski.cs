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
    public class Episode35Sierpieski : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Vector2 p0, p1, p2;
        public Episode35Sierpieski()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
            p0 = new Vector2(200, 400);
            p1 = new Vector2(p0.X + 400, p0.Y);
            p2 = new Vector2((p0.X + p1.X) / 2, System.MathF.Cos(System.MathF.PI / 3) * (System.MathF.Abs(p1.X - p0.X) / 2));
        }
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }
        private void DrawTriangle(Vector2 p0, Vector2 p1, Vector2 p2, SpriteBatch spriteBatch)
        {
            spriteBatch.DrawLine(p0, p1, Color.Black, 1, 0);
            spriteBatch.DrawLine(p1, p2, Color.Black, 1, 0);
            spriteBatch.DrawLine(p2, p0, Color.Black, 1, 0);
        }

        private void Sirenpieski(Vector2 p0, Vector2 p1, Vector2 p2, SpriteBatch spriteBatch, int limit)
        {
            if (limit > 0)
            {
                Vector2 pA = (p0 + p1) / 2;
                Vector2 pB = (p1 + p2) / 2;
                Vector2 pC = (p2 + p0) / 2;
                Sirenpieski(p0, pA, pC, spriteBatch, limit - 1);
                Sirenpieski(pA, p1, pB, spriteBatch, limit - 1);
                Sirenpieski(pB, pC, p2, spriteBatch, limit - 1);
            }
            else
            {
                DrawTriangle(p0, p1, p2, spriteBatch);
            }
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
            Sirenpieski(p0, p1, p2, _spriteBatch, 4);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
        class Star
        {
            public Line[] lines;
            public Vector2[] offsets;
            public Vector2 position;

            public Star(Vector2 position)
            {
                this.position = position;
                lines = new Line[10];
                for (int i = 0; i < 10; i++)
                {
                    lines[i] = new Line(new Vector2(0, 0), new Vector2(0, 0));
                }
                offsets = new Vector2[10]
                {
                    new Vector2(100,0),
                    new Vector2(40,29),
                    new Vector2(31,95),
                    new Vector2(-15,48),
                    new Vector2(-81,59),
                    new Vector2(-50,0),
                    new Vector2(-81,-59),
                    new Vector2(-15,-48),
                    new Vector2(31,-95),
                    new Vector2(40,-29),
                };
            }

            public void UpdateStar()
            {
                int linesLength = lines.Length;
                for (int i = 0; i < lines.Length; i++)
                {
                    lines[i].StartPoint = position + offsets[i];
                    lines[i].EndPoint = position + offsets[(i + 1) % linesLength];
                }
            }

            public void Draw(SpriteBatch spriteBatch, Color color)
            {
                int linesLength = lines.Length;
                for (int i = 0; i < linesLength; i++)
                {
                    lines[i].Draw(spriteBatch, color);
                }
            }

            public bool IntersectsWith(Star other, out Vector2 intersection)
            {
                intersection = new Vector2(0, 0);
                for (int i = 0; i < lines.Length; i++)
                {
                    for (int j = 0; j < other.lines.Length; j++)
                    {
                        if (lines[i].LineIntersection(other.lines[j], out intersection))
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
        }
    }
}