using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
namespace CodingMath.Utils
{
    public static class UtilityFunctions
    {
        private static Texture2D singlePixel;
        public static void MultiCurve(Vector2[] points,
            SpriteBatch spriteBatch,
            GraphicsDevice graphicsDevice)
        {
            if (points.Length <= 4) return;

            //first draw those three points 
            DrawQuadraticeLineTo(points[0], points[1], (points[1] + points[2]) / 2);

            //loop remaining points
            Vector2 p0, p1, p2;
            for (int i = 2; i < points.Length - 2; i += 1)
            {
                p0 = points[i - 1];
                p1 = points[i];
                p2 = points[i + 1];

                DrawQuadraticeLineTo((p0 + p1) / 2, p1, (p1 + p2) / 2);
            }
            //draw last three points
            p0 = points[points.Length - 3];
            p1 = points[points.Length - 2];
            p2 = points[points.Length - 1];
            DrawQuadraticeLineTo((p0 + p1) / 2, p1, p2);

            void DrawQuadraticeLineTo(Vector2 p0, Vector2 p1, Vector2 p2)
            {
                for (float i = 0; i < 1; i += 0.01f)
                {
                    Vector2 position = CommonFunctions.QuadraticBezier(p0, p1, p2, i);
                    DrawPixel(position, spriteBatch, graphicsDevice);
                }
            }
        }

        public static void DrawPixel(Vector2 position, SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {
            singlePixel = singlePixel ?? GetRectangleTexture(1, 1, Color.Black, graphicsDevice);
            spriteBatch.Draw(singlePixel, position, null, Color.White, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 0);
        }

        public static Texture2D GetRectangleTexture(int width, int height, Color color, GraphicsDevice graphicsDevice)
        {
            Texture2D rect = new Texture2D(graphicsDevice, width, height);

            Color[] data = new Color[width * height];
            for (int i = 0; i < data.Length; ++i) data[i] = color;
            rect.SetData(data);

            return rect;
        }
    }
}