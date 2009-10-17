using System;

namespace KlimaKonflikt
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (KlimaKonfliktGame game = new KlimaKonfliktGame())
            {
                game.Run();
            }
        }
    }
}

