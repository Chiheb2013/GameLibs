using System;
using System.IO;

namespace Mappy.Worlds
{
    public class GeneralWorldLoader
    {
        string worldPath;
        string[] lines;

        public GeneralWorldLoader()
        {
        }

        public GeneralWorldLoader(string worldPath)
        {
            this.worldPath = worldPath;
        }

        public World Load(string worldPath)
        {
            this.worldPath = worldPath;
            return Load();
        }

        public World Load()
        {
            GetLines();
            return GetWorldLoader().Load(worldPath);
        }

        private IWorldLoader GetWorldLoader()
        {
            int layeredIndex = StringHelper.GetSymbolIndex("layered:", lines);

            if (layeredIndex != -1)
            {
                if (lines[layeredIndex] == "layered:yes")
                    return new LayeredWorldLoader();
                else if (lines[layeredIndex] == "layered:no")
                    return new WorldLoader();
                else throw new ArgumentException("Uncorrect layer property. source:GeneralWorldLoader.GetWorldLoader()");
            }
            else throw new ArgumentException("Layered index not found. source:GeneralWorldLoader.GetWorldLoader()");
        }

        private void GetLines()
        {
            lines = StringHelper.GetCleanLines(worldPath);
        }
    }
}
