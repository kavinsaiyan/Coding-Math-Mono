using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using System;
using CodingMath.Utils;

namespace CodingMath.Episodes
{
    public class Episode3 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private int _width;
        private int _height;

        private Color _color;
        private Texture2D _circleTexture;


        public Episode3()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
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
            _color = Color.Black;
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _color.A = (byte)CommonFunctions.Remap(MathF.Sin((float)gameTime.TotalGameTime.TotalSeconds * 4), -1, 1, 0, 255);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(blendState: BlendState.AlphaBlend, transformMatrix: Matrix.CreateTranslation(new Vector3(_width / 2, _height / 2, 0)));
            _spriteBatch.Draw(_circleTexture, Vector2.Zero, null, _color, 0, GameConstants.circleOrigin, Vector2.One, SpriteEffects.None, 0);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
