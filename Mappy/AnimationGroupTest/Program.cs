using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.Window;
using SFML.Graphics;

using Mappy;
using Mappy.States;
using Mappy.Textures;
using Mappy.Textures.Animation;

namespace AnimationGroupTest
{
    class Program
    {
        static Clock clock;

        public static StateSystem StateSystem;
        public static RenderWindow RenderWindow;

        static void Main(string[] args)
        {
            CreateWindow();

            //try
            //{
                InitializeTextures();
                InitializeAnimations();
                InitializeStates();
                CreateClock();

                GameLoop();
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e.Message + "\n" + e.StackTrace);
            //    Console.ReadKey(true);
            //}
        }

        private static void CreateWindow()
        {
            RenderWindow = new RenderWindow(new VideoMode(800, 600), "Test animation group system");

            RenderWindow.Closed += RenderWindow_Closed;
        }

        private static void RenderWindow_Closed(object sender, EventArgs e)
        {
            RenderWindow.Close();
        }

        private static void InitializeTextures()
        {
            TextureManager.AddTexture("characterSet", "characterSet.png");
        }

        private static void InitializeAnimations()
        {
            AnimationGroupAnimationManager.LoadAnimations("textures.txt");
            AnimationGroupManager.LoadAnimationGroups("textures.txt");
        }

        private static void InitializeStates()
        {
            StateSystem = new StateSystem();
            StateSystem.RenderWindow = RenderWindow;

            StateSystem.AddState("animation_group_state", new AnimationGroupState());

            StateSystem.SetCurrentState("animation_group_state");
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
