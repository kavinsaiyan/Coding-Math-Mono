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
    public class Episode34 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Star _star1;
        private Star _star2;

        private bool _isIntersecting = false;

        public Episode34()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
            _star1 = new Star(Vector2.Zero);
            _star2 = new Star(new Vector2(100, 100));
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

                _star1.position = mouseState.Position.ToVector2();

                _isIntersecting = _star1.IntersectsWith(_star2, out _);

                _star1.UpdateStar();
                _star2.UpdateStar();
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            if (_isIntersecting)
            {
                _star1.Draw(_spriteBatch, Color.Red);
                _star2.Draw(_spriteBatch, Color.Red);
            }
            else
            {
                _star1.Draw(_spriteBatch, Color.Black);
                _star2.Draw(_spriteBatch, Color.Black);
            }
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