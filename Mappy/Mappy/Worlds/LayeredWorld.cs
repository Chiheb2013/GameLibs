using System;
using System.Collections.Generic;

using SFML.Graphics;

using Mappy.Textures;
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
            //This double loop doesn't please me, but I can't find something better.
            foreach (IGameObject[] layer in layers)
                foreach (IGameObject tile in layer)
                    tile.Update(deltaTime);
        }

        // TODO : optimiz.
        // This function too will have to be optimized.
        //

        // TODO : get this function to get the cell at correct position
        //          with hitbox coordinates, or some kind of use of vector coords.
        public new bool CollidesWith(IPhysicObject hitter)
        {
            foreach (Tile tile in layers[Layer])
                if (!tile.IsHollow && tile.CollidesWith(hitter))
                    return true;
            return false;
        }

        public new void Render(RenderWindow renderWindow)
        {
            foreach (IGameObject[] layer in layers)
                foreach (IGameObject tile in layer)
                    tile.Render(renderWindow);
        }
    }
}
