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
    public class Episode56 : Game
    {
        enum Align { Top, Middle, Bottom }
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Rectangle[] _rectangles;
        private const int NO_OF_RECTANGLES = 12;
        private const int MIN_WIDTH = 80;
        private const int MAX_WIDTH = 140;
        private const int MIN_HEIGHT = 80;
        private const int MAX_HEIGHT = 140;
        private const int SPACING_X = 20;
        private const int SPACING_Y = 20;

        private int _screenWidth;
        private int _screenHeight;

        private Align _align = Align.Top;

        public Episode56()
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

            _screenWidth = GraphicsDevice.Viewport.Width;
            _screenHeight = GraphicsDevice.Viewport.Height;

            // HBoxLayoutWithoutAlignment();
            HBoxLayoutWithoutAlignmentWithAlignment();
        }

        private void HBoxLayoutWithoutAlignmentWithAlignment()
        {
            _rectangles = new Rectangle[NO_OF_RECTANGLES];
            int x = 0, y = 0;
            int maxHeightInARow = 0;
            int maxHeight = 0;
            int[] rectangleYPos = new int[NO_OF_RECTANGLES];
            //create the rectangles with random height
            for (int i = 0; i < NO_OF_RECTANGLES; i++)
            {
                int width = CommonFunctions.RandomRangeInt(MIN_WIDTH, MAX_WIDTH);
                int height = CommonFunctions.RandomRangeInt(MIN_HEIGHT, MAX_HEIGHT);
                _rectangles[i] = new Rectangle(x, y, width, height);

                if (height > maxHeightInARow)
                    maxHeightInARow = height;
                if (height > maxHeight)
                    maxHeight = height;

                x += width + SPACING_X;
                rectangleYPos[i] = y;

                if (x + (MIN_WIDTH + MAX_WIDTH) / 2 + SPACING_X > _screenWidth)
                {
                    //wrapping code
                    x = 0;
                    y += maxHeightInARow + SPACING_Y;
                    maxHeightInARow = 0;
                }

                Debug.Log($"Rectangles[{i}] {_rectangles[i]}");
            }

            for (int i = 0; i < NO_OF_RECTANGLES; i++)
            {
                // assign the correct height back to the rectangle for looping
                if (_align == Align.Top)
                {
                    ResetYPos(rectangleYPos, i);
                }

                //assiging the half of the difference between maxheight and rectangle (elemnent) height 
                //as the y position to draw the element at
                if (_align == Align.Middle)
                {
                    ResetYPos(rectangleYPos, i);
                    _rectangles[i].Y += (maxHeight - _rectangles[i].Height) / 2;
                }
                //assiging the difference between maxheight and rectangle (elemnent) height 
                //as the y position to draw the element at
                if (_align == Align.Bottom)
                {
                    ResetYPos(rectangleYPos, i);
                    _rectangles[i].Y += maxHeight - _rectangles[i].Height;
                }
            }

            void ResetYPos(int[] rectangleYPos, int i)
            {
                _rectangles[i].Y = rectangleYPos[i];
            }
        }

        private void HBoxLayoutWithoutAlignment()
        {
            _rectangles = new Rectangle[NO_OF_RECTANGLES];
            int x = 0, y = 0;
            int maxHeight = 0;
            for (int i = 0; i < NO_OF_RECTANGLES; i++)
            {
                int width = CommonFunctions.RandomRangeInt(MIN_WIDTH, MAX_WIDTH);
                int height = CommonFunctions.RandomRangeInt(MIN_HEIGHT, MAX_HEIGHT);
                _rectangles[i] = new Rectangle(x, y, width, height);

                if (height > maxHeight)
                    maxHeight = height;

                x += width + SPACING_X;
                if (x + width + SPACING_X > _screenWidth)
                {
                    //wrapping code
                    x = 0;
                    y += maxHeight + SPACING_Y;
                    maxHeight = 0;
                }
                // Debug.Log($"Rectangles[{i}] {_rectangles[i]}");
            }
        }


        protected override void Update(GameTime gameTime)
        {
            if (Input.IsPressedOnce(Keys.Space, Keyboard.GetState()))
            {
                _align = (Align)(((int)_align + 1) % 3);
                HBoxLayoutWithoutAlignmentWithAlignment();
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            for (var i = 0; i < NO_OF_RECTANGLES; i++)
            {
                _spriteBatch.DrawRectangle(_rectangles[i].X, _rectangles[i].Y,
                            _rectangles[i].Width, _rectangles[i].Height, Color.Black, 1, 0);
            }
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}