using CodingMath.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using System;
using System.Collections.Generic;

namespace CodingMath.Episodes
{
    public class Episode23Carousel : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Texture2D squareTexture;

        List<PerspectiveSquare> squares;
        Color[] randomColors;
        Matrix centerTranslation;
        float angle;

        private const int NO_OF_SQUARES = 10;
        private const float RADIUS = 200f;

        public Episode23Carousel()
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

            randomColors = new Color[]{
                Color.AliceBlue,
                Color.AntiqueWhite,
                Color.Aqua,
                Color.Beige,
                Color.CadetBlue,
                Color.DarkBlue,
                Color.Violet,
                Color.Orange,
                Color.Red,
                Color.Black,
            };
            squares = new List<PerspectiveSquare>();

            for (int i = 0; i < NO_OF_SQUARES; i++)
            {
                angle = (MathF.PI * 2) / NO_OF_SQUARES * i;
                float x = (MathF.Cos(angle) * RADIUS);
                float z = (MathF.Sin(angle) * RADIUS) + RADIUS;
                squares.Add(new PerspectiveSquare(
                    new Vector3(x, 0, z),
                    squareTexture,
                    randomColors[i % randomColors.Length],
                    angle
                ));
            }
            angle = 0;

            squares.Sort(ZSort);
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            squareTexture = Content.Load<Texture2D>(GameConstants.SQUARE_TEXTURE_PATH);
        }

        protected override void Update(GameTime gameTime)
        {
            // if (Input.IsPressedOnce(Keys.Space, Keyboard.GetState()))
            {
                angle = ((float)gameTime.ElapsedGameTime.TotalSeconds);
                for (int i = 0; i < squares.Count; i++)
                {
                    squares[i].UpdateAngle(angle, RADIUS);
                }
                squares.Sort(ZSort);
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(transformMatrix: centerTranslation);
            for (int i = 0; i < squares.Count; i++)
            {
                squares[i].Draw(_spriteBatch);
            }
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        int ZSort(PerspectiveSquare s1, PerspectiveSquare s2)
        {
            return (int)(s2.position.Z - s1.position.Z);
        }
    }
}