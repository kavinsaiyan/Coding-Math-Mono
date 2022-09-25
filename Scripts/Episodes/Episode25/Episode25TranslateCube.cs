using CodingMath.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using System;
using System.Collections.Generic;

namespace CodingMath.Episodes
{
    public class Episode25TranslateCube : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Matrix centerTranslation;
        const int VERTEX_COUNT_IN_CUBE = 8;
        Point[] points;


        public Episode25TranslateCube()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
            centerTranslation = Matrix.CreateTranslation(GraphicsDevice.Viewport.Width / 2,
                    GraphicsDevice.Viewport.Height / 2, 0);

            points = new Point[VERTEX_COUNT_IN_CUBE];

            points[0] = new Point(-200, -200, 100, GameConstants.FOCAL_LENGTH);
            points[1] = new Point(-200, -200, 500, GameConstants.FOCAL_LENGTH);
            points[2] = new Point(200, -200, 500, GameConstants.FOCAL_LENGTH);
            points[3] = new Point(200, -200, 100, GameConstants.FOCAL_LENGTH);

            points[4] = new Point(-200, 200, 100, GameConstants.FOCAL_LENGTH);
            points[5] = new Point(-200, 200, 500, GameConstants.FOCAL_LENGTH);
            points[6] = new Point(200, 200, 500, GameConstants.FOCAL_LENGTH);
            points[7] = new Point(200, 200, 100, GameConstants.FOCAL_LENGTH);
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            // if (Input.IsPressedOnce(Keys.Space, Keyboard.GetState()))
            {
                KeyboardState keyboardState = Keyboard.GetState();
                Vector3 translation = new Vector3();
                float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds * 100;
                //left
                if (keyboardState.IsKeyDown(Keys.A))
                    translation.X += deltaTime;
                //right
                if (keyboardState.IsKeyDown(Keys.D))
                    translation.X -= deltaTime;
                //up
                if (keyboardState.IsKeyDown(Keys.Q))
                    translation.Y += deltaTime;
                //down
                if (keyboardState.IsKeyDown(Keys.E))
                    translation.Y -= deltaTime;
                //forward
                if (keyboardState.IsKeyDown(Keys.W))
                    translation.Z += deltaTime;
                //backward
                if (keyboardState.IsKeyDown(Keys.S))
                    translation.Z -= deltaTime;

                for (int i = 0; i < VERTEX_COUNT_IN_CUBE; i++)
                    points[i].TranslateModel(translation);
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(transformMatrix: centerTranslation);

            DrawLines(_spriteBatch, new int[] { 0, 1, 2, 3, 0 });
            DrawLines(_spriteBatch, new int[] { 4, 5, 6, 7, 4 });
            DrawLines(_spriteBatch, new int[] { 0, 4 });
            DrawLines(_spriteBatch, new int[] { 1, 5 });
            DrawLines(_spriteBatch, new int[] { 2, 6 });
            DrawLines(_spriteBatch, new int[] { 3, 7 });

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        private void DrawLines(SpriteBatch spriteBatch, int[] pointsIndex)
        {
            for (int i = 1; i < pointsIndex.Length; i++)
            {
                Vector2 point1ScreenPos = points[pointsIndex[i - 1]].screenPosition;
                Vector2 point2ScreenPos = points[pointsIndex[i]].screenPosition;
                spriteBatch.DrawLine(point1ScreenPos, point2ScreenPos, Color.Black, 2, 0);
            }
        }

        class Point
        {
            public Vector3 position;
            public Vector2 screenPosition;

            private readonly float focalLength;

            public Point(float x, float y, float z, float focalLength)
            {
                position = new Vector3(x, y, z);
                this.focalLength = focalLength;
                UpdateScreenPosition();
            }

            void UpdateScreenPosition()
            {
                float perspective = focalLength / (focalLength + position.Z);
                screenPosition.X = position.X * perspective;
                screenPosition.Y = position.Y * perspective;
            }

            public void TranslateModel(Vector3 translation)
            {
                position += translation;
                UpdateScreenPosition();
            }
        }
    }
}