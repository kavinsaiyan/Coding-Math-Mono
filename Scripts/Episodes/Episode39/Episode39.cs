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
    public class Episode39 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Texture2D _circleTexture;

        VerletIntegrationPoint[] _points;

        VerletIntegrationLine[] _lines;

        private int _screenWidth;
        private int _screenHeight;
        private Engine _engine;
        public Episode39()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
            _screenWidth = GraphicsDevice.Viewport.Width;
            _screenHeight = GraphicsDevice.Viewport.Height;
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _circleTexture = Content.Load<Texture2D>(GameConstants.CIRCLE_TEXTURE_PATH);

            _screenWidth = GraphicsDevice.Viewport.Width;
            _screenHeight = GraphicsDevice.Viewport.Height;

            _engine = new Engine(_screenWidth, _screenHeight)
            {
                position = new Vector2(300, 100),
                oldPosition = new Vector2(500, 100),
                IsPinned = true
            };
            _points = new VerletIntegrationPoint[]
            {
                //0
                new VerletIntegrationPoint(_screenWidth, _screenHeight)
                {
                    position = new Vector2(100, 100),
                    oldPosition = new Vector2(40f, 40f)
                },
                //1
                new VerletIntegrationPoint(_screenWidth, _screenHeight)
                {
                    position = new Vector2(200, 100),
                    oldPosition = new Vector2(200, 100)
                },
                //2
                new VerletIntegrationPoint(_screenWidth, _screenHeight)
                {
                    position = new Vector2(200, 200),
                    oldPosition = new Vector2(200, 200)
                },
                //3
                new VerletIntegrationPoint(_screenWidth, _screenHeight)
                {
                    position = new Vector2(100, 200),
                    oldPosition = new Vector2(100, 200)
                },
                //4
                new VerletIntegrationPoint(_screenWidth, _screenHeight)
                {
                    position = new Vector2(225, 100),
                    oldPosition = new Vector2(300, 100)
                },
                //5
                new VerletIntegrationPoint(_screenWidth, _screenHeight)
                {
                    position = new Vector2(250, 100),
                    oldPosition = new Vector2(400, 100)
                },
                //6
                _engine
            };

            _lines = new VerletIntegrationLine[]
            {
                new VerletIntegrationLine(_points[0],_points[1],GetLengthBetweenPoints(_points[0],_points[1])),
                new VerletIntegrationLine(_points[1],_points[2],GetLengthBetweenPoints(_points[1],_points[2])),
                new VerletIntegrationLine(_points[2],_points[3],GetLengthBetweenPoints(_points[2],_points[3])),
                new VerletIntegrationLine(_points[3],_points[0],GetLengthBetweenPoints(_points[3],_points[0])),
                new VerletIntegrationLine(_points[0],_points[2],GetLengthBetweenPoints(_points[0],_points[2])),
                new VerletIntegrationLine(_points[2],_points[4],GetLengthBetweenPoints(_points[0],_points[4])),
                new VerletIntegrationLine(_points[4],_points[5],GetLengthBetweenPoints(_points[4],_points[5])),
                new VerletIntegrationLine(_points[5],_points[6],GetLengthBetweenPoints(_points[5],_points[6])),
            };
        }
        protected override void Update(GameTime gameTime)
        {
            // if (Input.IsPressedOnce(Keys.Space, Keyboard.GetState()))
            {
                // _engine.UpdateEngine1();
                _engine.UpdateEngine2();
                for (int i = 0; i < _points.Length; i++)
                {
                    _points[i].Update();
                }
                for (int i = 0; i < _lines.Length; i++)
                {
                    _lines[i].Update();
                }

                //looping through update and constraint point for stability
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < _lines.Length; j++)
                    {
                        _lines[j].Update();
                    }
                    for (int j = 0; j < _points.Length; j++)
                    {
                        _points[j].ConstraintPoint();
                    }
                }
                // _line.Update();
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            // _engine.DrawEngine1(_spriteBatch);
            _engine.DrawEngine2(_spriteBatch);
            // _point1.Draw(_spriteBatch, _circleTexture);
            // _point2.Draw(_spriteBatch, _circleTexture);
            for (int i = 0; i < _lines.Length; i++)
            {
                _lines[i].Draw(_spriteBatch);
            }
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        float GetLengthBetweenPoints(VerletIntegrationPoint point1, VerletIntegrationPoint point2)
        {
            return Vector2.Distance(point1.position, point2.position);
        }

        class Engine : VerletIntegrationPoint
        {

            public float speed = 0.1f;
            public float angle = 0;
            public float range = 40;
            public Engine(int screenWidth, int screenHeight) : base(screenWidth, screenHeight) { }

            public void UpdateEngine1()
            {
                angle += speed;
                position.X = System.MathF.Cos(angle) * range + oldPosition.X;
            }

            public void DrawEngine1(SpriteBatch spriteBatch)
            {
                spriteBatch.DrawRectangle(oldPosition.X - range, oldPosition.Y - 10, range * 2, 20, Color.Black, 1, 0);
            }

            public void UpdateEngine2()
            {
                angle += speed;
                position.X = System.MathF.Cos(angle) * range + oldPosition.X;
                position.Y = System.MathF.Sin(angle) * range + oldPosition.Y;
            }

            public void DrawEngine2(SpriteBatch spriteBatch)
            {
                spriteBatch.DrawCircle(oldPosition.X, oldPosition.Y, range, 32, Color.Black, 1, 0);
            }
        }
    }
}