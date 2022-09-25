using CodingMath.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using System;
using System.Collections.Generic;

namespace CodingMath.Episodes
{
    public class Episode24CrankedSpiral : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Matrix centerTranslation;
        public const float RADIUS = 200f;
        public const int POINT_COUNT = 60;
        float angleOffset;
        Vector3[] points;
        float[] yOffsets;

        Texture2D circleTexture;

        const float INCREASE_IN_HIEGHT_PER_STEP = -5f;
        const int POINTS_PER_CIRCLE = 20;
        public Episode24CrankedSpiral()
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

            points = new Vector3[POINT_COUNT];
            yOffsets = new float[POINT_COUNT];
            float y = 200;
            for (int i = 0; i < points.Length; i++)
            {
                y = SetCirclePosition(y, i);
                yOffsets[i] = CommonFunctions.RandomRange(-20, 20);
                points[i].Y += yOffsets[i];
            }
            angleOffset = 0;
        }

        private float SetCirclePosition(float y, int i, float angleOffset = 0)
        {
            float angle = ((MathF.PI * 2) / POINTS_PER_CIRCLE) * i;
            angle += angleOffset;
            float x = MathF.Cos(angle) * RADIUS;
            y += INCREASE_IN_HIEGHT_PER_STEP;
            float z = MathF.Sin(angle) * RADIUS + RADIUS;
            points[i] = new Vector3(x, y, z);
            return y;
        }


        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            circleTexture = Content.Load<Texture2D>(GameConstants.CIRCLE_TEXTURE_PATH);
        }

        protected override void Update(GameTime gameTime)
        {
            // if (Input.IsPressedOnce(Keys.Space, Keyboard.GetState()))
            {
                float y = 200;
                angleOffset += (float)gameTime.ElapsedGameTime.TotalSeconds;
                for (int i = 0; i < points.Length; i++)
                {
                    y = SetCirclePosition(y, i, angleOffset);
                    points[i].Y += yOffsets[i];
                }
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(transformMatrix: centerTranslation);
            for (int i = 0; i < points.Length; i++)
            {
                Vector2 point1 = CalcualteScreenPosition(i);

                if (i + 1 < points.Length)
                {
                    Vector2 point2 = CalcualteScreenPosition(i + 1);
                    _spriteBatch.DrawLine(point1.X, point1.Y, point2.X, point2.Y, Color.Black, 1, 0);
                }
            }
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        private Vector2 CalcualteScreenPosition(int i)
        {
            float perscpective = (GameConstants.FOCAL_LENGTH) / (GameConstants.FOCAL_LENGTH + points[i].Z);
            Vector2 screenPos = new Vector2();
            screenPos.X = points[i].X * perscpective;
            screenPos.Y = points[i].Y * perscpective;
            return screenPos;
        }
    }
}