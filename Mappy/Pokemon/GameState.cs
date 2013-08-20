using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.Window;
using SFML.Graphics;

using Mappy;
using Mappy.Maps;
using Mappy.States;
using Mappy.Texture;

namespace Pokemon
{
    class GameState : IState
    {
        LayeredWorld world;
        Player player;

        public LayeredWorld World { get { return world; } }

        public GameState()
        {
            Initialize();
        }

        public void Initialize()
        {
            world = (LayeredWorld)WorldManager.GetWorld("grassland");
            player = new Player(this);
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
