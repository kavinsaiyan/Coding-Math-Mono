using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace CodingMath.Episodes
{
    /// <summary>
    /// Class used to draw the ship
    /// </summary>
    public class Ship
    {
        public readonly LineEpisode5[] _lines;
        private Matrix _translationMatrix;
        private Matrix _rotationMatrix;
        private Vector2 _thrustBase;
        private Vector2 _thrust;
        public Ship()
        {
            _lines = new LineEpisode5[3];
            _lines[0] = new LineEpisode5(new Vector2(-8, -8), new Vector2(30, 0));
            _lines[1] = new LineEpisode5(_lines[0].end, new Vector2(-8, 8));
            _lines[2] = new LineEpisode5(_lines[1].end, _lines[0].start);
            _thrustBase = (_lines[2].start + _lines[2].end) / 2;
            _translationMatrix = Matrix.Identity;
            _rotationMatrix = Matrix.CreateRotationZ(0);
        }

        public void SetPosition(Vector3 position)
        {
            _translationMatrix = Matrix.CreateTranslation(position);
        }

        public void SetRotation(float angle)
        {
            _rotationMatrix = Matrix.CreateRotationZ(angle);
        }

        public void SetThrust(Vector2 thrust)
        {
            _thrust = thrust.NormalizedCopy() * 4;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Matrix transformMatrix = _rotationMatrix * _translationMatrix;
            for (int i = 0; i < _lines.Length; i++)
            {
                Vector2 tempStart = Vector2.Transform(_lines[i].start, transformMatrix);
                Vector2 tempEnd = Vector2.Transform(_lines[i].end, transformMatrix);
                spriteBatch.DrawLine(tempStart, tempEnd, Color.Black, 1, 0);
            }
            Vector2 tempBase = Vector2.Transform(_thrustBase, transformMatrix);
            spriteBatch.DrawLine(tempBase, tempBase - _thrust, Color.Red, 4, 0);
        }
    }
}