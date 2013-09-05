using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.Window;
using SFML.Graphics;

using Mappy;
using Mappy.Textures;
using Mappy.Textures.Animation;

namespace AnimationGroupTest
{
    class Player : IGameObject
    {
        float deltaTime;

        Vector2D speed;
        Vector2D position;

        AnimationGroup animation;

        public Player()
        {
            this.speed = new Vector2D(3, 3);
            this.position = new Vector2D(10, 10);

            animation = AnimationGroupManager.GetAnimationGroup("player_animation");
            animation.SetCurrentAnimation("player_up");

            Program.RenderWindow.KeyPressed += RenderWindow_KeyPressed;
            Program.RenderWindow.KeyReleased += RenderWindow_KeyReleased;
        }

        public void Update(float deltaTime)
        {
            this.deltaTime = deltaTime;
        }

        private void RenderWindow_KeyPressed(object sender, SFML.Window.KeyEventArgs e)
        {
            Vector2D copy = position;

            if (e.Code == Keyboard.Key.Left)
            {
                position.X -= speed.X;
                animation.SetCurrentAnimation("player_left");
            }
            if (e.Code == Keyboard.Key.Right)
            {
                position.X += speed.X;
                animation.SetCurrentAnimation("player_right");
            }
            if (e.Code == Keyboard.Key.Up)
            {
                position.Y -= speed.Y;
                animation.SetCurrentAnimation("player_up");
            }
            if (e.Code == Keyboard.Key.Down)
            {
                position.Y += speed.Y;
                animation.SetCurrentAnimation("player_down");
            }
            animation.Update(deltaTime);
        }

        private void RenderWindow_KeyReleased(object sender, KeyEventArgs e)
        {
        }

        public void Render(SFML.Graphics.RenderWindow renderWindow)
        {
            animation.Position = position.InternalVector;
            animation.Render(renderWindow);
        }
    }
}
