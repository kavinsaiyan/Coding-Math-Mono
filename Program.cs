using System;
using CodingMath.Episodes;

namespace CodingMath
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new Episode17PlanetaryRevolutionOptimized())
                game.Run();
        }
    }
}