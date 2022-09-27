using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace CodingMath
{
    class Line
    {
        private Vector2 _startPoint;
        private Vector2 _endPoint;

        public Vector2 StartPoint { get => _startPoint; set => _startPoint = value; }
        public Vector2 EndPoint { get => _endPoint; set => _endPoint = value; }

        public float StartPointX { get => _startPoint.X; set => _startPoint.X = value; }
        public float StartPointY { get => _startPoint.Y; set => _startPoint.Y = value; }
        public float EndPointX { get => _endPoint.X; set => _endPoint.X = value; }
        public float EndPointY { get => _endPoint.Y; set => _endPoint.Y = value; }

        public Line(Vector2 startPoint, Vector2 endPoint)
        {
            this._startPoint = startPoint;
            this._endPoint = endPoint;
        }

        public void UpdateEndPoints(Vector2 startPoint, Vector2 endPoint)
        {
            _startPoint = startPoint;
            _endPoint = endPoint;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawLine(_startPoint, _endPoint, Color.Black, 1, 0);
        }

        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            spriteBatch.DrawLine(_startPoint, _endPoint, color, 1, 0);
        }

        public StandardForm GetStandardForm()
        {
            StandardForm res = new StandardForm();
            res.A = _endPoint.Y - _startPoint.Y;
            res.B = _startPoint.X - _endPoint.X;
            res.C = res.A * _startPoint.X + res.B * _startPoint.Y;
            return res;
        }

        public bool LineIntersection(Line otherLine, out Vector2 intersection)
        {
            intersection = new Vector2();
            Line.StandardForm standardFormLine1 = GetStandardForm();
            Line.StandardForm standardFormLine2 = otherLine.GetStandardForm();
            float denominator = standardFormLine1.A * standardFormLine2.B - standardFormLine2.A * standardFormLine1.B;
            if (denominator == 0) //means parallel or colinear lines
                return false;
            intersection.X = (standardFormLine2.B * standardFormLine1.C - standardFormLine1.B * standardFormLine2.C) / denominator;
            intersection.Y = (standardFormLine1.A * standardFormLine2.C - standardFormLine2.A * standardFormLine1.C) / denominator;

            float rx0 = (intersection.X - StartPoint.X) / (EndPoint.X - StartPoint.X);
            float ry0 = (intersection.Y - StartPoint.Y) / (EndPoint.Y - StartPoint.Y);
            float rx1 = (intersection.X - otherLine.StartPoint.X) / (otherLine.EndPoint.X - otherLine.StartPoint.X);
            float ry1 = (intersection.Y - otherLine.StartPoint.Y) / (otherLine.EndPoint.Y - otherLine.StartPoint.Y);

            if (((rx0 >= 0 && rx0 <= 1) || (ry0 >= 0 && ry0 <= 1))
                && ((rx1 >= 0 && rx1 <= 1) || (ry1 >= 0 && ry1 <= 1)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public struct StandardForm
        {
            public float A;
            public float B;
            public float C;
        }
    }
}