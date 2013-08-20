using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;

using SFML.Window;
using SFML.Graphics;

using Mappy;
using Mappy.Maps;
using Mappy.States;
using Mappy.Texture;
using Mappy.Texture.Animation;

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
            //int line = CoordinateSystemConverter.PlaneToLine(new Vector2D(13, 2), 30);
            //Console.WriteLine("p(13,2) = p(" + line + ")");

            //Vector2D plane = CoordinateSystemConverter.LineToPlane(line, 30);
            //Console.WriteLine("p(" + line + ") = p(" + plane.X + "," + plane.Y + ")");

            //Console.WriteLine("p(" + plane.X + "," + plane.Y + ") = p(" + CoordinateSystemConverter.PlaneToLine(plane, 30) + ")");

            //Console.ReadKey(true);
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
