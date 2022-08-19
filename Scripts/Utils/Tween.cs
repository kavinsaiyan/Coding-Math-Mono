using System;

namespace CodingMath.Utils
{
    public class Tween
    {
        /// <summary>
        /// simple linear tweening - no easing
        /// t: current time, b: beginning value, c: change in value
        /// t and d can be in frames or seconds/milliseconds
        /// </summary>
        public static float LinearTween(float t, float b, float c)
        {
            return c * t + b;
        }

        /// <summary>
        /// quadratic easing in - accelerating from zero velocity
        /// t: current time, b: beginning value, c: change in value
        /// t and d can be in frames or seconds/milliseconds
        /// </summary>
        public static float EaseInQuad(float t, float b, float c)
        {
            return c * t * t + b;
        }

        /// <summary>
        /// quadratic easing out - decelerating to zero velocity
        /// t: current time, b: beginning value, c: change in value
        /// t and d can be in frames or seconds/milliseconds
        /// </summary>
        public static float EaseOutQuad(float t, float b, float c)
        {
            return -c * t * (t - 2) + b;
        }

        /// <summary>
        /// quadratic easing in/out - acceleration until halfway, then deceleration
        /// t: current time, b: beginning value, c: change in value
        /// t and d can be in frames or seconds/milliseconds
        /// </summary>
        public static float EaseInOutQuad(float t, float b, float c)
        {
            t = t * 2;
            if (t < 1) return c / 2 * t * t + b;
            return -c / 2 * ((--t) * (t - 2) - 1) + b;
        }
    }
}