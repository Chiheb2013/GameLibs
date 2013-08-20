using System;
using System.Collections.Generic;

using SFML.Graphics;

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
        public IntRect Hitbox { get { return new IntRect(); } } //This won't be used. Just to respect IPhysicsObject.
        public Vector2D Position { get { return Vector2D.Zero; } } //Won't be used. For IPhysicOject.

        public Vector2D Center { get { return Vector2D.Zero; } }

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

        public bool IsInBounds(Vector2D hitterPosition)
        {
            if (hitterPosition.X < 0 || hitterPosition.Y < 0
                || hitterPosition.X >= width || hitterPosition.Y >= height)
                return false;
            return true;
        }

        public bool Collides(Vector2D hitterScreenPosition)
        {
            Vector2D worldPos = CoordinateSystemConverter.PixelsToWorld(hitterScreenPosition);
            RaiseTeleportationEventIfAny(worldPos);

            int tileIndex = (int)(worldPos.Y * width * worldPos.X);
            return tiles[tileIndex].Collides(hitterScreenPosition) && !tiles[tileIndex].IsHollow;
        }

        bool IPhysicObject.Collides(IPhysicObject hitter)
        {
            Vector2D worldPos = CoordinateSystemConverter.PixelsToWorld(hitter.Position);
            RaiseTeleportationEventIfAny(worldPos);

            int index = CoordinateSystemConverter.PlaneToLine(worldPos, Width);
            return tiles[index].Collides(hitter) && !tiles[index].IsHollow;
        }

        protected void RaiseTeleportationEventIfAny(Vector2D hitterPosition)
        {
            foreach (Teleporter teleporter in teleporters)
                teleporter.Collides(hitterPosition);
        }
     
        public void Render(RenderWindow renderWindow)
        {
            foreach (IGameObject tile in tiles)
                tile.Render(renderWindow);
        }

        private void teleporter_OnTeleportation(object sender, EventArgs e)
        {
            if (OnTeleportation != null)
                teleporter_OnTeleportation(this, e);
        }
    }
}
