using CodingMath.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using System;
using System.Collections.Generic;

namespace CodingMath.Episodes
{
    public class Episode22Perspective : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Texture2D squareTexture;

        List<PerspectiveSquare> squares;
        Color[] randomColors;
        Matrix centerTranslation;
        public Episode22Perspective()
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

            squares = new List<PerspectiveSquare>();
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
            for (int i = 0; i < 10; i++)
            {
                squares.Add(new PerspectiveSquare(
                    new Vector3(
                        CommonFunctions.RandomRange(-400, 400),
                        CommonFunctions.RandomRange(-300, 300),
                        CommonFunctions.RandomRange(0, 600)
                    ),
                    squareTexture,
                    randomColors[i % randomColors.Length]
                ));
            }

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
                squares.Sort(ZSort);
                for (int i = 0; i < squares.Count; i++)
                {
                    Vector3 deltaPosition = new Vector3(0, 0, 1f);
                    squares[i].Update(deltaPosition);
                }
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