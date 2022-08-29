using System;

namespace CodingMath.Episodes
{
    public class Vector
    {
        private float _x;
        public float X { get => _x; set => _x = value; }
        private float _y;
        public float Y { get => _y; set => _y = value; }
        public float GetAngle()
        {
            float x = MathF.Atan2(Y, X);
            float radians;
            if (x > 0)
            {
                radians = x;
            }
            else
            {
                radians = 2 * System.MathF.PI + x;
            }
            return radians;
        }

        public void SetAngle(float angle)
        {
            X = MathF.Cos(angle) * GetLength();
            Y = MathF.Sin(angle) * GetLength();
        }

        public void SetLength(float length)
        {
            float angle = GetAngle();
            X = MathF.Cos(angle) * length;
            Y = MathF.Sin(angle) * length;
        }

        public float GetLength()
        {
            return MathF.Sqrt(X * X + Y * Y);
        }

        public float GetLengthSquared()
        {
            return X * X + Y * Y;
        }
    }
}