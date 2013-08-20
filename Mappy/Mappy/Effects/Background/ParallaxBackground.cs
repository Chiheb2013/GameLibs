using System.Collections.Generic;

using SFML.Graphics;

namespace Mappy.Effects.Background
{
    public class ParallaxBackground : IGameObject
    {
        Vector2D defaultPosition;
        Vector2D defaultPush;

        List<Background> layers;

        public Vector2D DefaultPush { get { return defaultPush; } }
        public Vector2D DefaultPosition { get { return defaultPosition; } }

        public List<Background> Layers { get { return layers; } }

        public ParallaxBackground(Vector2D defaultPosition, Vector2D defaultPush)
        {
            this.defaultPush = defaultPush;
            this.defaultPosition = defaultPosition;

            this.layers = new List<Background>();
        }

        public ParallaxBackground(List<Background> backgrounds)
        {
            this.layers = backgrounds;
        }

        public void AddLayer(string texture)
        {
            AddLayer(texture, defaultPush, defaultPosition);
        }

        public void AddLayer(string texture, Vector2D push, Vector2D position)
        {
            Background layer = new Background(texture, position, push);
            AddLayer(layer);
        }

        public void AddLayer(Background layer)
        {
            layers.Add(layer);
        }

        public void Scale(float factor)
        {
            foreach (Background layer in layers)
                layer.Scale(factor);
        }

        public void Update(float deltaTime)
        {
            foreach (Background background in layers)
                background.Update(deltaTime);
        }

        public void Render(RenderWindow renderWindow)
        {
            foreach (Background background in layers)
                background.Render(renderWindow);
        }
    }
}
