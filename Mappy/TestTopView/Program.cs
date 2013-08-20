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

namespace TestTopView
{
    class Program
    {
        static Clock clock;

        public static StateSystem StateSystem;
        public static RenderWindow RenderWindow;

        static void Main(string[] args)
        {
            CreateWindow();

            //InitializeTextures();
            InitializeAnimations();
            //InitializeWorlds();
            InitializeStates();
            CreateClock();

            GameLoop();
        }

        private static void CreateWindow()
        {
            RenderWindow = new RenderWindow(new VideoMode(800, 600), "RPG");

            RenderWindow.Closed += RenderWindow_Closed;
        }

        private static void RenderWindow_Closed(object sender, EventArgs e)
        {
            RenderWindow.Close();
        }

        private static void InitializeTextures()
        {
            //TextureManager.LoadTextures("textures.txt");
            //TextureManager.AddTexture("layer_1", "layer_1.png");
        }

        private static void InitializeAnimations()
        {
            TextureManager.AddTexture("characterSet", "characterSet.png");
            AnimationManager.LoadAnimations("textures.txt");
        }

        private static void InitializeWorlds()
        {
            WorldManager.LoadWorlds("worlds.txt");
        }

        private static void InitializeStates()
        {
            StateSystem = new StateSystem();
            StateSystem.RenderWindow = RenderWindow;

            //StateSystem.AddState("top_view_state", new TopViewMapState());
            //StateSystem.AddState("parallax_screen_state", new ParallaxScreenState());
            StateSystem.AddState("character_animation_state", new CharacterAnimationState());

            //StateSystem.SetCurrentState("top_view_state");
            //StateSystem.SetCurrentState("parallax_screen_state");
            StateSystem.SetCurrentState("character_animation_state");
        }

        private static void CreateClock()
        {
            clock = new Clock();
        }

        private static void GameLoop()
        {
            while (RenderWindow.IsOpen())
            {
                double deltaTime = clock.GetDeltaTime();

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
