using Microsoft.Xna.Framework;

namespace CodingMath.Utils
{
    public static class RectangleExtensions
    {
        public static string ToStringExtended(this Rectangle rectangle)
        {
            string res = "Rectangle:\n";
            res += "\t center: " + rectangle.Center + "\n";
            res += "\tTop :" + rectangle.Top + "\n";
            res += "\tBottom :" + rectangle.Bottom + "\n";
            res += "\tLeft :" + rectangle.Left + "\n";
            res += "\tRight :" + rectangle.Right + "\n";
            return res;
        }
    }
}