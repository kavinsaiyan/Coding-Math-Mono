using CodingMath.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using System;

namespace CodingMath.Episodes
{
    public class Episode4Sub2 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private int _width;
        private int _height;
        private Texture2D _circleTexture;
        private const int NO_OF_BEES = 20;
        private const float MIN_RADIUS = 60f;
        private const float MAX_RADIUS = 200f;
        private const float MIN_SPEED = 0.04f;
        private const float MAX_SPEED = 0.1f;

        private Bee[] bees;

        public Episode4Sub2()
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

            bees = new Bee[NO_OF_BEES];
            for (int i = 0; i < NO_OF_BEES; i++)
            {
                bees[i] = new Bee();
            }
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
            for (int i = 0; i < NO_OF_BEES; i++)
            {
                bees[i].Update();
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(transformMatrix: Matrix.CreateTranslation(_width / 2, _height / 2, 0));

            for (int i = 0; i < NO_OF_BEES; i++)
            {
                bees[i].Draw(_spriteBatch, _circleTexture);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        class Bee
        {
            public Vector2 angle;
            public Vector2 speed;
            public float radius;
            public Vector2 position;
            public Bee()
            {
                angle.X = CommonFunctions.RandomRange(0, MathF.PI * 2);
                angle.Y = CommonFunctions.RandomRange(0, MathF.PI * 2);
                speed.X = CommonFunctions.RandomRange(MIN_SPEED, MAX_SPEED);
                speed.Y = CommonFunctions.RandomRange(MIN_SPEED, MAX_SPEED);
                radius = CommonFunctions.RandomRange(MIN_RADIUS, MAX_RADIUS);
            }

            public void Draw(SpriteBatch spriteBatch, Texture2D texture2D)
            {
                spriteBatch.Draw(texture2D, position, null, Color.Black, 0, GameConstants.circleOrigin, Vector2.One / 4, SpriteEffects.None, 0);
            }

            public void Update()
            {
                position.X = MathF.Cos(angle.X) * radius;
                position.Y = MathF.Sin(angle.Y) * radius;
                angle.X += speed.X;
                angle.Y += speed.Y;
            }
        }
    }
}
