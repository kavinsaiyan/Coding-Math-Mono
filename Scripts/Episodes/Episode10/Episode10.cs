using CodingMath.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using System;
using System.Collections.Generic;

namespace CodingMath.Episodes
{
    public class Episode10 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private int _width;
        private int _height;
        private Ship _ship;
        /// <summary>
        /// used for actually calculating movement and rotation of the ship
        /// </summary>
        private Particle _particle;
        private Vector2 _thrust;
        private float _angle = 0;

        public Episode10()
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
            _particle = new Particle(Content);
            _particle.friction = 0.95f;
            _ship = new Ship();
            _thrust = new Vector2();
            _angle = 0;
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.W))
                _thrust.SetLength(0.5f);
            else
                _thrust.SetLength(0);

            if (keyboardState.IsKeyDown(Keys.A))
                _angle += 0.1f;
            else if (keyboardState.IsKeyDown(Keys.D))
                _angle -= 0.1f;

            _ship.SetThrust(_thrust);
            _thrust.SetAngle(_angle);
            _ship.SetRotation(_angle);

            _particle.acceleration = _thrust;
            _particle.Update(gameTime);

            if (_particle.position.X < -_width / 2)
                _particle.position.X = _width / 2;
            if (_particle.position.X > _width / 2)
                _particle.position.X = -_width / 2;
            if (_particle.position.Y < -_height / 2)
                _particle.position.Y = _height / 2;
            if (_particle.position.Y > _height / 2)
                _particle.position.Y = -_height / 2;

            _ship.SetPosition(new Vector3(_particle.position, 0));

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(transformMatrix: Matrix.CreateTranslation(_width / 2, _height / 2, 0));
            // _spriteBatch.Begin();

            _ship.Draw(_spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        /// <summary>
        /// Class used to draw the ship
        /// </summary>
        class Ship
        {
            public readonly Line[] _lines;
            private Matrix _translationMatrix;
            private Matrix _rotationMatrix;
            private Vector2 _thrustBase;
            private Vector2 _thrust;
            public Ship()
            {
                _lines = new Line[3];
                _lines[0] = new Line(new Vector2(-8, -8), new Vector2(30, 0));
                _lines[1] = new Line(_lines[0].end, new Vector2(-8, 8));
                _lines[2] = new Line(_lines[1].end, _lines[0].start);
                _thrustBase = (_lines[2].start + _lines[2].end) / 2;
                _translationMatrix = Matrix.Identity;
                _rotationMatrix = Matrix.CreateRotationZ(0);
            }

            public void SetPosition(Vector3 position)
            {
                _translationMatrix = Matrix.CreateTranslation(position);
            }

            public void SetRotation(float angle)
            {
                _rotationMatrix = Matrix.CreateRotationZ(angle);
            }

            public void SetThrust(Vector2 thrust)
            {
                _thrust = thrust.NormalizedCopy() * 4;
            }

            public void Draw(SpriteBatch spriteBatch)
            {
                Matrix transformMatrix = _rotationMatrix * _translationMatrix;
                for (int i = 0; i < _lines.Length; i++)
                {
                    Vector2 tempStart = Vector2.Transform(_lines[i].start, transformMatrix);
                    Vector2 tempEnd = Vector2.Transform(_lines[i].end, transformMatrix);
                    spriteBatch.DrawLine(tempStart, tempEnd, Color.Black, 1, 0);
                }
                Vector2 tempBase = Vector2.Transform(_thrustBase, transformMatrix);
                spriteBatch.DrawLine(tempBase, tempBase - _thrust, Color.Red, 4, 0);
            }
        }
    }
}