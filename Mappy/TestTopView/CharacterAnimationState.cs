using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Mappy;
using Mappy.States;
using Mappy.Texture;

namespace TestTopView
{
    class CharacterAnimationState : IState
    {
        Player player;

        public CharacterAnimationState()
        {
            Initialize();
        }

        public void Initialize()
        {
            player = new Player(new Vector2D(1, 1));
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
