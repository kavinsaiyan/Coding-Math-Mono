using System;

namespace CodingMath.Episodes
{
    public class Vector
    {
        private float _x;
        public float X { get => _x; set => _x = value; }
        private float _y;
        public float Y { get => _y; set => _y = value; }

        public Vector(float x, float y)
        {
            _x = x;
            _y = y;
        }
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

        public Vector Add(Vector other)
        {
            return new Vector(X + other.X, Y + other.Y);
        }

        public Vector Subtract(Vector other)
        {
            return new Vector(X - other.X, Y - other.Y);
        }

        public Vector Multiply(float val)
        {
            return new Vector(X * val, Y * val);
        }

        public Vector Divide(float val)
        {
            return new Vector(X / val, Y / val);
        }

        public void AddTo(Vector other)
        {
            _x += other.X;
            _y += other.Y;
        }

        public void SubtractFrom(Vector other)
        {
            _x -= other.X;
            _y -= other.Y;
        }

        public void MultiplytBy(float val)
        {
            _x *= val;
            _y *= val;
        }

        public void DivideBy(float val)
        {
            _x /= val;
            _y /= val;
        }

        public static Vector operator +(Vector a, Vector b)
        {
            return a.Add(b);
        }

        public static Vector operator -(Vector a, Vector b)
        {
            return a.Subtract(b);
        }

        public static Vector operator *(Vector a, float b)
        {
            return a.Multiply(b);
        }

        public static Vector operator /(Vector a, float b)
        {
            return a.Divide(b);
        }
    }
}