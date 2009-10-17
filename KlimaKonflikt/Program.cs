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
            //LevelLoader.GetLevels(null, null, null, 3);
            using (KlimaKonfliktGame game = new KlimaKonfliktGame())
            {
                game.Run();
            }
        }
    }
}

