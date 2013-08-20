using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Mappy;
using Mappy.States;

namespace AnimationGroupTest
{
    class AnimationGroupState : IState
    {
        Player player;

        public AnimationGroupState()
        {
            Initialize();
        }

        public void Initialize()
        {
            player = new Player();
        }

        public void Dispose()
        {
        }

        public void Update(float deltaTime)
        {
            player.Update(deltaTime);
        }

        public void Render(SFML.Graphics.RenderWindow renderWindow)
        {
            player.Render(renderWindow);
        }
    }
}
