using System;
using System.Collections.Generic;

using SFML.Graphics;

using Mappy.Collisions;

namespace Mappy.Maps
{
    public class World : IGameObject, IPhysicObject
    {
        public event EventHandler OnTeleportation;

        int width;
        int height;

        Vector2D playerStartPosition;

        Tile[] tiles;
        protected List<Teleporter> teleporters;

        public int Width { get { return width; } }
        public int Height { get { return height; } }

        public Vector2D PlayerStartPosition { get { return playerStartPosition; } set { playerStartPosition = value; } }

        public World()
        {
            this.teleporters = new List<Teleporter>();
            this.playerStartPosition = new Vector2D();
        }

        public World(int width, int height, Tile[] tiles)
        {
            this.tiles = tiles;
            this.width = width;
            this.height = height;
            this.teleporters = new List<Teleporter>();
            this.playerStartPosition = new Vector2D();
        }

        public void AddTeleporter(Teleporter newTeleporter)
        {
            newTeleporter.OnCollision += teleporter_OnTeleportation;
            
            teleporters.Add(newTeleporter);
        }

        public void Update(float deltaTime)
        {
            foreach (IGameObject tile in tiles)
                tile.Update(deltaTime);
        }

        // TODO : optimiz.
        // This has to be optimized
        //  - something like just checking the neighbouring tiles
        //
        public bool CollidesWith(IPhysicObject other)
        {
            foreach (Tile tile in tiles)
                if (!tile.IsHollow && tile.CollidesWith(other))
                    return true;
            return false;
        }

        public void Render(RenderWindow renderWindow)
        {
            foreach (IGameObject tile in tiles)
                tile.Render(renderWindow);
        }

        public bool IsInBounds(Vector2D otherPosition)
        {
            if (otherPosition.X < 0
                || otherPosition.Y < 0
                || otherPosition.X >= width
                || otherPosition.Y >= height)
                return false;
            return true;
        }
     
        private void teleporter_OnTeleportation(object sender, EventArgs e)
        {
            if (OnTeleportation != null)
                teleporter_OnTeleportation(this, e);
        }
    }
}
