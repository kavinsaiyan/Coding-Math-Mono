using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace CodingMath.InputSystem
{
    public static class Input
    {
        public static byte[] keyState = new byte[255];
        public static bool IsPressedOnce(Keys keyCode, KeyboardState keyboardState)
        {
            if (keyboardState.IsKeyUp(keyCode))
            {
                keyState[(int)keyCode] = 0;
            }
            else
            {
                if (keyState[(int)keyCode] == 0)
                {
                    keyState[(int)keyCode] = 1;
                    return true;
                }
                keyState[(int)keyCode] = 1;
            }
            return false;
        }
    }
}