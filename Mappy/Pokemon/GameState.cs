using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.Window;
using SFML.Graphics;

using Mappy;
using Mappy.Worlds;
using Mappy.States;
using Mappy.Textures;

namespace Pokemon
{
    class GameState : IState
    {
        LayeredWorld world;
        SecondPlayer player;

        public LayeredWorld World { get { return world; } }

        public GameState()
        {
            Initialize();
        }

        public void Initialize()
        {
            world = (LayeredWorld)WorldManager.GetWorld("grassland");
            player = new SecondPlayer(world);
        }

        public void Dispose()
        {
        }

        public void Update(float deltaTime)
        {
            world.Update(deltaTime);
            player.Update(deltaTime);
        }

        public void Render(RenderWindow renderWindow)
        {
            world.Render(renderWindow);
            player.Render(renderWindow);
        }
    }
}
