using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Mappy;
using Mappy.Maps;
using Mappy.States;
using Mappy.Texture;
using Mappy.Texture.Animation;

using SFML.Window;
using SFML.Graphics;

namespace AnimatedTileTest
{
    class AnimatedTileTestState : IState
    {
        World world;

        public AnimatedTileTestState()
        {
            Initialize();
        }

        public void Initialize()
        {
            world = WorldManager.GetWorld("loke");
        }

        public void Dispose()
        {
        }

        public void Update(float deltaTime)
        {
            world.Update(deltaTime);
        }

        public void Render(RenderWindow renderWindow)
        {
            world.Render(renderWindow);
        }
    }
}
