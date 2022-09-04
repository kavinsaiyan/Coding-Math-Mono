using Microsoft.Xna.Framework;
namespace CodingMath
{
    public readonly struct Line
    {
        public readonly Vector2 start;
        public readonly Vector2 end;
        public Line(Vector2 start, Vector2 end) => (this.start, this.end) = (start, end);
    }
}