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

namespace TestTopView
{
    class TopViewMapState : IState
    {
        View view;
        LayeredWorld world;
        Player player;

        public LayeredWorld World { get { return world; } }

        public TopViewMapState()
        {
            Initialize();
        }

        public void Initialize()
        {
            world = (LayeredWorld)WorldManager.Worlds["first_world"];

            view = new View(new Vector2f(100, 0), new Vector2f(50, 50));
            view.Zoom(10);

            player = new Player(world.PlayerStartPosition);

            Program.RenderWindow.KeyPressed += RenderWindow_KeyPressed;
        }

        public void Dispose()
        {
        }

        public void Update(float deltaTime)
        {
            RefreshWorldView();
            UpdateWorld(deltaTime);

            player.Update(deltaTime);
        }

        private void RenderWindow_KeyPressed(object sender, KeyEventArgs e)
        {
            Vector2D position = new Vector2D(view.Center.X, view.Center.Y);

            if (e.Code == Keyboard.Key.Q)
                position.X -= 10;
            if (e.Code == Keyboard.Key.D)
                position.X += 10;
            if (e.Code == Keyboard.Key.Z)
                position.Y -= 10;
            if (e.Code == Keyboard.Key.S)
                position.Y += 10;

            view.Center = position.InternalVector;
        }

        private void RefreshWorldView()
        {
            Program.RenderWindow.SetView(view);
        }

        private void UpdateWorld(float deltaTime)
        {
            world.Update(deltaTime);
        }

        public void Render(RenderWindow renderWindow)
        {
            world.Render(renderWindow);
            player.Render(renderWindow);
        }
    }
}
