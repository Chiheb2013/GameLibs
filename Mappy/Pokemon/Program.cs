using System;

using SFML.Window;
using SFML.Graphics;

using Mappy;
using Mappy.Worlds;
using Mappy.States;
using Mappy.Textures;
using Mappy.Textures.Animation;

namespace Pokemon
{
    class Program
    {
        static Clock clock;
        static RenderWindow renderWindow;
        static StateSystem stateSystem;

        public static RenderWindow RenderWindow { get { return renderWindow; } }

        public static void Main(string[] args)
        {
            CreateWindow();
            CreateClock();

            InitializeTextures();
            InitializeAnimations();
            InitializeWorlds();
            InitializeStates();

            GameLoop();
        }

        private static void CreateWindow()
        {
            renderWindow = new RenderWindow(new VideoMode(800, 600), "Pokemon");
            renderWindow.Closed += renderWindow_Closed;
        }

        private static void renderWindow_Closed(object sender, EventArgs e)
        {
            renderWindow.Close();
        }

        private static void CreateClock()
        {
            clock = new Clock();
        }

        private static void InitializeTextures()
        {
            GeneralTextureManager.LoadTextures("textures.txt");
        }

        private static void InitializeAnimations()
        {
            AnimationGroupManager.LoadAnimationGroups("textures.txt");
            AnimationGroupAnimationManager.LoadAnimations("textures.txt");
        }

        private static void InitializeWorlds()
        {
            WorldManager.LoadWorlds("worlds.txt");
        }

        private static void InitializeStates()
        {
            stateSystem = new StateSystem();

            stateSystem.AddState("game", new GameState());

            stateSystem.SetCurrentState("game");
        }

        private static void GameLoop()
        {
            while (renderWindow.IsOpen())
            {
                float deltaTime = Clock.AsMillisecond(clock.Restart());
                
                renderWindow.DispatchEvents();

                UpdateFrame(deltaTime);
                DrawFrame();
            }
        }

        private static void UpdateFrame(float deltaTime)
        {
            stateSystem.CurrentState.Update(deltaTime);
        }

        private static void DrawFrame()
        {
            renderWindow.Clear(SFML.Graphics.Color.White);

            stateSystem.CurrentState.Render(renderWindow);

            renderWindow.Display();
        }
    }
}
