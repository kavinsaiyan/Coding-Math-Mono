using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
namespace CodingMath
{
    public class PerspectiveSquare
    {
        public Vector3 position;
        public Vector2 screenPosition;
        public Vector2 scale;

        private Vector2 origin;
        private Texture2D squareTexture;
        private Color color;

        private float angle;

        public PerspectiveSquare(Vector3 pos, Texture2D squareTexture, Color color, float angle = 0f)
        {
            this.color = color;
            this.position = pos;
            this.squareTexture = squareTexture;
            this.angle = angle;
            origin = new Vector2(squareTexture.Width / 2, squareTexture.Height / 2);
            UpdatesScreenPositionAndScale();
        }

        public void SetPosition(Vector3 position) => this.position = position;

        public void Update(Vector3 deltaPosition)
        {
            this.position += deltaPosition;
            UpdatesScreenPositionAndScale();
            ClampPosition();
        }

        public void UpdateAngle(float deltaAngle, float radius = 10f)
        {
            angle = deltaAngle + angle;
            float x = (MathF.Cos(angle)) * radius;
            float z = (MathF.Sin(angle)) * radius + radius;
            position.X = x;
            position.Z = z;
            UpdatesScreenPositionAndScale();
        }

        void ClampPosition()
        {
            if (position.Z > 600 || position.Z < -600f)
            {
                position.Z = 0f;
            }
        }

        public void UpdatesScreenPositionAndScale()
        {
            float perspective = (GameConstants.FOCAL_LENGTH) / (GameConstants.FOCAL_LENGTH + position.Z);
            screenPosition.X = perspective * position.X;
            screenPosition.Y = perspective * position.Y;
            scale.X = scale.Y = perspective;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(squareTexture, screenPosition, null, color, 0, origin, scale,
                SpriteEffects.None, 0);
        }
    }
}