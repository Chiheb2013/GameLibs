using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Mappy;
using Mappy.States;
using Mappy.Textures;
using Mappy.Effects.Background;

namespace TestTopView
{
    class ParallaxScreenState : IState
    {
        ParallaxBackground parallax;

        public ParallaxScreenState()
        {
            Initialize();
        }

        public void Initialize()
        {
            parallax = new ParallaxBackground(new Vector2D(0, 0), new Vector2D(-0.5f, -0.3f));
            
            parallax.AddLayer("layer_1");
            parallax.AddLayer("layer_1", new Vector2D(-1,-0.6f), parallax.DefaultPosition);

            parallax.Scale(0.5f);
        }

        public void Dispose()
        {
        }

        public void Update(float deltaTime)
        {
            parallax.Update(deltaTime);
        }

        public void Render(SFML.Graphics.RenderWindow renderWindow)
        {
            parallax.Render(renderWindow);
        }
    }
}
