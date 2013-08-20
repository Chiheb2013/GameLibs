using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

using SFML.Window;
using SFML.Graphics;

namespace Mappy.Maps
{
    public class WorldLoader : IWorldLoader
    {
        string worldPath;
        string[] lines;

        int width;
        int height;

        int graphicsIndex;
        int teleportationIndex;

        List<Tile> tiles;
        Dictionary<int, bool> hollowLinkage;
        Dictionary<int, string> linkage;

        public int Width { get { return width; } }
        public int Height { get { return height; } }

        public WorldLoader()
        {
            this.tiles = new List<Tile>();
            this.linkage = new Dictionary<int, string>();
            this.hollowLinkage = new Dictionary<int, bool>();
        }

        public WorldLoader(string path)
        {
            this.worldPath = path;

            this.tiles = new List<Tile>();
            this.linkage = new Dictionary<int, string>();
            this.hollowLinkage = new Dictionary<int, bool>();
        }

        public World Load(string path)
        {
            this.worldPath = path;
            return Load();
        }

        public World Load()
        {
            GetLines();
            GetMapSize();
            GetTextureMap();
            GetIndexes();
            return CreateWorld();
        }

        private void GetLines()
        {
            lines = StringHelper.GetCleanLines(worldPath);
        }

        private void GetMapSize()
        {
            int widthIndex = StringHelper.GetSymbolIndex("width:", lines);
            int heightIndex = StringHelper.GetSymbolIndex("height:", lines);

            width = int.Parse(lines[widthIndex].Split(':')[1]);
            height = int.Parse(lines[heightIndex].Split(':')[1]);
        }

        private void GetTextureMap()
        {
            foreach (string line in lines)
                if (line.StartsWith("link:"))
                    AddLink(line);
        }

        private void AddLink(string line)
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

        private void GetIndexes()
        {
            GetGraphicsLayerIndex();
            GetTeleportationLayerIndex();
        }

        private void GetGraphicsLayerIndex()
        {
            graphicsIndex = StringHelper.GetSymbolIndex("graphics_layer:", lines);
        }

        private void GetTeleportationLayerIndex()
        {
            teleportationIndex = StringHelper.GetSymbolIndex("teleportation_layer:", lines);
        }
        
        private World CreateWorld()
        {
            GetTiles();

            World world = GetWorld();
            SetPlayerPosition(world);

            if (teleportationIndex != -1)
                SetWorldTeleporters(world);

            return world;
        }

        private void GetTiles()
        {
            for (int y = 0; y < height; y++)
            {
                string[] ids = lines[y + graphicsIndex + 1].Split(' ');
                for (int x = 0; x < width; x++)
                    tiles.Add(GetTile(ids[x], x, y));
            }
        }

        private Tile GetTile(string id, int x, int y)
        {
            int iid = int.Parse(id);

            return RightTileCreator.GetRightTile(linkage[iid], new Vector2D(x, y),
                hollowLinkage[iid]);
        }

        private World GetWorld()
        {
            return new World(width, height, tiles.ToArray());
        }

        private void SetPlayerPosition(World world)
        {
            int playerX = int.Parse(lines[StringHelper.GetSymbolIndex("player_x:", lines)].Split(':')[1]);
            int playerY = int.Parse(lines[StringHelper.GetSymbolIndex("player_y:", lines)].Split(':')[1]);

            world.PlayerStartPosition = new Vector2D(playerX, playerY);
        }

        private void SetWorldTeleporters(World world)
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
    }
}
