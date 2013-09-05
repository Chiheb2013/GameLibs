using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Mappy;
using Mappy.Worlds;
using Mappy.Textures;

using SFML.Window;
using SFML.Graphics;

namespace TestTopView
{
    class Player : IGameObject, IPhysicObject
    {
        float deltaTime;

        Vector2D speed;
        Vector2D position;

        AnimatedSprite sprite;

        public Player(Vector2D position)
        {
            this.speed = new Vector2D(3, 3);
            this.position = CoordinateSystemConverter.WorldToPixels(position);

            this.sprite = AnimationManager.GetAnimation("character");

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
                position.X -= speed.X;
            if (e.Code == Keyboard.Key.Right)
                position.X += speed.X;
            if (e.Code == Keyboard.Key.Up)
                position.Y -= speed.Y;
            if (e.Code == Keyboard.Key.Down)
                position.Y += speed.Y;

            //if (!Collides(position))
            //    sprite.Update(deltaTime);
            //else
            //{
            //    position = copy;
            //    sprite.Reset();
            //}
            sprite.Update(deltaTime);
        }

        private void RenderWindow_KeyReleased(object sender, KeyEventArgs e)
        {
            sprite.Reset();
        }

        public bool Collides(Vector2D hitterPosition)
        {
            Vector2D worldPosition = CoordinateSystemConverter.PixelsToWorld(hitterPosition);
            LayeredWorld world = (LayeredWorld)WorldManager.Worlds["first_world"];

            return world.Collides(worldPosition, 0);
        }

        public void Render(SFML.Graphics.RenderWindow renderWindow)
        {
            sprite.Position = position.InternalVector;
            sprite.Render(renderWindow);
        }
    }
}
