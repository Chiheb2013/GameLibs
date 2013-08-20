using System;
using System.Collections.Generic;

using SFML.Graphics;

using Mappy.Texture;

namespace Mappy.Maps
{
    public class LayeredWorld : World, IGameObject, IPhysicObject
    {
        List<Tile[]> layers;

        public int Layer { get; set; }

        IntRect IPhysicObject.Hitbox { get { return new IntRect(); } }

        public LayeredWorld(int width, int height)
            : base(width, height, null)
        {
            this.layers = new List<Tile[]>();
        }

        public void AddLayer(Tile[] layer)
        {
            layers.Add(layer);
        }

        public void InsertRenderObject(IRenderObject renderObject, Vector2D position)
        {
            int i = CoordinateSystemConverter.PlaneToLine(position, Width);
        }

        public new void Update(float deltaTime)
        {
            foreach (IGameObject[] layer in layers)
                foreach (IGameObject tile in layer)
                    tile.Update(deltaTime);
        } //TODO : collision with A center works, but it would be better if it
        //was with a bigger center, more of a circle, not just a point

        public bool Collides(Vector2D hitterPosition, int layer)
        {
            Vector2D worldPos = CoordinateSystemConverter.PixelsToWorld(hitterPosition);
            base.RaiseTeleportationEventIfAny(worldPos);

            int index = CoordinateSystemConverter.PlaneToLine(worldPos, Width);
            return layers[layer][index].Collides(hitterPosition) && !layers[layer][index].IsHollow;
        }

        public bool Collides(IPhysicObject hitter, int layer)
        {
            Layer = layer;
            return ((IPhysicObject)this).Collides(hitter);
        }

        bool IPhysicObject.Collides(IPhysicObject hitter)
        {
            Vector2D center = CollisionHelper.GetCenter(hitter.Hitbox);
            Vector2D worldPos = CoordinateSystemConverter.PixelsToWorld(center);

            base.RaiseTeleportationEventIfAny(worldPos);

            int index = CoordinateSystemConverter.PlaneToLine(worldPos, Width);
            return layers[Layer][index].Collides(hitter) && !layers[Layer][index].IsHollow;
        }

        public new void Render(RenderWindow renderWindow)
        {
            foreach (IGameObject[] layer in layers)
                foreach (IGameObject tile in layer)
                    tile.Render(renderWindow);
        }
    }
}
