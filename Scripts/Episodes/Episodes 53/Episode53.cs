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
    public class Episode53 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private long _seedLCG = 1;
        private long a = 1103515245;
        private int c = 12345;
        private long m = 0;
        private int _width;
        private int _height;
        private Texture2D _circleTexture;
        private bool _drawnOnce = false;
        public Episode53()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            m = (long)MathF.Pow(2, 31);
        }

        protected override void Initialize()
        {
            base.Initialize();
            _width = GraphicsDevice.Viewport.Width;
            _height = GraphicsDevice.Viewport.Height;
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _circleTexture = Content.Load<Texture2D>(GameConstants.CIRCLE_TEXTURE_PATH);
        }

        protected override void Update(GameTime gameTime)
        {
        }

        protected override void Draw(GameTime gameTime)
        {
            // GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            if (!_drawnOnce)
            {
                // Draw2DMountains();
                DrawCirclesPattern();
                _drawnOnce = true;
            }
            _spriteBatch.End();

            base.Draw(gameTime);
        }


        private void Draw2DMountains()
        {
            _seedLCG = 4;
            Vector2 prevPos = new Vector2(20, 100);
            for (int x = 20; x < 400; x += 20)
            {
                float nextPosY = 100 + RandomRange(0, 100);
                _spriteBatch.DrawLine(prevPos.X, prevPos.Y, x + 20, nextPosY, Color.Black, 1, 0);
                prevPos = new Vector2(x + 20, nextPosY);
            }

            _seedLCG = 1;
            prevPos = new Vector2(20, 300);
            for (int x = 20; x < 400; x += 20)
            {
                float nextPosY = 300 + RandomRange(0, 100);
                _spriteBatch.DrawLine(prevPos.X, prevPos.Y, x + 20, nextPosY, Color.Black, 1, 0);
                prevPos = new Vector2(x + 20, nextPosY);
            }
        }

        private void DrawCirclesPattern()
        {
            _seedLCG = 4;
            for (int i = 0; i < 40; i++)
            {
                Vector2 pos = new Vector2(RandomRange(0, _width), RandomRange(0, _height));
                float sizeMultiplier = RandomRange(1, 4);
                _spriteBatch.Draw(_circleTexture, pos, null, Color.Red,
                                        0, GameConstants.circleOrigin, new Vector2(0.5f) * sizeMultiplier, SpriteEffects.None, 0);
            }

            _seedLCG = 4;
            for (int i = 0; i < 40; i++)
            {
                Vector2 pos = new Vector2(RandomRange(0, _width), RandomRange(0, _height));
                float sizeMultiplier = RandomRange(1, 4);
                _spriteBatch.Draw(_circleTexture, pos, null, Color.Blue,
                                        0, GameConstants.circleOrigin, new Vector2(0.4f) * sizeMultiplier, SpriteEffects.None, 0);
            }
        }

        private long NextRandLCG()
        {
            _seedLCG = ((a * _seedLCG + c)) % m;
            return _seedLCG;
        }

        private float NextFloatLCG()
        {
            float s = NextRandLCG();
            return (float)(s / m);
        }

        private float RandomRange(float min, float max)
        {
            return min + NextFloatLCG() * (max - min);
        }
    }
}