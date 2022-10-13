using Microsoft.Xna.Framework;

namespace CodingMath.Utils
{
    public static class CommonFunctions
    {
        public static readonly System.Random random = new System.Random(System.Guid.NewGuid().GetHashCode());
        public static float RandomRange(float min, float max)
        {
            return min + (float)random.NextDouble() * (max - min);
        }

        public static int RandomRangeInt(int min, int max)
        {
            return min + (int)(random.NextDouble() * (max - min));
        }

        public static float Remap(float value, float low1, float high1, float low2, float high2)
        {
            return low2 + (value - low1) * (high2 - low2) / (high1 - low1);
        }

        public static Vector2 QuadraticBezier(Vector2 p0, Vector2 p1, Vector2 p2, float t, Vector2? pF = null)
        {
            Vector2 pFinal = pF == null ? new Vector2() : (Vector2)pF;

            pFinal.X = System.MathF.Pow(1 - t, 2) * p0.X +
                        (1 - t) * 2 * t * p1.X +
                        t * t * p2.X;

            pFinal.Y = System.MathF.Pow(1 - t, 2) * p0.Y +
                                    (1 - t) * 2 * t * p1.Y +
                                    t * t * p2.Y;

            return pFinal;
        }

        public static Vector2 CubicBezier(Vector2 p0, Vector2 p1, Vector2 p2, Vector2 p3, float t, Vector2? pF = null)
        {
            Vector2 pFinal = pF == null ? new Vector2() : (Vector2)pF;

            pFinal.X = System.MathF.Pow(1 - t, 3) * p0.X +
                   System.MathF.Pow(1 - t, 2) * 3 * t * p1.X +
                   (1 - t) * 3 * t * t * p2.X +
                   t * t * t * p3.X;

            pFinal.Y = System.MathF.Pow(1 - t, 3) * p0.Y +
                   System.MathF.Pow(1 - t, 2) * 3 * t * p1.Y +
                   (1 - t) * 3 * t * t * p2.Y +
                   t * t * t * p3.Y;

            return pFinal;
        }

        public static float RoundToPlaces(float number, float places)
        {
            float mult = System.MathF.Pow(10, places);
            return System.MathF.Round(mult * number) / mult;
        }
        public static float RoundNearest(float number, float nearest)
        {
            return System.MathF.Round(number / nearest) * nearest;
        }
    }
}