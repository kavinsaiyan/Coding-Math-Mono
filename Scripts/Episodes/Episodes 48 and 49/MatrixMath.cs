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
    public class MatrixMath : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Texture2D _squareTexture;

        public MatrixMath()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _squareTexture = Content.Load<Texture2D>(GameConstants.SQUARE_TEXTURE_PATH);
        }

        protected override void Update(GameTime gameTime)
        {
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // Matrix transformationMatrix = Matrix.CreateTranslation(100, 100, 0) * Matrix.CreateRotationZ(45);
            // Matrix transformationMatrix = Matrix.CreateRotationZ(45) * Matrix.CreateTranslation(100, 100, 0);
            //TRS Matrix
            Matrix transformationMatrix = Matrix.CreateScale(2, 1, 1) * Matrix.CreateRotationZ(10) * Matrix.CreateTranslation(100, 100, 0);
            //SRT Matrix
            Matrix inverseTransformationMatrix = Matrix.CreateTranslation(-100, -100, 0) * Matrix.CreateRotationZ(-10) * Matrix.CreateScale(1 / 2, 1 / 1, 1 / 1);

            // Debug.Log("Translated Pt : " + Matrix.CreateTranslation(-100, -100, 0) * Matrix.CreateTranslation(100, 100, 0));
            // Debug.Log("Scaled Pt : " + Matrix.CreateScale(2, 0.6f, 1) * Matrix.CreateScale(1 / 2, 1 / 0.6f, 1));
            // Debug.Log("Rotated Pt : " + Matrix.CreateRotationZ(-10) * Matrix.CreateRotationZ(10));

            _spriteBatch.Begin(transformMatrix: transformationMatrix);
            _spriteBatch.Draw(_squareTexture, Vector2.Zero, null, Color.White, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 0);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}