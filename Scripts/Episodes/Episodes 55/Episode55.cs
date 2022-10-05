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
    public class Episode55 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D _wheel;

        public Episode55()
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
            _wheel = Content.Load<Texture2D>("wheel");
        }

        protected override void Update(GameTime gameTime)
        {

        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            float aspectRatio = 488f / 461f;//1600f / 900f; //488f/461f
            float width = 0.5f; //means half width == 488/2 = 244
            float height = width / aspectRatio; //adjusting the height according to width
            float height1 = 0.5f;//means half height
            float width1 = height1 * aspectRatio; //adjusting the width according to height

            _spriteBatch.Begin();
            //fit using cavnas width(image width is matched with canvas width)
            _spriteBatch.Draw(_wheel, Vector2.Zero, null, Color.White, 0, Vector2.Zero, new Vector2(width, height), SpriteEffects.None, 0);
            _spriteBatch.DrawRectangle(0, 0, 244f, 461 * height, Color.Black, 1, 0);
            //fit using canvas height(image height is matched with canvas height)
            _spriteBatch.Draw(_wheel, Vector2.One * 200, null, Color.White, 0, Vector2.Zero, new Vector2(width1, height1), SpriteEffects.None, 0);
            _spriteBatch.DrawRectangle(200, 200, 244f + (244 * (0.0585684f)), 461 * 0.5f, Color.Black, 1, 0); /// 0.05 = 1 - 1.0585684f (where 1.0585684f is aspect ratio)
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}