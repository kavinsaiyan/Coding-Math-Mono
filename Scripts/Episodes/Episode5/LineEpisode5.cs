using Microsoft.Xna.Framework;
namespace CodingMath
{
    public readonly struct LineEpisode5
    {
        public readonly Vector2 start;
        public readonly Vector2 end;
        public LineEpisode5(Vector2 start, Vector2 end) => (this.start, this.end) = (start, end);
    }
}