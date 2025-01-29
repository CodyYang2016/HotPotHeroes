using CSE3902; // ✅ Ensure Game1 is accessible
using System;

namespace CSE3902
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new Game1()) // Game1
            {
                game.Run();
            }
        }
    }
}
