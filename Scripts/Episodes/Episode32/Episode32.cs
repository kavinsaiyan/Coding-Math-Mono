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
    public class Episode32 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Line _line1, _line2;
        private DraggablePoint[] draggablePoints;
        public Episode32()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
            _line1 = new Line(new Vector2(0, 0), new Vector2(200, 200));
            _line2 = new Line(new Vector2(200, 0), new Vector2(0, 200));
            draggablePoints = new DraggablePoint[4];
            draggablePoints[0] = new DraggablePoint(Content, new Vector2(0, 0), 32);
            draggablePoints[1] = new DraggablePoint(Content, new Vector2(200, 200), 32);
            draggablePoints[2] = new DraggablePoint(Content, new Vector2(200, 0), 32);
            draggablePoints[3] = new DraggablePoint(Content, new Vector2(0, 200), 32);

        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            // if (Input.IsPressedOnce(Keys.Space, Keyboard.GetState()))
            {
                MouseState mouseState = Mouse.GetState();
                for (int i = 0; i < 4; i++)
                {
                    draggablePoints[i].Update(gameTime, mouseState);
                }
                _line1.UpdateEndPoints(draggablePoints[0].Position, draggablePoints[1].Position);
                _line2.UpdateEndPoints(draggablePoints[2].Position, draggablePoints[3].Position);
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
            _line1.Draw(_spriteBatch);
            _line2.Draw(_spriteBatch);
            _spriteBatch.DrawCircle(LineIntersection(_line1, _line2), 10, 32, Color.Black, 1, 0);
            for (int i = 0; i < 4; i++)
            {
                draggablePoints[i].Draw(_spriteBatch);
            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }
        Vector2 LineIntersection(Line line1, Line line2)
        {
            Vector2 res = new Vector2();
            Line.StandardForm standardFormLine1 = line1.GetStandardForm();
            Line.StandardForm standardFormLine2 = line2.GetStandardForm();
            float denominator = standardFormLine1.A * standardFormLine2.B - standardFormLine2.A * standardFormLine1.B;
            res.X = (standardFormLine2.B * standardFormLine1.C - standardFormLine1.B * standardFormLine2.C) / denominator;
            res.Y = (standardFormLine1.A * standardFormLine2.C - standardFormLine2.A * standardFormLine1.C) / denominator;
            return res;
        }
    }
}