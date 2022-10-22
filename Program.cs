﻿using System;
using CodingMath.Episodes;
using CodingMath.Mini;

namespace CodingMath
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new Episode12BounceDVD())
                game.Run();
        }
    }
}