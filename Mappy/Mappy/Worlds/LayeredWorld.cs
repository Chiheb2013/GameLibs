using System;
using System.Collections.Generic;

using SFML.Graphics;

using Mappy.Textures;
using Mappy.Entities;
using Mappy.Collisions;

namespace Mappy.Worlds
{
    public class LayeredWorld : World, IPhysicObject
    {
        public int Layer;

        List<Tile[]> layers;

        public LayeredWorld(int width, int height)
            : base(width, height, null)
        {
            this.layers = new List<Tile[]>();
        }

        public void AddLayer(Tile[] layer)
        {
            layers.Add(layer);
        }

        public new void Update(float deltaTime)
        {
            foreach (IGameObject[] layer in layers)
                foreach (IGameObject tile in layer)
                    tile.Update(deltaTime);
        }

        // TODO : optimiz.
        // This function too will have to be optimized.
        //
        public override bool CollidesWith(IPhysicObject hitter)
        {
            foreach (Tile[] layer in layers)
                foreach (Tile tile in layer)
                    if (!tile.IsHollow && tile.CollidesWith(hitter)) return true;
            return false;
        }

        public override bool CollidesWith(IPhysicObject hitter, Direction direction)
        {
            PhysicsObject hit = (PhysicsObject)hitter;
            Vector2D dirVec = VectorHelper.GetDirectionVector(direction);

            int x = (int)(hit.Position.X / TextureManager.TextureSize.X + dirVec.X);
            int y = (int)(hit.Position.Y / TextureManager.TextureSize.Y + dirVec.Y);
            int index = CoordinateSystemConverter.PlaneToLine(new Vector2D(x, y), Width);

            Tile tile = layers[Layer][index];

            if (tile.IsHollow) return false;
            return tile.CollidesWith(hit);
        }

        public new void Render(RenderWindow renderWindow)
        {
            foreach (IGameObject[] layer in layers)
                foreach (IGameObject tile in layer)
                    tile.Render(renderWindow);
        }
    }
}
