using CodingMath.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using System;
using System.Collections.Generic;

namespace CodingMath.Episodes
{
    public class Episode26RotateCube : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        public const float CENTERZ = 300;
        Matrix centerTranslation;
        const int VERTEX_COUNT_IN_CUBE = 8;
        ModelVertex[] points;


        public Episode26RotateCube()
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

            points = new ModelVertex[VERTEX_COUNT_IN_CUBE];

            points[0] = new ModelVertex(-200, -200, 100);
            points[1] = new ModelVertex(-200, -200, 500);
            points[2] = new ModelVertex(200, -200, 500);
            points[3] = new ModelVertex(200, -200, 100);

            points[4] = new ModelVertex(-200, 200, 100);
            points[5] = new ModelVertex(-200, 200, 500);
            points[6] = new ModelVertex(200, 200, 500);
            points[7] = new ModelVertex(200, 200, 100);
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
                Vector3 rotation = new Vector3();
                float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds * 100;
                //left
                if (keyboardState.IsKeyDown(Keys.A))
                {
                    if (keyboardState.IsKeyDown(Keys.LeftControl))
                        rotation.X = 0.05f;
                    else
                        translation.X = deltaTime;
                }
                //right
                if (keyboardState.IsKeyDown(Keys.D))
                {
                    if (keyboardState.IsKeyDown(Keys.LeftControl))
                        rotation.X = -0.05f;
                    else
                        translation.X = -deltaTime;
                }
                //up
                if (keyboardState.IsKeyDown(Keys.Q))
                {
                    if (keyboardState.IsKeyDown(Keys.LeftControl))
                        rotation.Y = 0.05f;
                    else
                        translation.Y = deltaTime;
                }
                //down
                if (keyboardState.IsKeyDown(Keys.E))
                {
                    if (keyboardState.IsKeyDown(Keys.LeftControl))
                        rotation.Y = -0.05f;
                    else
                        translation.Y = -deltaTime;
                }
                //forward
                if (keyboardState.IsKeyDown(Keys.W))
                {
                    if (keyboardState.IsKeyDown(Keys.LeftControl))
                        rotation.Z = 0.05f;
                    else
                        translation.Z = deltaTime;
                }
                //backward
                if (keyboardState.IsKeyDown(Keys.S))
                {
                    if (keyboardState.IsKeyDown(Keys.LeftControl))
                        rotation.Z = -0.05f;
                    else
                        translation.Z = -deltaTime;
                }

                for (int i = 0; i < VERTEX_COUNT_IN_CUBE; i++)
                {
                    points[i].Rotate(rotation);
                    points[i].TranslateModel(translation);
                }
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
    }
}