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
    public class Episode44 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private FKSysten _leg0;
        private FKSysten _leg1;
        public Episode44()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
            _leg0 = new FKSysten(200, 200, 0, 0.02f);
            _leg0.AddArm(100, MathF.PI / 2, MathF.PI / 4);
            _leg0.AddArm(80, 0.87f, 0.87f, -1.5f);

            _leg1 = new FKSysten(200, 200, MathF.PI, 0.02f);
            _leg1.AddArm(100, MathF.PI / 2, MathF.PI / 4);
            _leg1.AddArm(80, 0.87f, 0.87f, -1.5f);
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            // if (Input.IsPressedOnce(Keys.Space, Keyboard.GetState()))
            {
                _leg0.Update();
                _leg1.Update();
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            _leg0.Draw(_spriteBatch);
            _leg1.Draw(_spriteBatch);
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
            private readonly float _rotationRange;
            private readonly float _centerAngle;
            private readonly float _phaseOffset;

            public Arm(float length, float centerAngle, float rotationRange, float phaseOffset = 0)
            {
                _centerAngle = centerAngle;
                _rotationRange = rotationRange;
                this.length = length;
                _phaseOffset = phaseOffset;
            }

            public float GetEndX()
            {
                float tempAngle = angle;
                Arm head = parent;

                while (head != null)
                {
                    tempAngle += head.angle;
                    head = head.parent;
                }

                return x + MathF.Cos(tempAngle) * length;
            }
            public float GetEndY()
            {
                float tempAngle = angle;
                Arm head = parent;

                while (head != null)
                {
                    tempAngle += head.angle;
                    head = head.parent;
                }
                return y + MathF.Sin(tempAngle) * length;
            }

            public void SetPhase(float phase)
            {
                angle = _centerAngle + MathF.Sin(phase + _phaseOffset) * _rotationRange;
            }

            public void Draw(SpriteBatch spriteBatch)
            {
                spriteBatch.DrawLine(x, y, GetEndX(), GetEndY(), Color.Black, 1, 0);
            }
        }

        public class FKSysten
        {
            private Arm _lastArm;
            private List<Arm> _arms;
            private float _x = 0;
            private float _y = 0;
            private readonly float _speed;
            private float _phase;

            public FKSysten(float x, float y, float phase, float speed)
            {
                _phase = phase;
                _x = x;
                _y = y;
                _speed = speed;
                _arms = new List<Arm>();
                _lastArm = null;
            }

            public void AddArm(float length, float centerAngle, float rotationRange, float phaseOffset = 0)
            {
                Arm arm = new Arm(length, centerAngle, rotationRange, phaseOffset);
                arm.parent = _lastArm;
                _lastArm = arm;
                _arms.Add(arm);
                Update();
            }

            public void Update()
            {
                for (int i = 0; i < _arms.Count; i++)
                {
                    _arms[i].SetPhase(_phase);
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
                _phase += _speed;
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
        }
    }
}