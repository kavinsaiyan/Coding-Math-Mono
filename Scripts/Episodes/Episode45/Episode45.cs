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
    public class Episode45 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private IKSystem ikSysten;
        public Episode45()
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
            ikSysten = new IKSystem(200, 200);

            ikSysten.AddArm(100);
            ikSysten.AddArm(80);
            ikSysten.AddArm(60);
        }

        protected override void Update(GameTime gameTime)
        {
            // if (Input.IsPressedOnce(Keys.Space, Keyboard.GetState()))
            {
                // arm.PointAt(Mouse.GetState().Position.X, Mouse.GetState().Position.Y);
                Point position = Mouse.GetState().Position;
                ikSysten.Drag(position.X, position.Y);
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            ikSysten.Draw(_spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        public class Arm
        {
            public float x;
            public float y;
            public float angle;
            public Arm parent;
            public readonly float length;

            public Arm(float length)
            {
                this.length = length;
            }

            public float GetEndX()
            {
                return x + MathF.Cos(angle) * length;
            }
            public float GetEndY()
            {
                return y + MathF.Sin(angle) * length;
            }

            public void PointAt(float x, float y)
            {
                float dx = x - this.x;
                float dy = y - this.y;
                angle = System.MathF.Atan2(dy, dx);
            }

            public void Drag(float x, float y)
            {
                PointAt(x, y);
                this.x = x - MathF.Cos(angle) * length;
                this.y = y - MathF.Sin(angle) * length;
                if (parent != null)
                {
                    parent.Drag(this.x, this.y);
                }
            }

            public void Draw(SpriteBatch spriteBatch)
            {
                spriteBatch.DrawLine(x, y, GetEndX(), GetEndY(), Color.Black, 1, 0);
            }
        }

        public class IKSystem

        {
            private Arm _lastArm;
            private List<Arm> _arms;
            private float _x = 0;
            private float _y = 0;

            public IKSystem(float x, float y)
            {
                _x = x;
                _y = y;
                _arms = new List<Arm>();
                _lastArm = null;
            }

            public void AddArm(float length)
            {
                Arm arm = new Arm(length);
                arm.parent = _lastArm;
                _lastArm = arm;
                _arms.Add(arm);
                Update();
            }

            public void Update()
            {
                //forward loop in the IKSystem
                for (int i = 0; i < _arms.Count; i++)
                {
                    if (_arms[i].parent != null)
                    {
                        _arms[i].x = _arms[i].parent.GetEndX();
                        _arms[i].y = _arms[i].parent.GetEndY();
                    }
                    else
                    {
                        _arms[i].x = _x;
                        _arms[i].y = _y;
                    }
                }
            }

            public void Draw(SpriteBatch spriteBatch)
            {
                for (int i = 0; i < _arms.Count; i++)
                {
                    _arms[i].Draw(spriteBatch);
                }
            }

            public void RotateArm(int index, float angle)
            {
                _arms[index].angle = angle;
            }

            public void Drag(float x, float y)
            {
                for (int i = 0; i < 2; i++)
                {
                    //backward loop in the IKSystem
                    //tracing the position and angle from the given x and y
                    _lastArm.Drag(x, y);
                    Update();
                }
            }
        }
    }
}