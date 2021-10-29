using System;

namespace Iterex
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new Game1(1280, 720))
                game.Run();
        }
    }
}
