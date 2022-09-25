using System;
using Microsoft.Xna.Framework;

namespace CodingMath
{
    class ModelVertex
    {
        public Vector3 position;
        public Vector2 screenPosition;


        public ModelVertex(float x, float y, float z)
        {
            position = new Vector3(x, y, z);
            UpdateScreenPosition();
        }

        void UpdateScreenPosition()
        {
            float perspective = GameConstants.FOCAL_LENGTH / (GameConstants.FOCAL_LENGTH + position.Z + GameConstants.CENTERZ);
            screenPosition.X = position.X * perspective;
            screenPosition.Y = position.Y * perspective;
        }

        public void TranslateModel(Vector3 translation)
        {
            position += translation;
            UpdateScreenPosition();
        }

        void RotateX(float angle)
        {
            float cos = MathF.Cos(angle);
            float sin = MathF.Sin(angle);
            position.Y = position.Y * cos - position.Z * sin;
            position.Z = position.Z * cos + position.Y * sin;
        }

        void RotateY(float angle)
        {
            float cos = MathF.Cos(angle);
            float sin = MathF.Sin(angle);
            position.X = position.X * cos - position.Z * sin;
            position.Z = position.Z * cos + position.X * sin;
        }

        void RotateZ(float angle)
        {
            float cos = MathF.Cos(angle);
            float sin = MathF.Sin(angle);
            position.X = position.X * cos - position.Y * sin;
            position.Y = position.Y * cos + position.X * sin;
        }

        public void Rotate(Vector3 rotation)
        {
            RotateX(rotation.X);
            RotateY(rotation.Y);
            RotateZ(rotation.Z);
            UpdateScreenPosition();
        }
    }
}