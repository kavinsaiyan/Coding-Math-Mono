using CodingMath.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using System;
using System.Collections.Generic;

namespace CodingMath.Episodes
{
    public class Episode24Extra : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Matrix centerTranslation;
        public const int PARTICLE_COUNT = 200;
        public const int CENTERZ = 1000;
        float baseAngle = 0, rotationSpeed = 0.01f;
        Texture2D circleTexture;
        List<PerspectiveParticle> perspectiveParticles;

        private const float UPPER_LIMIT = -2000f;
        private const float LOWER_LIMIT = 2000f;

        public Episode24Extra()
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
            perspectiveParticles = new List<PerspectiveParticle>(PARTICLE_COUNT);
            for (int i = 0; i < PARTICLE_COUNT; i++)
            {
                PerspectiveParticle perspectiveParticle = new PerspectiveParticle();
                perspectiveParticle.angle = CommonFunctions.RandomRange(0, MathF.PI * 2);
                perspectiveParticle.radius = CommonFunctions.RandomRange(60, 600);
                perspectiveParticle.position.X = (MathF.Cos(perspectiveParticle.angle + baseAngle) * perspectiveParticle.radius);
                perspectiveParticle.position.Y = CommonFunctions.RandomRange(LOWER_LIMIT, UPPER_LIMIT);
                perspectiveParticle.position.Z = (MathF.Sin(perspectiveParticle.angle + baseAngle) * perspectiveParticle.radius) + CENTERZ;
                perspectiveParticles.Add(perspectiveParticle);
            }
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
                //update rotation speed base on mouse position
                rotationSpeed = (Mouse.GetState().Position.X - GraphicsDevice.Viewport.Width / 2) * 0.00005f;
                baseAngle += rotationSpeed;

                perspectiveParticles.Sort(zSort);
                for (int i = 0; i < PARTICLE_COUNT; i++)
                {
                    float perspective = GameConstants.FOCAL_LENGTH / (GameConstants.FOCAL_LENGTH + perspectiveParticles[i].position.Z);
                    //update screen pos and scale
                    perspectiveParticles[i].Update(perspective);
                    //update alpha 
                    perspectiveParticles[i].color.A = (byte)CommonFunctions.Remap(perspectiveParticles[i].position.Y, LOWER_LIMIT, UPPER_LIMIT, 255, 0);

                    //recalculate position after rotation
                    perspectiveParticles[i].position.X = (MathF.Cos(perspectiveParticles[i].angle + baseAngle) * perspectiveParticles[i].radius);
                    perspectiveParticles[i].position.Z = (MathF.Sin(perspectiveParticles[i].angle + baseAngle) * perspectiveParticles[i].radius) + CENTERZ;
                    //move the particle upwards
                    perspectiveParticles[i].position.Y -= 1;

                    if (perspectiveParticles[i].position.Y < UPPER_LIMIT)
                    {
                        perspectiveParticles[i].position.Y = LOWER_LIMIT;
                    }
                }
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(transformMatrix: centerTranslation, blendState: BlendState.AlphaBlend);
            for (int i = 0; i < PARTICLE_COUNT; i++)
            {
                perspectiveParticles[i].Draw(_spriteBatch, circleTexture);
            }
            _spriteBatch.End();

            base.Draw(gameTime);
        }


        int zSort(PerspectiveParticle particleA, PerspectiveParticle particleB)
        {
            return (int)(particleB.position.Z - particleA.position.Z);
        }

        class PerspectiveParticle
        {
            public Vector3 position;
            public float angle;
            public float radius;
            public Vector2 scale;
            public Vector2 screenPosition;
            public Color color = Color.Black;

            public void Update(float perspective)
            {
                scale.X = scale.Y = perspective;
                screenPosition.X = position.X * perspective;
                screenPosition.Y = position.Y * perspective;
            }

            public void Draw(SpriteBatch spriteBatch, Texture2D texture2D)
            {
                spriteBatch.Draw(texture2D, screenPosition, null, color, 0, GameConstants.circleOrigin, scale, SpriteEffects.None, 0);
            }
        }
    }
}