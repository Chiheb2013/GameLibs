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
using Mappy.Texture.Animation;

namespace AnimatedTileTest
{
    class Program
    {
        static Clock clock;

        public static StateSystem StateSystem;
        public static RenderWindow RenderWindow;

        static void Main(string[] args)
        {
            CreateWindow();

            InitializeTextures();
            InitializeAnimations();
            InitializeWorlds();
            InitializeStates();
            CreateClock();

            GameLoop();
        }

        private static void CreateWindow()
        {
            RenderWindow = new RenderWindow(new VideoMode(800, 600), "Test animated tile class with animation group");

            RenderWindow.Closed += RenderWindow_Closed;
        }

        private static void RenderWindow_Closed(object sender, EventArgs e)
        {
            RenderWindow.Close();
        }

        private static void InitializeTextures()
        {
            TextureManager.LoadTextures("textures.txt");
        }

        private static void InitializeAnimations()
        {
            AnimationGroupManager.LoadAnimationGroups("textures.txt");
        }

        private static void InitializeWorlds()
        {
            WorldManager.LoadWorlds("worlds.txt");
        }

        private static void InitializeStates()
        {
            StateSystem = new StateSystem();
            StateSystem.RenderWindow = RenderWindow;

            StateSystem.AddState("animated_tile_state", new AnimatedTileTestState());

            StateSystem.SetCurrentState("animated_tile_state");
        }

        private static void CreateClock()
        {
            clock = new Clock();
        }

        private static void GameLoop()
        {
            while (RenderWindow.IsOpen())
            {
                double deltaTime = clock.Restart();

                RenderWindow.DispatchEvents();
                UpdateFrame(deltaTime);
                DrawFrame();
            }
        }

        private static void UpdateFrame(double deltaTime)
        {
            StateSystem.CurrentState.Update((float)deltaTime);
        }

        private static void DrawFrame()
        {
            RenderWindow.Clear();

            StateSystem.CurrentState.Render(RenderWindow);

            RenderWindow.Display();
        }
    }
}
