using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
namespace CodingMath
{
    public class DraggablePoint
    {
        private Texture2D _circleTexture;
        private Vector2 _position;
        public Vector2 Position { get => _position; }
        private Vector2 _scale;
        private float _radius;
        private bool _isDragging = false;

        public DraggablePoint(ContentManager content, Vector2 position, float radius)
        {
            _circleTexture = content.Load<Texture2D>(GameConstants.CIRCLE_TEXTURE_PATH);
            _position = position;
            _radius = radius;
            _scale = new Vector2((1 / (float)_circleTexture.Width) * radius, (1 / (float)_circleTexture.Height) * radius);
        }

        public void Update(GameTime gameTime, MouseState mouseState)
        {
            Vector2 mousePosition = mouseState.Position.ToVector2();
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                if ((mousePosition - _position).Length() < _radius)
                {
                    _isDragging = true;
                }
            }
            else
            {
                _isDragging = false;
            }

            if (_isDragging)
            {
                _position = mousePosition;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_circleTexture, _position, null, Color.Black, 0, GameConstants.circleOrigin,
            _scale, SpriteEffects.None, 0);
        }
    }
}