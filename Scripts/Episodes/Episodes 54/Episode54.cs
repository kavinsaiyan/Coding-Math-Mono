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
    public class Episode54 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private DraggablePoint[] _draggablePoints;

        public Episode54()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
            _draggablePoints = new DraggablePoint[3];
            _draggablePoints[0] = new DraggablePoint(Content, new Vector2(40, 40), 32);
            _draggablePoints[1] = new DraggablePoint(Content, new Vector2(200, 200), 32);
            _draggablePoints[2] = new DraggablePoint(Content, new Vector2(200, 40), 32);
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
                for (int i = 0; i < _draggablePoints.Length; i++)
                {
                    _draggablePoints[i].Update(gameTime, mouseState);
                }

                Vector2 v0 = _draggablePoints[1].Position - _draggablePoints[0].Position;
                Vector2 v1 = _draggablePoints[1].Position - _draggablePoints[2].Position;

                float dot = Dot(v0.NormalizedCopy(), v1.NormalizedCopy());
                Debug.Log("Dot : " + dot);
                float angleInRadians = AngleInRadians(dot, v0.NormalizedCopy(), v1.NormalizedCopy());
                Debug.Log("Angle In Radians : " + angleInRadians);
                Debug.Log("Angle in Degrees : " + (angleInRadians * 180) / MathF.PI);
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            for (int i = 0; i < _draggablePoints.Length; i++)
            {
                _draggablePoints[i].Draw(_spriteBatch);
            }
            _spriteBatch.DrawLine(_draggablePoints[0].Position, _draggablePoints[1].Position, Color.Black, 2, 0);
            _spriteBatch.DrawLine(_draggablePoints[2].Position, _draggablePoints[1].Position, Color.Black, 2, 0);
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        private float Dot(Vector2 v0, Vector2 v1)
        {
            return v0.X * v1.X + v0.Y * v1.Y;
        }

        private float AngleInRadians(float dot, Vector2 v0, Vector2 v1)
        {
            return MathF.Acos(dot / (v0.Length() * v1.Length()));
        }
    }
}