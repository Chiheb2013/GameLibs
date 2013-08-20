using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Mappy.Texture;

namespace Mappy.Maps
{
    public class LayeredWorldLoader : IWorldLoader
    {
        string worldPath;
        string[] lines;

        int width;
        int height;

        int teleportationIndex;

        List<Tile[]> layers;
        Dictionary<int, bool> hollowLinkage;
        Dictionary<int, string> linkage;

        public int Width { get { return width; } }
        public int Height { get { return height; } }

        public LayeredWorldLoader()
        {
            this.layers = new List<Tile[]>();
            this.linkage = new Dictionary<int, string>();
            this.hollowLinkage = new Dictionary<int, bool>();
        }

        public LayeredWorldLoader(string path)
        {
            this.worldPath = path;

            this.layers = new List<Tile[]>();
            this.linkage = new Dictionary<int, string>();
            this.hollowLinkage = new Dictionary<int, bool>();
        }

        public World Load(string path)
        {
            this.worldPath = path;
            return Load();
        }

        public LayeredWorld LoadLayered(string path)
        {
            return (LayeredWorld)Load(path);
        }

        public World Load()
        {
            GetLines();
            GetWorldSize();
            GetTextureMap();
            GetTeleportationIndex();
            return CreateWorld();
        }

        private void GetLines()
        {
            lines = StringHelper.GetCleanLines(worldPath);
        }

        private void GetWorldSize()
        {
            int widthIndex = StringHelper.GetSymbolIndex("width:", lines);
            int heightIndex = StringHelper.GetSymbolIndex("height:", lines);

            if (widthIndex != -1 && heightIndex != -1)
            {
                width = int.Parse(lines[widthIndex].Split(':')[1]);
                height = int.Parse(lines[heightIndex].Split(':')[1]);
            }
            else throw new ArgumentException("Size indexes not found. source:LayeredWorldLoader.GetWorldSize()");
        }

        private void GetTextureMap()
        {
            foreach (string line in lines)
                if (line.StartsWith("link:"))
                    AddTextureLink(line);
        }

        private void AddTextureLink(string line)
        {
            string[] parts = line.Replace("link:", "").Split(',');

            string textureName = parts[0];
            bool isHollow = parts[2] == "h";
            int id = int.Parse(parts[1]);

            if (!linkage.Keys.Contains(id))
                linkage.Add(id, textureName);
            if (!hollowLinkage.Keys.Contains(id))
                hollowLinkage.Add(id, isHollow);
        }

        private void GetTeleportationIndex()
        {
            teleportationIndex = StringHelper.GetSymbolIndex("teleportation_layer:", lines);
        }

        private LayeredWorld CreateWorld()
        {
            GetLayers();

            LayeredWorld world = GetWorld();
            SetPlayerPosition(world);

            if (teleportationIndex != -1)
                SetWorldTeleporters(world);

            return world;
        }

        private void GetLayers()
        {
            List<int> layersIndexes = GetLayersIndexes();
            List<Tile> layer = new List<Tile>();

            foreach (int index in layersIndexes)
            {
                for (int y = 0; y < height; y++)
                {
                    string[] ids = lines[y + index + 1].Split(' ');
                    for (int x = 0; x < width; x++)
                        layer.Add(GetTile(ids[x], x, y));
                }
                layers.Add(layer.ToArray());
                layer.Clear();
            }
        }

        private Tile GetTile(string id, int x, int y)
        {
            int iid = int.Parse(id);

            return RightTileCreator.GetRightTile(linkage[iid], new Vector2D(x, y),
                hollowLinkage[iid]);
        }

        private LayeredWorld GetWorld()
        {
            LayeredWorld world = new LayeredWorld(width, height);
            foreach (Tile[] layer in layers)
                world.AddLayer(layer);
            return world;
        }

        private void SetPlayerPosition(LayeredWorld world)
        {
            int playerX = int.Parse(lines[StringHelper.GetSymbolIndex("player_x:", lines)].Split(':')[1]);
            int playerY = int.Parse(lines[StringHelper.GetSymbolIndex("player_y:", lines)].Split(':')[1]);

            world.PlayerStartPosition = new Vector2D(playerX, playerY);
        }

        private void SetWorldTeleporters(LayeredWorld world)
        {
            int x;
            int y;
            string target;
            string texture;
            string[] parts;

            for (int i = teleportationIndex + 1; i < lines.Length; i++)
            {
                parts = lines[i].Split(':', ',');
                texture = parts[0];
                x = int.Parse(parts[1]);
                y = int.Parse(parts[2]);
                target = parts[3];

                Teleporter teleporter = new Teleporter(new Vector2D(x, y), target, texture);
                world.AddTeleporter(teleporter);
            }
        }

        private List<int> GetLayersIndexes()
        {
            int lastIndex = 0;
            int nextIndex = 0;
            List<int> indexes = new List<int>();

            while ((nextIndex = StringHelper.GetNextSymbol(lastIndex, "graphics_layer:", lines)) != -1)
            {
                indexes.Add(nextIndex);
                lastIndex = nextIndex + 1;
            }
            return indexes;
        }
    }
}
