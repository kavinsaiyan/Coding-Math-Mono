using Microsoft.Xna.Framework;
using System;
namespace CodingMath
{
    public static class Vector2Extension
    {
        public const float DEG2RAD = 0.0174533f;
        public static float GetAngle(this Vector2 vector2)
        {
            float x = MathF.Atan2(vector2.Y, vector2.X);
            float radians;
            if (x > 0) { radians = x; } else { radians = 2 * System.MathF.PI + x; }
            return radians;
        }

        public static void SetAngle(ref this Vector2 vector2, float angle)
        {
            float length = vector2.GetLength();
            vector2.X = MathF.Cos(angle) * length;
            vector2.Y = MathF.Sin(angle) * length;
        }

        public static void SetLength(ref this Vector2 vector2, float length)
        {
            float angle = vector2.GetAngle();
            vector2.X = MathF.Cos(angle) * length;
            vector2.Y = MathF.Sin(angle) * length;
        }

        public static float GetLength(this Vector2 vector2)
        {
            return MathF.Sqrt(vector2.X * vector2.X + vector2.Y * vector2.Y);
        }

        public static float GetLengthSquared(this Vector2 vector2)
        {
            return vector2.X * vector2.X + vector2.Y * vector2.Y;
        }
    }
}

