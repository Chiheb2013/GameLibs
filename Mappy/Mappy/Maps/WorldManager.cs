using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace Mappy.Maps
{
    public static class WorldManager
    {
        static GeneralWorldLoader loader = new GeneralWorldLoader();
        static Dictionary<string, World> worlds = new Dictionary<string, World>();

        public static Dictionary<string, World> Worlds { get { return worlds; } }

        public static void LoadWorlds(string worldsFile)
        {
            string[] parts;
            string[] lines = StringHelper.GetCleanLines(worldsFile);

            foreach (string line in lines)
            {
                parts = line.Split(':');
                AddWorld(parts[0], parts[1]);
            }
        }

        public static World GetWorld(string world)
        {
            ExceptionHelper.AssertIsInDictionnary<World>(worlds, world, "WorldManager.GetWorld()");
            return worlds[world];
        }

        public static void AddWorld(string name, string path)
        {
            World world = loader.Load(path);
            worlds.Add(name, world);
        }
    }
}
