using CodingMath.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using System;
using System.Collections.Generic;

namespace CodingMath.Episodes
{
    public class Episode21BitmapCollision : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Particle particle1, particle2;

        Color[] particle1Colors, particle2Colors;
        Rectangle rectA;
        Rectangle rectB;
        Rectangle rectC;
        public Episode21BitmapCollision()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
            particle1.position = new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2);
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            particle1 = new Particle(Content);
            particle2 = new Particle(Content, GameConstants.RECTANGLE_TEXTURE_PATH);

            particle1Colors = particle1.GetColors();
            particle2Colors = particle2.GetColors();
        }

        protected override void Update(GameTime gameTime)
        {
            particle2.position = new Vector2(Mouse.GetState().Position.X, Mouse.GetState().Position.Y);

            string message;
            rectA = new Rectangle((int)particle1.position.X, (int)particle1.position.Y, particle1.TextureWidth, particle1.TextureHeight);
            rectB = new Rectangle((int)particle2.position.X, (int)particle2.position.Y, particle2.TextureWidth, particle2.TextureHeight);
            if (CollisionDetection(rectA, particle1Colors, rectB, particle2Colors) == true)
            {
                message = "true";
            }
            else
            {
                message = "false";
            }
            Debug.Log(message);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            particle1.DrawWithoutOffset(_spriteBatch);
            particle2.DrawWithoutOffset(_spriteBatch);
            _spriteBatch.DrawRectangle(rectA, Color.White);
            _spriteBatch.DrawRectangle(rectB, Color.White);
            _spriteBatch.DrawRectangle(rectC, Color.Green);
            _spriteBatch.End();
            base.Draw(gameTime);
        }

        private bool CollisionDetection(Rectangle rectA, Color[] dataA, Rectangle rectB, Color[] dataB)
        {
            int top = Math.Max(rectA.Top, rectB.Top);
            int bottom = Math.Min(rectA.Bottom, rectB.Bottom);
            int left = Math.Max(rectA.Left, rectB.Left);
            int right = Math.Min(rectA.Right, rectB.Right);

            // rectC = new Rectangle((left + right) / 2, (top + bottom) / 2, right - left, bottom - top);
            rectC = new Rectangle(left, top, right - left, bottom - top);

            for (int i = top; i < bottom; i++)
            {
                for (int j = left; j < right; j++)
                {
                    Color a = dataA[(j - rectA.Left) + (i - rectA.Top) * particle1.TextureWidth];
                    Color b = dataB[(j - rectB.Left) + (i - rectB.Top) * particle2.TextureWidth];
                    // Debug.Log("COlor data A : " + ((i - rectA.Top) + " length : " + dataA.Length));
                    // Debug.Log("((i - rectA.Top) * particle1.TextureWidth)" + ((i - rectA.Top) * particle1.TextureWidth));
                    if (a.A != 0 && b.A != 0)
                    {
                        return (true);
                    }
                }
            }

            return (false);
        }
    }
}