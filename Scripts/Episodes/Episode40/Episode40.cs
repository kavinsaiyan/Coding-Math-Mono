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
    public class Episode40 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private float _trunkRatio = 0.5f;
        private float _angleA = System.MathF.PI / 4;
        private float _angleB = System.MathF.PI / 4;

        private Vector2 p0, p1;

        public Episode40()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
            p0 = new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2);
            p1 = new Vector2(GraphicsDevice.Viewport.Width / 2, 10);
            _angleA = CommonFunctions.RandomRange(-System.MathF.PI / 2, System.MathF.PI / 2);
            _angleB = CommonFunctions.RandomRange(System.MathF.PI / 2, -System.MathF.PI / 2);
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            _angleA += 2f * (float)gameTime.ElapsedGameTime.TotalSeconds;
            _angleB -= 2f * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            DrawBranch(p0, p1, 5, _spriteBatch);
            _spriteBatch.End();
            base.Draw(gameTime);
        }

        private void DrawBranch(Vector2 p0, Vector2 p1, int limit, SpriteBatch spriteBatch)
        {
            float dx = p1.X - p0.X;
            float dy = p1.Y - p0.Y;
            Vector2 diff = new Vector2(dx, dy);
            float angle = System.MathF.Atan2(dy, dx);
            Vector2 pA = diff * _trunkRatio + p0;
            float branchLength = diff.Length() * (1 - _trunkRatio);
            Vector2 pB = pA + new Vector2(System.MathF.Cos(angle + _angleA), System.MathF.Sin(angle + _angleA)) * branchLength;
            Vector2 pC = pA + new Vector2(System.MathF.Cos(angle - _angleB), System.MathF.Sin(angle - _angleB)) * branchLength;

            // DrawBranch(p0, pA, limit - 1, spriteBatch);
            spriteBatch.DrawLine(p0, pA, Color.Black);

            if (limit > 0)
            {
                DrawBranch(pA, pB, limit - 1, spriteBatch);
                DrawBranch(pA, pC, limit - 1, spriteBatch);
            }
            else
            {
                spriteBatch.DrawLine(pA, pB, Color.Black);
                spriteBatch.DrawLine(pA, pC, Color.Black);
            }
        }
    }
}