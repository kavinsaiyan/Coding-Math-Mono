using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace CodingMath
{
    public class Particle
    {
        public Vector2 position;
        public Vector2 velocity;
        public Vector2 acceleration { get; set; }
        public Vector2 gravity = new Vector2(0, 0f);
        public float friction;
        private Texture2D texture2D;
        public float mass = 1;
        private Vector2 size;
        public Vector2 Size
        {
            get => size;
            set => size = value;
        }
        public Vector2 Scale = Vector2.One;
        private Color color;
        public int TextureWidth => texture2D.Width;
        public int TextureHeight => texture2D.Height;
        public float Radius => TextureWidth / 2 * Scale.X;
        private Vector2 Origin => new Vector2(size.X * Scale.X, size.Y * Scale.Y) / 2;
        public Particle(ContentManager contentManager, string textureFileName = GameConstants.CIRCLE_TEXTURE_PATH)
        {
            velocity = Vector2.Zero;
            acceleration = Vector2.Zero;
            texture2D = contentManager.Load<Texture2D>(textureFileName);
            size = new Vector2(texture2D.Width, texture2D.Height);
            friction = 1;
            color = Color.Black;
        }

        public void SetColor(Color color)
        {
            this.color = color;
        }

        public void Update()
        {
            position += velocity;
            velocity += acceleration + gravity;
            velocity *= friction;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture2D, position, null, color, 0, Origin, Scale, SpriteEffects.None, 0);
        }

        public void DrawWithoutOffset(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture2D, position, null, color, 0, Vector2.Zero, Scale, SpriteEffects.None, 0);
        }

        public void GravitateTo(Particle other)
        {
            // f = (G * M )/ r * r;
            // f is force
            // G is constant(can be ignored)
            // M is mass of the bigger object
            // r is distance between them
            Vector2 gravity = new Vector2();
            Vector2 angleTo = other.position - position;
            gravity.SetLength((other.mass) / (angleTo.GetLengthSquared()));
            gravity.SetAngle(angleTo.GetAngle());
            velocity += gravity;
        }

        public void SpringTo(Particle other, float springLength, float sprintConstant = GameConstants.SPRING_CONSTANT)
        {
            //formula -> F = k * d;
            //  F = force
            //  k = spring constant
            //  d = distacne between the ends of the spring
            Vector2 distane = position - other.position;
            distane.SetLength(distane.GetLength() - springLength);
            other.velocity += sprintConstant * distane;
            velocity -= sprintConstant * distane;
        }

        public Color[] GetColors(Color[] pColors = null)
        {
            Color[] colors = pColors ?? new Color[texture2D.Width * texture2D.Height];
            texture2D.GetData<Color>(colors);
            return colors;
        }
    }
}