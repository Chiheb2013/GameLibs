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

namespace Pokemon
{
    class Player : IGameObject, IPhysicObject
    {
        float deltaTime;

        Vector2D speed;
        Vector2D position;

        AnimationGroup animation;

        GameState parent;

        IntRect hitbox;
        IntRect bufferHitbox;

        public IntRect Hitbox { get { return hitbox; } }
        public Vector2D Position { get { return position; } }
        public Vector2D Center { get { return CollisionHelper.GetCenter(bufferHitbox); } }

        public Player(GameState parent)
        {
            this.parent = parent;

            this.speed = new Vector2D(3, 3);
            this.position = new Vector2D(10, 10);

            animation = AnimationGroupManager.GetAnimationGroup("player_animation");
            animation.CurrentAnimation = "player_up";

            hitbox = CollisionHelper.CreateHitbox(position);

            Program.RenderWindow.KeyPressed += RenderWindow_KeyPressed;
            Program.RenderWindow.KeyReleased += RenderWindow_KeyReleased;
        }

        public void Update(float deltaTime)
        {
            this.deltaTime = deltaTime;
        }

        private void RenderWindow_KeyPressed(object sender, SFML.Window.KeyEventArgs e)
        {
            string animation = string.Empty;
            Vector2D copy = new Vector2D(position.X, position.Y);

            if (e.Code == Keyboard.Key.Left)
            {
                copy.X -= speed.X;
                animation = "player_left";
            }
            else if (e.Code == Keyboard.Key.Right)
            {
                copy.X += speed.X;
                animation = "player_right";
            }
            else if (e.Code == Keyboard.Key.Up)
            {
                copy.Y -= speed.Y;
                animation = "player_up";
            }
            else if (e.Code == Keyboard.Key.Down)
            {
                copy.Y += speed.Y;
                animation = "player_down";
            }

            Console.WriteLine("p(center) = " + Center.ToString());

            if (!Collides(copy))
            {
                this.animation.SetCurrentAnimation(animation);
                position = copy;
                hitbox = bufferHitbox;
                this.animation.Update(deltaTime);
            }
        }

        public bool Collides(IPhysicObject hitter)
        {
            throw new NotImplementedException();
        }

        public bool Collides(Vector2D hitterPosition)
        {
            bufferHitbox = CollisionHelper.CreateHitbox(hitterPosition);
            Vector2D worldCoords = CoordinateSystemConverter.PixelsToWorld(hitterPosition);

            if (parent.World.IsInBounds(worldCoords))       //if in world :
            {                                              //
                bool c = parent.World.Collides(this, 0);  //"advienne que pourra"
                return c;                                //====================
            }                                           //
            return true;                               //else, can't move
        }

        private void RenderWindow_KeyReleased(object sender, KeyEventArgs e)
        {
            animation.Reset();
        }

        public void Render(SFML.Graphics.RenderWindow renderWindow)
        {
            animation.Position = position.InternalVector;
            animation.Render(renderWindow);
        }
    }
}
