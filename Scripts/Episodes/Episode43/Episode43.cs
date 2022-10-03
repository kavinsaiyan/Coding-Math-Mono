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
    public class Episode43 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        const float TILE_HEIGHT = 50;
        const float TILE_WIDTH = 100;
        private const int TILE_GRID_SIZE = 5;
        private float[] _heights;

        private const string TILE_PATH = "Tile";

        private Texture2D _tileTexture;
        private Texture2D _circleTexture;

        private Vector2 _characterPosition;

        public Episode43()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
            _heights = new float[TILE_GRID_SIZE * 2];
            for (int i = 0; i < _heights.Length; i++)
            {
                _heights[i] = CommonFunctions.RandomRange(1, 1);
            }
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _tileTexture = Content.Load<Texture2D>(TILE_PATH);
            _circleTexture = Content.Load<Texture2D>(GameConstants.CIRCLE_TEXTURE_PATH);
            _characterPosition = new Vector2(0, 0);
        }

        protected override void Update(GameTime gameTime)
        {
            //character movement
            KeyboardState keyboardState = Keyboard.GetState();

            if (Input.IsPressedOnce(Keys.A, keyboardState))
                _characterPosition.X--;
            if (Input.IsPressedOnce(Keys.D, keyboardState))
                _characterPosition.X++;
            if (Input.IsPressedOnce(Keys.W, keyboardState))
                _characterPosition.Y--;
            if (Input.IsPressedOnce(Keys.S, keyboardState))
                _characterPosition.Y++;

            ClampPosition();
        }

        void DrawTileImage(float x, float y, SpriteBatch spriteBatch)
        {
            Vector2 pos = new Vector2((x - y) * ((TILE_WIDTH) / 2), (x + y) * TILE_HEIGHT / 2);
            spriteBatch.Draw(_tileTexture, pos, null, Color.Black, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 0);
        }

        void DrawCharacter(float x, float y, SpriteBatch spriteBatch)
        {
            x += 1;
            Vector2 pos = new Vector2((x - y) * ((TILE_WIDTH) / 2), (x + y) * TILE_HEIGHT / 2);
            spriteBatch.Draw(_circleTexture, pos, null, Color.White, 0, GameConstants.circleOrigin, Vector2.One / 2, SpriteEffects.None, 0);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(transformMatrix: Matrix.CreateTranslation(new Vector3(400, 0, 0)));
            for (int x = 0; x < TILE_GRID_SIZE && x < _heights.Length; x++)
            {
                for (int y = 0; y < TILE_GRID_SIZE; y++)
                {
                    DrawTileImage(x, y, _spriteBatch);
                }
            }
            DrawCharacter(_characterPosition.X, _characterPosition.Y, _spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        private void ClampPosition()
        {
            _characterPosition.X = Math.Clamp(_characterPosition.X, 0, TILE_GRID_SIZE - 1);
            _characterPosition.Y = Math.Clamp(_characterPosition.Y, 0, TILE_GRID_SIZE - 1);
        }
    }
}