using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using System;
namespace CodingMath.Episodes
{
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
}