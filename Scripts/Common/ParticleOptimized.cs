using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace CodingMath
{
    public class ParticleOptimized
    {
        public float positionX, positionY;
        public float velocityX, velocityY;
        public float gravity = 0;
        public float friction = 1;
        private Texture2D texture2D;
        public float mass = 1;
        public Vector2 size;
        public Vector2 scale = Vector2.One;
        private Color color;
        public int TextureWidth => texture2D.Width;
        public int TextureHeight => texture2D.Height;
        public float Radius => TextureWidth / 2 * scale.X;
        private Vector2 Origin => new Vector2(size.X * scale.X, size.Y * scale.Y) / 2;
        public ParticleOptimized(ContentManager contentManager, string textureFileName = GameConstants.CIRCLE_TEXTURE_PATH)
        {
            texture2D = contentManager.Load<Texture2D>(textureFileName);
            size = new Vector2(texture2D.Width, texture2D.Height);
            color = Color.Black;
        }

        public void Update()
        {
            positionX += velocityX;
            positionY += velocityY;
            velocityY += gravity;
            velocityX *= friction;
            velocityY *= friction;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture2D, new Vector2(positionX, positionY), null, color, 0, Origin, scale, SpriteEffects.None, 0);
        }

        public void SetColor(Color color)
        {
            this.color = color;
        }

        public void GravitateTo(ParticleOptimized other)
        {
            // f = (G * M )/ r * r;
            // f is force
            // G is constant(can be ignored)
            // M is mass of the bigger object
            // r is distance between them
            float dx = other.positionX - positionX,
                    dy = other.positionY - positionY,
                    distSQ = dx * dx + dy * dy,
                    dist = System.MathF.Sqrt(distSQ),
                    force = other.mass / distSQ,
                    ax = dx / (dist * force),
                    ay = dy / (dist * force);

            velocityX += ax;
            velocityY += ay;
        }

        public void SpringTo(Vector2 position, float length)
        {
            float dx = position.X - positionX,
                dy = position.Y - positionY,
                distance = MathF.Sqrt(dx * dx + dy * dy),
                springForce = (distance - length) * GameConstants.SPRING_CONSTANT;
            velocityX += dx / distance * springForce;
            velocityY += dy / distance * springForce;
        }
    }
}