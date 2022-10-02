using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace CodingMath
{
    class VerletIntegrationPoint
    {
        private const float FRICTION = 0.95f;
        private const float BOUNCINESS = 0.90f;
        private const float GRAVITY = 0.5f;
        public Vector2 position;
        public Vector2 oldPosition;
        private int _screenWidth;
        private int _screenHeight;

        public VerletIntegrationPoint(int screenWidth, int screenHeight)
        {
            _screenWidth = screenWidth;
            _screenHeight = screenHeight;
        }

        public void Update()
        {
            Vector2 velocity = new Vector2();
            velocity.X = (position.X - oldPosition.X) * FRICTION;
            velocity.Y = (position.Y - oldPosition.Y) * FRICTION;

            oldPosition = position;
            position += velocity;
            position.Y += GRAVITY;
        }

        public void ConstraintPoint()
        {
            Vector2 velocity = new Vector2();
            velocity.X = (position.X - oldPosition.X) * FRICTION;
            velocity.Y = (position.Y - oldPosition.Y) * FRICTION;
            //bounds check
            if (position.X > _screenWidth)
            {
                position.X = _screenWidth;
                oldPosition.X = position.X + velocity.X * BOUNCINESS;
            }
            else if (position.X < 0)
            {
                position.X = 0;
                oldPosition.X = position.X + velocity.X * BOUNCINESS;
            }

            if (position.Y > _screenHeight)
            {
                position.Y = _screenHeight;
                oldPosition.Y = position.Y + velocity.Y * BOUNCINESS;
            }
            else if (position.Y < 0)
            {
                position.Y = 0;
                oldPosition.Y = position.Y + velocity.Y * BOUNCINESS;
            }
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D texture2D)
        {
            spriteBatch.Draw(texture2D, position, null, Color.Black, 0, GameConstants.circleOrigin, Vector2.One, SpriteEffects.None, 0);
        }

        public override string ToString()
        {
            string res = string.Empty;
            res += "Position : " + position + "\n";
            res += "Old Position : " + oldPosition + "\n";
            return res;
        }
    }
}