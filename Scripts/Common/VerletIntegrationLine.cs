using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace CodingMath
{
    class VerletIntegrationLine
    {
        private VerletIntegrationPoint _point1;
        private VerletIntegrationPoint _point2;
        private float _length;

        public VerletIntegrationLine(VerletIntegrationPoint point1, VerletIntegrationPoint point2, float length)
        {
            _point1 = point1;
            _point2 = point2;
            _length = length;
        }

        public void Update()
        {
            float dx = _point2.position.X - _point1.position.X;
            float dy = _point2.position.Y - _point1.position.Y;
            float distance = System.MathF.Sqrt(dx * dx + dy * dy);
            float differnce = _length - distance;
            float percent = (differnce / distance) / 2;
            // Debug.Log("distance : " + distance + " differnce : " + differnce + " percent : " + percent + "\n dx : " + dx + " dy: " + dy);
            // Debug.LogWarning(_point1.ToString() + "\n" + _point2.ToString());
            _point1.position.X -= percent * dx;
            _point1.position.Y -= percent * dy;
            _point2.position.X += percent * dx;
            _point2.position.Y += percent * dy;
            // Debug.LogError(_point1.ToString() + "\n" + _point2.ToString());
        }

        public void Draw(SpriteBatch spriteBatch, bool drawPoints = false)
        {
            if (drawPoints)
            {
                spriteBatch.DrawCircle(_point1.position, 4, 12, Color.Black, 1, 0);
                spriteBatch.DrawCircle(_point2.position, 4, 12, Color.Black, 1, 0);
            }
            spriteBatch.DrawLine(_point1.position, _point2.position, Color.Black, 1, 0);
        }

        public float GetAngle()
        {
            float dx = _point2.position.X - _point1.position.X;
            float dy = _point2.position.Y - _point1.position.Y;
            return MathF.Atan2(dy, dx);
        }

        public Vector2 GetPoint1Position() => _point1.position;
    }
}