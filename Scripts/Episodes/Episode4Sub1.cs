using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using System;

namespace CodingMath.Episodes
{
    public class Episode4Sub1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private int _width;
        private int _height;
        private Texture2D _circleTexture;
        private Vector2 _center;
        private const int NO_OF_CIRCLES = 10;
        private const float RADIUS = 200f;

        public Episode4Sub1()
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
            _center = new Vector2(_width / 2, _height / 2);
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _circleTexture = Content.Load<Texture2D>(GameConstants.CIRCLE_TEXTURE_PATH);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            float angleOffset = (MathF.PI * 2) / NO_OF_CIRCLES;
            float angle = 0;
            for (int i = 0; i < NO_OF_CIRCLES; i++)
            {
                float x = _center.X + MathF.Cos(angle) * RADIUS;
                float y = _center.Y + MathF.Sin(angle) * RADIUS;
                _spriteBatch.Draw(_circleTexture, new Vector2(x, y), null, Color.Black, 0, GameConstants.circleOrigin, Vector2.One, SpriteEffects.None, 0);
                angle += angleOffset;
            }
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
