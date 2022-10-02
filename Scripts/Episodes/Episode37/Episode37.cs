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
    public class Episode37 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Texture2D _circleTexture;

        VerletIntegrationPoint[] _points;

        VerletIntegrationLine[] _lines;

        private int _screenWidth;
        private int _screenHeight;

        public Episode37()
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

            _points = new VerletIntegrationPoint[4]
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
                }
            };

            _lines = new VerletIntegrationLine[]
            {
                new VerletIntegrationLine(_points[0],_points[1],GetLengthBetweenPoints(_points[0],_points[1])),
                new VerletIntegrationLine(_points[1],_points[2],GetLengthBetweenPoints(_points[1],_points[2])),
                new VerletIntegrationLine(_points[2],_points[3],GetLengthBetweenPoints(_points[2],_points[3])),
                new VerletIntegrationLine(_points[3],_points[0],GetLengthBetweenPoints(_points[3],_points[0])),
            };
        }
        protected override void Update(GameTime gameTime)
        {
            // if (Input.IsPressedOnce(Keys.Space, Keyboard.GetState()))
            {
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
            for (int i = 0; i < _lines.Length; i++)
            {
                _lines[i].Draw(_spriteBatch, true);
            }
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        float GetLengthBetweenPoints(VerletIntegrationPoint point1, VerletIntegrationPoint point2)
        {
            return Vector2.Distance(point1.position, point2.position);
        }
    }
}