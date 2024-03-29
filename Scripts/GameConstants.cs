using Microsoft.Xna.Framework;

namespace CodingMath
{
    public class GameConstants
    {
        public const float SPRING_CONSTANT = 0.1f;
        public static readonly Vector2 circleOrigin = new Vector2(32, 32);
        internal const float CENTERZ = 300f;
        public const string FONT_PATH = "Font/ArialBlack";
        public const string CIRCLE_TEXTURE_PATH = "Circle64x64";
        public const string SQUARE_TEXTURE_PATH = "100x100_Square";
        public const string RECTANGLE_TEXTURE_PATH = "Rectangle_64x128";
        public const string STEERING_WHEEL_TEXTURE_PATH = "wheel";
        public const string DVD_LOGO_TEXTURE_PATH = "DVD_logo";
        public const float FOCAL_LENGTH = 300f;
    }
}